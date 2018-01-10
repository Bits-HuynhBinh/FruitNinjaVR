using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public List<SpawnPoint> spawnPoints;
    public List<GameObject> fruitPrefabs;

    public float minForceY;
    public float maxForceY;

    public static SpawnerManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public void Spawn(Level currentLevel)
    {


        // get random fruit to spawn
        int random = Random.Range(0, fruitPrefabs.Count);
        GameObject prefabToSpawn = fruitPrefabs[random];

        // get random spawn point 
        random = Random.Range(0, spawnPoints.Count);
        SpawnPoint spawnPoint = spawnPoints[random];

        // play the particle system when spawning the fruit
        ParticleManager.Instance.PlayParticleOn(spawnPoint.transform.position, ParticleManager.Instance.parSpawningFruit);

        // spawn the fruit at the spawn point position
        GameObject obj = Instantiate(prefabToSpawn, spawnPoint.gameObject.transform.position, Quaternion.identity);

        // throw the fruit into the space
        float randomForceY = Random.Range(minForceY, maxForceY);
        obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, randomForceY, 0));
    }

}
