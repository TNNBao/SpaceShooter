using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    public GameObject smallAsteroid;
    public GameObject bigAsteroid;
    public GameObject massiveAsteroid;
    public GameObject star;
    public float spawnTimer;
    public float spawnInterval;

    void Update()
    {
        spawnTimer += Time.deltaTime * PlayerController.Instance.boost;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            SpawnRandomAsteroid();
            SpawnStar();
        }
    }

    private void SpawnRandomAsteroid()
    {
        int randomAsteroidType = Random.Range(0, 3);
        switch (randomAsteroidType)
        {
            case 0: Instantiate(smallAsteroid, RandomSpawnPos(), transform.rotation); break;
            case 1: Instantiate(bigAsteroid, RandomSpawnPos(), transform.rotation); break;
            case 2:
                float spawnChance = Random.Range(0f, 1f);
                if (spawnChance > 0.8f) Instantiate(massiveAsteroid, RandomSpawnPos(), transform.rotation); 
                break;
        }

    }
    
    private void SpawnStar()
    {
        float spawnChance = Random.Range(0f, 1f);
        if (spawnChance > 0.8f) Instantiate(star, RandomSpawnPos(), transform.rotation);
    }
    
    private Vector2 RandomSpawnPos()
    {
        Vector2 spawnPos;

        spawnPos.x = minPos.position.x;
        spawnPos.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPos;
    }
}
