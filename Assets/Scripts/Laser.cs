using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser)
        {
            EnemyLaser();
        }
        else
        {
            PlayerLaser();
        }
    }

    private void PlayerLaser()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
        if (transform.position.y > 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void EnemyLaser()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        if (transform.position.y < -8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && _isEnemyLaser)
        {
            if(other.TryGetComponent<Player>(out var player))
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
