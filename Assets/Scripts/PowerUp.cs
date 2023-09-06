using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
