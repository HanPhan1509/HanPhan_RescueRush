using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Watermelon;
using static Cinemachine.DocumentationSortingAttribute;
using static UnityEditor.PlayerSettings;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject level;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private float phase_1 = 400.0f;
    [SerializeField] private float phase_2 = 1000.0f;

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

    private void Awake()
    {
        if (DataManager.level != null)
        {
            this.level = DataManager.level;
            parentCat.gameObject.SetActive(true);
            parentObstacle.gameObject.SetActive(true);
            LoadLevel();
        }    
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
        //Spawn cat
        foreach (Vector3 pos in lstTransformCat)
        {
            Quaternion quaternion = Quaternion.Euler(0, Random.Range(0, 360), 0);
            CreateCat(pos, quaternion);
        }

        //Spawn obstacle
        foreach (Vector3 pos in lstObstaclePosition)
        {
            Quaternion quaternion = Quaternion.Euler(0, Random.Range(0, 360), 0);
            CreateObstacle(pos, quaternion);
        }
    }

    [Button("Generation level")]
    private void GenerationLevel()
    {
        SetupStraightRoad();
        GenerationObstacles();
        GenerationCats();
    }

    [Button("Generation cats")]
    private void GenerationCats()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)((phase_1 + phase_2) / 100);

        float zRoad = (totalRoad * xSizeRoad) / numberCat;
        float zPos = 0.0f;

        lstTransformCat.Clear();
        for (int i = 0; i < numberCat; i++)
        {
            zPos += zRoad;
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, zPos);
            Quaternion quaternion = Quaternion.Euler(0, Random.Range(0, 360), 0);
            lstTransformCat.Add(pos);
            CreateCat(pos, quaternion);
        }
    }

    private void CreateCat(Vector3 pos, Quaternion rot)
    {
        GameObject prefCat = cats[Random.Range(0, cats.Length)];
        Instantiate(prefCat, pos, rot, parentCat);
    }

    [Button("Generation obstacles")]
    private void GenerationObstacles()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)((phase_1 + phase_2) / 100);

        float zRoad = (totalRoad * xSizeRoad) / numberCat;
        float zPos = 0.0f;

        lstObstaclePosition.Clear();
        for (int i = 0; i < totalObstacles; i++)
        {
            zPos += zRoad;
            int randRot = Random.Range(0, 360);
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, zPos);
            Quaternion quaternion = Quaternion.Euler(0, randRot, 0);
            //dictObstacle.Add(obs, obs.transform);
            lstObstaclePosition.Add(pos);
            CreateObstacle(pos, quaternion);
        }
    }

    private void CreateObstacle(Vector3 pos, Quaternion rot)
    {
        GameObject prefObs = obstacles[Random.Range(0, obstacles.Length)];
        Instantiate(prefObs, pos, rot, parentObstacle);
    }

    [Button("ROAD")]
    private void SetupStraightRoad()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)((phase_1 + phase_2) / 100) + 10;

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
    }

    [Space]

    public LevelScriptableObject[] levels;
    [SerializeField] private int numberMap = 10;
    [SerializeField] private int startNumber = 1;

    [Button("Automation Generate Map")]
    private void AutomationGenerateMap()
    {
        ClearAll();
        for (int i = startNumber; i < numberMap; i++)
        {
            numberCat += (i % 2);
            totalObstacles += (i / 2);
            GenerationLevel();
            SaveLevel();
            i += 1;
        }
    }

    [MenuItem("Tools/Create Levels")]
    private void SaveLevel()
    {
        LevelScriptableObject level = ScriptableObject.CreateInstance<LevelScriptableObject>();
        level.lstTransformCat = lstTransformCat;
        level.lstTransformObstacle = lstObstaclePosition;

        string resourcePath = "Assets/Resources/Levels";
        string assetPath = $"{resourcePath}/Level_{numberMap}.asset";
        AssetDatabase.CreateAsset(level, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
