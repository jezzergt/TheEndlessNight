using UnityEngine;

public class SpritHealth : MonoBehaviour
{
    [Header("Box Collider")]
    [SerializeField] private BoxCollider2D _nonTriggerCollider;

    [Header("Audio Source")]
    [SerializeField] private AudioSource _deathSound;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _nonTriggerCollider.enabled = false;
            Die();
        }
    }

    private void Die()
    {
        _deathSound.Play();
        _animator.SetTrigger("death");
        Invoke("DelayedDestroy", 3f);
    }

    private void DelayedDestroy()
    {
        Destroy(gameObject);
    }
}
