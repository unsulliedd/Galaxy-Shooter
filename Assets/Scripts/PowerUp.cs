using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _powerUpID; // 0 = Triple Shot, 1 = Speed, 2 = Shields
    [SerializeField]
    private float _speed = 3.0f;

    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Audio_Manager").TryGetComponent(out _audioManager))
        {
            Debug.LogError("The Game Manager is NULL.");
        }
    }

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
            if (other.transform.TryGetComponent<Player>(out var player))
            {
                switch (_powerUpID)
                {
                    case 0:
                        _audioManager.PowerUpPickUpSound(transform.position);
                        player.TripleShotActive();
                        break;
                    case 1:
                        _audioManager.PowerUpPickUpSound(transform.position);
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        _audioManager.PowerUpPickUpSound(transform.position);
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
