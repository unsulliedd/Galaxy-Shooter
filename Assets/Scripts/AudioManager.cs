using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionSound;
    [SerializeField]
    private GameObject _powerUpPickUpSound;
    [SerializeField]
    private GameObject _laserSound;

    public void ExplosionSound(Vector2 position)
    {
        if (_explosionSound != null)
        {
            if (_explosionSound.TryGetComponent<AudioSource>(out var _explosionSoundclip))
            {
                AudioSource explosionInstance = Instantiate(_explosionSoundclip, position, Quaternion.identity);
                explosionInstance.Play();
                Destroy(explosionInstance.gameObject, _explosionSoundclip.clip.length);
            }
            else
            {
                Debug.LogError("AudioSource component is missing in _explosionSound.");
            }
        }
        else
        {
            Debug.LogError("_explosionSound is not assigned in AudioManager.");
        }
    }

    public void PowerUpPickUpSound(Vector2 position)
    {
        if (_powerUpPickUpSound != null)
        {
            if (_powerUpPickUpSound.TryGetComponent<AudioSource>(out var _powerUpPickUpSoundclip))
            {
                AudioSource powerUpSoundInstance = Instantiate(_powerUpPickUpSoundclip, position, Quaternion.identity);
                powerUpSoundInstance.Play();
                Destroy(powerUpSoundInstance.gameObject, _powerUpPickUpSoundclip.clip.length);
            }
            else
            {
                Debug.LogError("AudioSource component is missing in _powerUpPickUpSound.");
            }
        }
        else
        {
            Debug.LogError("_powerUpSound is not assigned in AudioManager.");
        }
    }

    public void DoubleLaserSound(Vector2 position)
    {
        if (_laserSound != null)
        {
            if (_laserSound.TryGetComponent<AudioSource>(out var _doubleLaserSoundclip))
            {
                AudioSource doubleLaserSoundInstance = Instantiate(_doubleLaserSoundclip, position, Quaternion.identity);
                doubleLaserSoundInstance.Play();
                Destroy(doubleLaserSoundInstance.gameObject, _doubleLaserSoundclip.clip.length);
            }
            else
            {
                Debug.LogError("AudioSource component is missing in _doubleLaserSound.");
            }
        }
        else
        {
            Debug.LogError("_doubleLaserSound is not assigned in AudioManager.");
        }
    }
}
