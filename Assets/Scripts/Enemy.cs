using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(_player != null)
            {
                _player.Damage();
            }
            Destroy(this.gameObject);
        }

        else if(other.CompareTag("Laser"))
        {
            if(_player != null)
            {
                _player.AddScore(10);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
