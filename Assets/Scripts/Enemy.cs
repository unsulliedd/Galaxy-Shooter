using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if(transform.position.y < -6.0f)
        {
            float randomXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomXPosition, 7.0f, 0);
        }
    }
}
