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

    [SerializeField]
    public EnemyType[] myEnemyTypes;
    public Transform[] mySpawnPoints;
    public GameObject[] myItemTypes;
    public int myNumberOfEnemiesAlive;
    private int myCurrentWave = 0;
    public int myWaveValue;
    public float myTimeBtwSpawns = 100;
    private float timeBtwSpawns;
    bool isSpawningEnemies = false;
    int myLowestCostingEnemy;

    // Start is called before the first frame update
    void Start()
    {
        myNumberOfEnemiesAlive = 0;
        myLowestCostingEnemy = GetLowestCostingEnemies();
        StartNewWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(myNumberOfEnemiesAlive == 0 && isSpawningEnemies == false)
        {
            StartNewWave();
        }

        if (isSpawningEnemies)
        {
            SpawnEnemies();
        }

    }

    void StartNewWave()
    {
        isSpawningEnemies = true;
        myCurrentWave++;
        myWaveValue = myCurrentWave * 20;
    }

    void SpawnEnemies()
    {
        while(myWaveValue >= myLowestCostingEnemy)
        {
            if(timeBtwSpawns <= 0)
            {
                int randEnemy = Random.Range(0, myEnemyTypes.Length);
                int randSpawn = Random.Range(0, mySpawnPoints.Length);
                if(myEnemyTypes[randEnemy].myCost <= myWaveValue && myEnemyTypes[randEnemy].mySpawnAfterWave <= myCurrentWave)
                {
                    Instantiate(myEnemyTypes[randEnemy].myEnemy, mySpawnPoints[randSpawn].position, Quaternion.identity);
                    myNumberOfEnemiesAlive++;
                    myWaveValue -= myEnemyTypes[randEnemy].myCost;
                    timeBtwSpawns = myTimeBtwSpawns;
                }
            }
            timeBtwSpawns -= Time.fixedDeltaTime;
        }
        isSpawningEnemies = false;
    }

    public void OnEnemyDeath()
    {
        myNumberOfEnemiesAlive--;
    }

    int GetLowestCostingEnemies()
    {
        int lowestCostingEnemy = myEnemyTypes[0].myCost;
        for (int i = 0; i < myEnemyTypes.Length; i++)
        {
            if(lowestCostingEnemy > myEnemyTypes[i].myCost)
            {
                lowestCostingEnemy = myEnemyTypes[i].myCost;
            }
        }
        return lowestCostingEnemy;
    }

    public void SpawnItem(Vector3 aPos)
    {
        int rand = Random.Range(0, myItemTypes.Length);
        Instantiate(myItemTypes[rand], aPos, Quaternion.identity);
    }

    [System.Serializable]
    public class EnemyType
    {
        public GameObject myEnemy;
        public int mySpawnAfterWave;
        public int myCost;
    }
}
