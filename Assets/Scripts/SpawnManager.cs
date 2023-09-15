using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroidPrefab;
    [SerializeField]
    private GameObject _asteroidContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private float _enemySpawnRate = 3.0f;
    [SerializeField]
    private float _asteroidSpawnRate = 10.0f;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    // Spawn game objects every 3 seconds
    IEnumerator EnemySpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            // Instantiate an enemy prefab
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

            // Set the parent of the new enemy to the enemy container
            newEnemy.transform.parent = _enemyContainer.transform;

            // Wait for 3 seconds
            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(10);

        while (_stopSpawning == false)
        {
            // Instantiate an asteroid prefab
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 9.0f, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;

            yield return new WaitForSeconds(_asteroidSpawnRate);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        // Wait 20 seconds before spawning the first power up
        yield return new WaitForSeconds(20);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 21));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
