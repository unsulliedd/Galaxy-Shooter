using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _powerUpID; // 0 = Triple Shot, 1 = Speed, 2 = Shields
    [SerializeField]
    private float _speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((_speed * Time.deltaTime) * Vector3.down);
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("No power up collected");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
