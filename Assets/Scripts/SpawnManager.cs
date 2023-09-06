using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    [SerializeField]
    private float _spawnRate = 3.0f;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
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
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        // Wait 20 seconds before spawning the first power up
        yield return new WaitForSeconds(20);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
            Instantiate(_tripleShotPowerUpPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 21));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
