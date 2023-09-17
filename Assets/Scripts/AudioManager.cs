using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionSound;

    public void ExplosionSound(Vector3 position)
    {
        if (_explosionSound != null)
        {
            if (_explosionSound.TryGetComponent<AudioSource>(out var __explosionSoundclip))
            {
                AudioSource explosionInstance = Instantiate(__explosionSoundclip, position, Quaternion.identity);
                explosionInstance.Play();
                Destroy(explosionInstance.gameObject, __explosionSoundclip.clip.length);
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
}
