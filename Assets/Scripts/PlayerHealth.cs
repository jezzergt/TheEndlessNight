using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _gameOverPanelGO;
    [SerializeField] private GameObject _respawnGO;

    [Header("Images")]
    [SerializeField] private Image _hp_1;
    [SerializeField] private Image _hp_2;
    [SerializeField] private Image _hp_3;

    [Header("Sprites")]
    [SerializeField] private Sprite _fullHpSprite;
    [SerializeField] private Sprite _emptyHpSprite;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _playerDialogSound;
    [SerializeField] private AudioSource _deathSound;

    [Header("Transforms")]
    [SerializeField] private Transform spawnLocation;
    
    private Rigidbody2D _rBody2D;
    private Animator _animator;

    private int _currentHealth;
    private int _maxHealth = 3;

    private void Start()
    {
        _rBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();           
        }
    }

    private void TakeDamage()
    {
        switch (_currentHealth)
        {
            case 3:
                _hp_3.sprite = _emptyHpSprite;
                break;
            case 2:
                _hp_2.sprite = _emptyHpSprite;
                break;
            case 1:
                _hp_1.sprite = _emptyHpSprite;
                break;
            default:
                break;
        }

        if (_currentHealth > 0)
        {
            _currentHealth--;
            Die();
        }
        
        if (_currentHealth == 0)
        {
            _gameOverPanelGO.SetActive(true);
            Invoke("RestartGame", 2f);
        }
    }

    private void Die()
    {
        _deathSound.Play();
        _rBody2D.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger("death");
    }

    public void HealthPotion()
    {
        switch (_currentHealth)
        {
            case 2:
                _hp_3.sprite = _fullHpSprite;
                break;
            case 1:
                _hp_2.sprite = _fullHpSprite;
                break;
            default:
                break;
        }

        if (_currentHealth == 2) 
        {
            _currentHealth++;
        }

        if (_currentHealth == 1) 
        {
            _currentHealth++;
        }
    }

    private void Respawn()
    {
        transform.position = spawnLocation.position;
        _rBody2D.bodyType = RigidbodyType2D.Dynamic;
        _animator.Play("Player_Idle");
        StartCoroutine("RespawnText");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    IEnumerator RespawnText()
    {
        _respawnGO.SetActive(true);
        _playerDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _respawnGO.SetActive(false);
    }
}
