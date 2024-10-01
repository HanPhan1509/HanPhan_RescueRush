using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Watermelon;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private float phase_1 = 400.0f;
    [SerializeField] private float phase_2 = 1000.0f;

    [Space(2.0f)]
    [Header("Cat")]
    [SerializeField] private Transform parentCat;
    [SerializeField] private int numberCat = 10;
    [SerializeField] private GameObject[] cats;

    [Space(2.0f)]
    [Header("Obstacle")]
    [SerializeField] private Transform parentObstacle;
    [SerializeField] private int totalObstacles = 10;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private List<Transform> lstObstaclePosition = new List<Transform>();
    [SerializeField] private Dictionary<GameObject, Transform> dictObstacle = new Dictionary<GameObject, Transform>();

    [Space(2.0f)]
    [Header("Road")]
    //[SerializeField] private Transform parentRoad;
    //[SerializeField] private GameObject prefStreetRoad;
    [SerializeField] private Mesh mStreetRoad;

    [Button("Generation cats")]
    private void GenerationCats()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)((phase_1 + phase_2) / 100);

        float xRoad = (totalRoad * xSizeRoad) / numberCat;
        float xPos = 0.0f;

        for (int i = 0; i < numberCat; i++)
        {
            GameObject prefCat = cats[Random.Range(0, cats.Length)];
            int randRot = Random.Range(0, 360);
            xPos += xRoad;
            Vector3 pos = new Vector3(xPos, 0, Random.Range(-10, 10));
            Quaternion quaternion = Quaternion.Euler(0, randRot, 0);

            Instantiate(prefCat, pos, quaternion, parentCat);
        }
    }  

    [Button("Generation obstacles")]
    private void GenerationObstacles()
    {
        float xSizeRoad = mStreetRoad.bounds.size.x;
        int totalRoad = (int)((phase_1 + phase_2) / 100);

        float xRoad = (totalRoad * xSizeRoad) / totalObstacles;
        float xPos = 0.0f;

        for (int i = 0; i < totalObstacles; i++)
        {
            GameObject prefObs = obstacles[Random.Range(0, obstacles.Length)];

            xPos += xRoad;
            Vector3 pos = new Vector3(xPos, 0, Random.Range(-10, 10));

            int randRot = Random.Range(0, 360);
            Quaternion quaternion = Quaternion.Euler(0, randRot, 0);

            GameObject obs = Instantiate(prefObs, pos, quaternion, parentObstacle);
            dictObstacle.Add(obs, obs.transform);
        }
    }

    [Button("Clear All")]
    private void ClearAll()
    {
        if(parentCat.childCount > 0)
        {
            foreach (Transform child in parentCat.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        if(parentObstacle.childCount > 0)
        {
            foreach (Transform child in parentObstacle.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }    
    }    

    //[Button("ROAD")]
    //private void SetupStraightRoad()
    //{
    //    float xSizeRoad = mStreetRoad.bounds.size.x;
    //    int totalRoad = (int)((phase_1 + phase_2) / 100);

    //    for (int i = 0; i < totalRoad; i++)
    //    {
    //        Instantiate(prefStreetRoad, new Vector3(xSizeRoad * i, 0, 0), Quaternion.identity, parentRoad);
    //    }
    //}
}
