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
    private AudioClip _explosionSoundClip;

    private Player _player;
    private AudioSource _audioSource;
    private Renderer _renderer;
    private Collider2D _collider2D;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            Debug.LogError("The Player is NULL.");
        }
        if (!TryGetComponent(out _audioSource))
        {
            Debug.LogError("The AudioSource on the Asteroid is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
        if (!TryGetComponent(out _renderer))
        {
            Debug.LogError("The Renderer on the Asteroid is NULL.");
        }
        if (!TryGetComponent(out _collider2D))
        {
            Debug.LogError("The Collider2D on the Asteroid is NULL.");
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
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            if (_player != null)
            {
                _player.Damage();
            }

            _speed = 0;

            _renderer.enabled = false;
            Destroy(this.gameObject, 2.5f);
            Destroy(prefabInstance, 3f);
        }

        if (other.CompareTag("Laser"))
        {
            _audioSource.Play();

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(50);
            }

            _speed = 0;

            _renderer.enabled = false;
            Destroy(_collider2D);
            Destroy(this.gameObject, 2.5f);
            Destroy(prefabInstance, 3f);
        }

        if (other.CompareTag("Enemy"))
        {
            _audioSource.Play();

            GameObject prefabInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            _speed = 0;

            _renderer.enabled = false;
            Destroy(_collider2D);
            Destroy(this.gameObject, 2.5f);
            Destroy(prefabInstance, 3f);
        }
    }
}
