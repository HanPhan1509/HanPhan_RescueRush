using Imba.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Watermelon;

public class LevelEditor : ManualSingletonMono<LevelEditor>
{
    [SerializeField] private LevelScriptableObject level;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private float phase_1 = 400.0f;
    [SerializeField] private float phase_2 = 1000.0f;

    [Space(2.0f)]
    [Header("PHASE")]
    [SerializeField] private Transform parentPhase;
    [SerializeField] private Transform loopPhase1;
    [SerializeField] private Transform loopPhase2;
    [SerializeField] private int numberLoop = 0;

    [Space(2.0f)]
    [Header("Cat")]
    [SerializeField] private Transform parentCat;
    [SerializeField] private int numberCat = 5;
    [SerializeField] private GameObject[] cats;
    [SerializeField] private List<Vector3> lstTransformCat = new List<Vector3>();

    [Space(2.0f)]
    [Header("Obstacle")]
    [SerializeField] private Transform parentObstacle;
    [SerializeField] private int totalObstacles = 10;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private List<Vector3> lstObstaclePosition = new List<Vector3>();
    [SerializeField] private Dictionary<GameObject, Transform> dictObstacle = new Dictionary<GameObject, Transform>();

    [Space(2.0f)]
    [Header("Road")]
    [SerializeField] private Transform parentRoad;
    [SerializeField] private GameObject prefStreetRoad;
    [SerializeField] private Mesh mStreetRoad;

    [Space(2.0f)]
    [Header("Ground")]
    [SerializeField] private Transform ground;

    private void Start()
    {
        if (DataManager.level != null)
        {
            this.level = DataManager.level;
            DataManager.OnLoopPhase = LoopPhase;
            numberLoop = 0;
            parentCat.gameObject.SetActive(true);
            parentObstacle.gameObject.SetActive(true);
            loopPhase2 = Instantiate(loopPhase1, new Vector3(0, 0, phase_1), Quaternion.identity, parentPhase);
            LoadLevel();
        }
    }

    [Button("LOOP")]
    private void LoopPhase()
    {
        if(numberLoop < 1)
        {
            numberLoop++;
            return;
        }    
        if (numberLoop % 2 == 0)
            loopPhase2.position = new Vector3(0, 0, phase_1 + (phase_1 * (float)numberLoop));
        else
            loopPhase1.position = new Vector3(0, 0, phase_1 + (phase_1 * (float)numberLoop));
        numberLoop++;
    }

    [Button("Load level")]
    private void LoadLevel()
    {
        ClearAll();
        if (level == null)
        {
            int currentLevel = DataGameSave.GetLevel();
            LoadLevels(currentLevel);
        }
        else
        {
            SpawnCatAndObstacle(level);
        }
    }

    private void LoadLevels(int levelIndex)
    {
        string levelName = $"Level_{levelIndex}";

        level = Resources.Load<LevelScriptableObject>($"Levels/{levelName}");

        if (level != null)
        {
            SpawnCatAndObstacle(level);
        }
        else
        {
            Debug.LogError($"Failed to load {levelName}");
        }
    }

    private void SpawnCatAndObstacle(LevelScriptableObject level)
    {
        lstTransformCat = level.lstTransformCat;
        lstObstaclePosition = level.lstTransformObstacle;
        GenerationCats();
        GenerationObstacles();
    }

    [Button("Generation cats")]
    private void GenerationCats()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)(phase_1 / xSizeRoad);

        float zRoad = (totalRoad * xSizeRoad) / numberCat;
        float zPos = 0.0f;

        if (lstTransformCat.Count > 0)
        {
            //Create with list
            for (int i = 0; i < lstTransformCat.Count; i++)
            {
                CreateCat(lstTransformCat[i]);
            }
        }
        else
        {
            //Random new pos for cats
            for (int i = 0; i < numberCat; i++)
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, zPos);
                lstTransformCat.Add(pos);
                CreateCat(pos);
                zPos += zRoad;
            }
        }

    }

    private void CreateCat(Vector3 pos)
    {
        GameObject prefCat = cats[UnityEngine.Random.Range(0, cats.Length)];
        Quaternion rot = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Instantiate(prefCat, pos, rot, parentCat);
    }

    [Button("Generation obstacles")]
    private void GenerationObstacles()
    {
        float zRoad = phase_1 / totalObstacles;
        float zPos = 0.0f;

        if (lstObstaclePosition.Count > 0)
        {
            //Create with list
            for (int i = 0; i < lstObstaclePosition.Count; i++)
            {
                CreateObstacle(lstObstaclePosition[i]);
            }
        }
        else
        {
            for (int i = 0; i < totalObstacles; i++)
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, Mathf.CeilToInt(zPos));
                lstObstaclePosition.Add(pos);
                CreateObstacle(pos);
                zPos += zRoad;
            }
        }
    }

    private void CreateObstacle(Vector3 pos)
    {
        GameObject prefObs = obstacles[UnityEngine.Random.Range(0, obstacles.Length)];
        Quaternion rot = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Instantiate(prefObs, pos, rot, parentObstacle);
    }

    [Button("ROAD")]
    private void SetupStraightRoad()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)(phase_1 / xSizeRoad);

        for (int i = 0; i < totalRoad; i++)
        {
            Instantiate(prefStreetRoad, new Vector3(0, 0, xSizeRoad * i), Quaternion.identity, parentRoad);
        }
    }

    [Button("Clear All")]
    private void ClearAll()
    {
        if (parentCat.childCount > 0)
        {
            List<Transform> childrenToDestroy = new List<Transform>();

            foreach (Transform child in parentCat)
            {
                childrenToDestroy.Add(child);
            }

            foreach (Transform child in childrenToDestroy)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        if (parentObstacle.childCount > 0)
        {
            List<Transform> childrenToDestroy = new List<Transform>();

            foreach (Transform child in parentObstacle)
            {
                childrenToDestroy.Add(child);
            }

            foreach (Transform child in childrenToDestroy)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        lstTransformCat.Clear();
        lstObstaclePosition.Clear();
    }

    [Space]

    public List<LevelScriptableObject> levels;
    [SerializeField] private int startNumber = 1;
    [SerializeField] private int endNumber = 10;

    [Button("Automation Generate Map")]
    private void AutomationGenerateMap()
    {
        levels.Clear();
        for (int i = startNumber; i <= endNumber; i++)
        {
            ClearAll();
            numberCat += (i % 2);
            totalObstacles += (i / 2);
            GenerationObstacles();
            GenerationCats();
            numberLevel = i;
            SaveLevel();
        }
    }

    [SerializeField] private int numberLevel = 1;
    [Button("Save Level")]
    private void SaveLevel()
    {
        LevelScriptableObject level = ScriptableObject.CreateInstance<LevelScriptableObject>();
        level.lstTransformCat = new List<Vector3>(lstTransformCat);
        level.lstTransformObstacle = new List<Vector3>(lstObstaclePosition);
        levels.Add(level);

        string resourcePath = "Assets/Resources/Levels";
        string assetPath = $"{resourcePath}/Level_{numberLevel}.asset";
        AssetDatabase.CreateAsset(level, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
