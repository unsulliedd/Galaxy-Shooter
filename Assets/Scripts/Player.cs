using System.Collections;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedBoostMultiplier = 2.0f;
    [SerializeField]
    private float _fireRate = 0.345f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private GameObject _leftEngineFire, _rightEngineFire;

    [SerializeField]
    private AudioClip _laserSoundClip;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Set the player's position to the origin
        transform.position = new Vector3(0, 0, 0);
        _shieldVisualizer.SetActive(false);

        if (!GameObject.Find("Canvas").TryGetComponent<UIManager>(out _uiManager))
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        if (!GameObject.Find("Spawn_Manager").TryGetComponent<SpawnManager>(out _spawnManager))
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (!TryGetComponent<AudioSource>(out _audioSource))
        {
            Debug.LogError("The Audio Source on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        // If the space key is pressed and the current time is greater than the next fire time
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire || Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void PlayerMovement()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set the boundaries for the player
        float VerticalBoundary = 4.0f;
        float HorizontalBoundary = 11.2f;

        // Create a vector based on the input
        Vector3 inputDirection = new(horizontalInput, verticalInput, 0);

        // Move the player based on input
        transform.Translate(_speed * Time.deltaTime * inputDirection);

        // Clamp the player's position vertically
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -VerticalBoundary, VerticalBoundary), 0);
        
        // If the player is moving vertically and pressed shift, activate the thruster
        if(_isSpeedBoostActive == false)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                _speed = 6f;
                _thruster.SetActive(true);
            }
            else
            {
                _speed = 5f;
                _thruster.SetActive(false);
            }
        }

        // Wrap the player's position horizontally
        if (transform.position.x > HorizontalBoundary)
        {
            transform.position = new Vector3(-HorizontalBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < -HorizontalBoundary)
        {
            transform.position = new Vector3(HorizontalBoundary, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        // Set the next fire time
        _nextFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        int randomIndex = Random.Range(0, 2);

        if (_isShieldsActive)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }


        _lives--;
        _uiManager.UpdateLives(_lives);
        HandleEngineDamage(randomIndex);

        if (_lives < 1)
        {
            if (_spawnManager != null)
            {
                _spawnManager.OnPlayerDeath();
            }
            Destroy(this.gameObject);
        }
    }

    private void HandleEngineDamage(int randomIndex)
    {
        if (_lives == 2)
        {
            if (randomIndex == 0)
            {
                _leftEngineFire.SetActive(true);
            }
            else
            {
                _rightEngineFire.SetActive(true);
            }
        }
        else if (_lives == 1)
        {
            _leftEngineFire.SetActive(true);
            _rightEngineFire.SetActive(true);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedBoostMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldsPowerDownRoutine());
    }

    IEnumerator ShieldsPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualizer.SetActive(false);
        _isShieldsActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
