using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _fireRate = 3.0f;
    [SerializeField]
    private float _canFire = 0f;
    [SerializeField]
    private GameObject _doubleLaser;

    private Player _player;
    private Animator _animator;
    private Collider2D _collider2D;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.Find("Player").TryGetComponent(out _player))
        {
            Debug.LogError("The Player is NULL.");
        }
        if (!GameObject.Find("Audio_Manager").TryGetComponent(out _audioManager))
        {
            Debug.LogError("The Game Manager is NULL.");
        }
        if (!TryGetComponent(out _animator))
        {
            Debug.LogError("The Animator is NULL.");
        }
        if(!TryGetComponent(out _collider2D))
        {
            Debug.LogError("The Collider2D is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        EnemyFire();
    }

    private void EnemyMovement()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (_player != null && transform.position.y < -6.0f)
        {
            float randomXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomXPosition, 7.0f, 0);
        }
        else if (_player == null)
        {
            _audioManager.ExplosionSound(transform.position);
            Destroy(_collider2D);
            Destroy(this.gameObject);
        }
    }

    private void EnemyFire()
    {
        if (Time.time > _canFire && _collider2D != null)
        {
            _fireRate = Random.Range(5.0f, 8.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaserPrefab = Instantiate(_doubleLaser, transform.position + new Vector3(0.1f, -1, 0), Quaternion.identity);
            Laser[] lasers = enemyLaserPrefab.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(_player != null)
            {
                _player.Damage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioManager.ExplosionSound(transform.position);

            Destroy(_collider2D);
            Destroy(this.gameObject, 2.8f);

        }

        else if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioManager.ExplosionSound(transform.position);

            Destroy(_collider2D);
            Destroy(this.gameObject, 2.8f);
        }
    }
}
