using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource _collectSound;
    
    private PlayerHealth _playerHealth;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HPotion"))
        {
            _collectSound.Play();
            Destroy(collision.gameObject);
            _playerHealth.HealthPotion();
        }

        if (collision.gameObject.CompareTag("Key"))
        { 
            _collectSound.Play();
            _playerInventory.HasKey = true;
            Destroy(collision.gameObject);
        }
    }
}
