using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public List<SpawnPoint> spawnPoints;
    public List<GameObject> fruitPrefabs;

    public List<GameObject> boomPrefabs;

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


    public void SpawnFruit(Level currentLevel)
    {
        int currentFruitOnScene = GameManager.Instance.currentLevel.currentOnSceneFruits;
        int maxFruitOnScene = GameManager.Instance.currentLevel.maxOnSceneFruits;
        int minFruitOnScene = GameManager.Instance.currentLevel.minOnsceneFruits;

        if (currentFruitOnScene < maxFruitOnScene)
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

            // increase the current on scene fruit count
            GameManager.Instance.currentLevel.currentOnSceneFruits++;

            // throw the fruit into the space
            float randomForceY = Random.Range(minForceY, maxForceY);
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, randomForceY, 0));
        }
    }

    public void SpawnBoom(Level currentLevel)
    {
        int currentBoomOnScene = GameManager.Instance.currentLevel.currentOnSceneBoom;
        int maxBoomOnScene = GameManager.Instance.currentLevel.maxOnSceneBoom;
        int minBoomOnScene = GameManager.Instance.currentLevel.minOnSceneBoom;

        if (currentBoomOnScene < maxBoomOnScene)
        {
            // get random fruit to spawn
            int random = Random.Range(0, boomPrefabs.Count);
            GameObject prefabToSpawn = boomPrefabs[random];

            // get random spawn point 
            random = Random.Range(0, spawnPoints.Count);
            SpawnPoint spawnPoint = spawnPoints[random];

            // play the particle system when spawning the fruit
            ParticleManager.Instance.PlayParticleOn(spawnPoint.transform.position, ParticleManager.Instance.parSpawningBoom);

            // spawn the fruit at the spawn point position
            GameObject obj = Instantiate(prefabToSpawn, spawnPoint.gameObject.transform.position, Quaternion.identity);

            // increase the current on scene fruit count
            GameManager.Instance.currentLevel.currentOnSceneBoom++;

            // throw the fruit into the space
            float randomForceY = Random.Range(minForceY, maxForceY);
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, randomForceY, 0));
        }
    }

}
