using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EnemyManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Transform[] mySpawnPoints;
    public GameObject[] myEnemyTypes;
    public GameObject[] myItemTypes;
    public float myTimeBtwSpawns;
    private float timeBtwSpawns;
    public int myAmountOfEnemySpawns;
    int randomEnemyType;
    int randomSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = myTimeBtwSpawns;
        randomEnemyType = Random.Range(0, 0);
        randomSpawnPoint = Random.Range(0, mySpawnPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            for (int i = 0; i < myAmountOfEnemySpawns; i++)
            {
                Instantiate(myEnemyTypes[randomEnemyType], mySpawnPoints[randomSpawnPoint].position, Quaternion.identity);
                randomEnemyType = Random.Range(0, 2);
                Debug.Log(randomEnemyType);
                randomSpawnPoint = Random.Range(0, mySpawnPoints.Length);
            }
            timeBtwSpawns = myTimeBtwSpawns;
            myAmountOfEnemySpawns += 1;
        }
        timeBtwSpawns -= Time.deltaTime;
    }

    public void SpawnItem(Vector3 aPos)
    {
        int rand = Random.Range(0, myItemTypes.Length);
        Instantiate(myItemTypes[rand], aPos, Quaternion.identity);
    }
}
