using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private bool _isDestroyed = false;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on the z axis at 20 degrees per second
        transform.localEulerAngles += new Vector3(0, 0, _rotateSpeed * Time.deltaTime);
        
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        // If the object goes off the screen on the bottom, destroy it
        if (transform.position.x > 12f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDestroyed)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            _isDestroyed = true;

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            if (_player != null)
            {
                _player.Damage();
            }

            _speed = 0;
            Destroy(this.gameObject, 0.25f);
            Destroy(prefabInstance, 3f);
        }

        if (other.CompareTag("Laser"))
        {
            _isDestroyed = true;

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(50);
            }

            _speed = 0;
            Destroy(this.gameObject, 0.25f);
            Destroy(prefabInstance, 3f);
        }
        if (other.CompareTag("Enemy"))
        {
            _isDestroyed = true;

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            _speed = 0;
            Destroy(this.gameObject, 0.25f);
            Destroy(prefabInstance, 3f);
        }
    }
}
