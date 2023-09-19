using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Player _player;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            Debug.LogError("The Player is NULL.");
        }
        if (!GameObject.Find("Audio_Manager").TryGetComponent(out _audioManager))
        {
            Debug.LogError("The Game Manager is NULL.");
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
        GameObject explosionPrefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        if (other.CompareTag("Player"))
        {
            _audioManager.ExplosionSound(transform.position);


            if (_player != null)
            {
                _player.Damage();
            }

            _speed = 0;

            Destroy(this.gameObject, 0.25f);
            Destroy(explosionPrefabInstance, 3f);
        }

        if (other.CompareTag("Laser"))
        {
            _audioManager.ExplosionSound(transform.position);

            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(50);
            }

            _speed = 0;

            Destroy(this.gameObject, 0.25f);
            Destroy(explosionPrefabInstance, 3f);
        }

        if (other.CompareTag("Enemy"))
        {
            _audioManager.ExplosionSound(transform.position);

            Destroy(other.gameObject);

            _speed = 0;

            Destroy(this.gameObject, 0.25f);
            Destroy(explosionPrefabInstance, 3f);
        }

        if (other.CompareTag("EnemyLaser"))
        {
            _audioManager.ExplosionSound(transform.position);

            Destroy(other.gameObject);

            _speed = 0;

            Destroy(this.gameObject, 0.25f);
            Destroy(explosionPrefabInstance, 3f);
        }
    }
}
