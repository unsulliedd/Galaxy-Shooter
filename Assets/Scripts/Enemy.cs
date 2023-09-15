using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if(_player != null && transform.position.y < -6.0f)
        {
            float randomXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomXPosition, 7.0f, 0);
        }
        else if(_player == null)
        {
            Destroy(this.gameObject);
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
            Destroy(this.gameObject, 2.2f);
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
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.2f);
        }
    }
}
