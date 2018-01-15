using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    public GameObject bossPrefab;
    public List<GameObject> bossSpawnPoints; // also the path points

    private List<GameObject> spawnedBosses = new List<GameObject>();

    public bool isSpawn = false;
    public int howManyBossToSpawn = 1;
    public int howManyBossOnScene = 0;


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawn)
        {
            if(howManyBossOnScene < howManyBossToSpawn)
            {
                for (int i = 0; i < howManyBossToSpawn; i++)
                {
                    SpawnBoss();
                    howManyBossOnScene++;
                }
            }
            
        }
    }

    public void SpawnBoss()
    {
        int random = Random.Range(0, bossSpawnPoints.Count);
        GameObject spawnPoint = bossSpawnPoints[random];

        GameObject obj = Instantiate(bossPrefab, spawnPoint.transform.position, Quaternion.identity);
        obj.transform.parent = this.gameObject.transform;
        spawnedBosses.Add(obj);

        //obj.GetComponent<BossController>().MakeColor();
    }
}
