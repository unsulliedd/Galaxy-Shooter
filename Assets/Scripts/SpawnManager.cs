using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _spawnRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn game objects every 3 seconds
    IEnumerator SpawnRoutine()
    {
        while (true)
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
}
