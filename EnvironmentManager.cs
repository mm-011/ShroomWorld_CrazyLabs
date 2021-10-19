using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironmentManager : MonoBehaviour
{
    [Header("SET")]
    public GameObject player;
    public GameObject obstacle1Prefab;
    public GameObject obstacle2Prefab;
    public GameObject coinPrefab;
    public GameObject floorsParent;
    public GameObject obstaclesParent;
    public GameObject coinParent;
    public float floorRadiusSpawning;
    [Range(1, 5)] public int obstacleSpawnDiley;
    [Range(1, 5)] public int obstacleIntesityLevel;
    public float finishDistance;
    [Header("DON'T TOUCH")]
    public List<GameObject> floors;
    public List<GameObject> obstacles;
    public List<GameObject> coins;
    public float playerPositionZ;
    public float playerPositionZRoundingPoint;
    public float nextEnvironmentPositionZ;

    

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnvironmentFloors();
        SpawnObstaclesAndCoins();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeEnvironmentFloors()
    {
        for(int i = 0; i < floorsParent.transform.childCount; i++)
        {
            floors.Add(floorsParent.transform.GetChild(i).gameObject);
        }
    }
        
    public int intensityCounter = 0;
    public void SpawnObstaclesAndCoins()
    {
        for (int i = 0; i < floorsParent.transform.childCount-2; i++)
        {
            if (i >= obstacleSpawnDiley)
            {
                if (intensityCounter == 5 - obstacleIntesityLevel)
                {
                    GameObject obstacle = PickRandomObstacle();
                    int obstaclePositionX = PickRandomNumberBetween(-4, 4);
                    int? coinPositionX = null;

                    if (RandomTrueOrFalse() == true)
                    {
                        if (obstacle == obstacle1Prefab)
                        {                        
                            coinPositionX = CalculatePositionXForCoin(2, obstaclePositionX);
                        }
                        else if(obstacle == obstacle2Prefab)
                        {
                            coinPositionX = CalculatePositionXForCoin(2, obstaclePositionX);
                        }
                        if (coinPositionX != null)
                        {
                            Vector3 coinPosition = new Vector3((float)coinPositionX, 0, floorsParent.transform.GetChild(i).gameObject.transform.position.z);
                            coins.Add(Instantiate(coinPrefab, coinPosition, Quaternion.Euler(0, 180, 0), coinParent.transform));
                        }                        
                    }    

                    Vector3 obstaclePosition = new Vector3(obstaclePositionX, 0, floorsParent.transform.GetChild(i).gameObject.transform.position.z);                    
                    obstacles.Add(Instantiate(obstacle, obstaclePosition , Quaternion.Euler(0, 180, 0), obstaclesParent.transform));                    
                    intensityCounter = 0;
                }
                else
                {
                    intensityCounter++;
                }
            }
        }
    }

    public int? CalculatePositionXForCoin(int minimumDistance, int obstaclePositionX)
    {
        int? positionX = null;
        if (RandomTrueOrFalse() == true)
        {
            for (int j = -4; j <= 4; j++)
            {
                if (j > obstaclePositionX && Mathf.Abs(j - obstaclePositionX) > minimumDistance)
                {
                    positionX = j;
                    break;
                }
                else if (j <= obstaclePositionX && Mathf.Abs(obstaclePositionX - j) > minimumDistance)
                {
                    positionX = j;
                    break;
                }
            }
        }
        else
        {
            for (int j = 4; j >= -4; j--)
            {
                if (j > obstaclePositionX && Mathf.Abs(j - obstaclePositionX) > minimumDistance)
                {
                    positionX = j;
                    break;
                }
                else if (j <= obstaclePositionX && Mathf.Abs(obstaclePositionX - j) > minimumDistance)
                {
                    positionX = j;
                    break;
                }
            }
        }

        return positionX;
    }

    public GameObject PickRandomObstacle()
    {
        GameObject randomObstacle=null;
        switch (Random.Range(0, 2))
        {
            case 0:
                randomObstacle = obstacle1Prefab;
                break;
            case 1:
                randomObstacle = obstacle2Prefab;
                break;
        }
        return randomObstacle;
    }

    public bool? RandomTrueOrFalse()
    {
        bool? trueOrFalse=null;
        switch (Random.Range(0, 2))
        {
            case 0:
                trueOrFalse = false;
                break;
            case 1:
                trueOrFalse = true;
                break;
        }
        return trueOrFalse;
    }

    public int PickRandomNumberBetween(int a, int b)
    {
        return Random.Range(a, b);
    }

}
