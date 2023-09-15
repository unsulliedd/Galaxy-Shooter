using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    [SerializeField]
    private GameObject _explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on the z axis at 20 degrees per second
        transform.localEulerAngles += new Vector3(0, 0, _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.25f);
            Destroy(prefabInstance, 3f);
        }
    }
}
