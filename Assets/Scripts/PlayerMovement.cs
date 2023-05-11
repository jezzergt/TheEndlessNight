using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _walkSound;

    [Header("Primitives")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private LayerMask jumpableGround;
    public bool IsFrozen = false;
    public bool HasRelic = false;

    private float _dirX = 0;
    private enum _movementState { idle, running, jumping, falling }

    private Rigidbody2D _rBody2D;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rBody2D = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");

        if (!IsFrozen)
        {
            _rBody2D.velocity = new Vector2(_dirX * _moveSpeed, _rBody2D.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _jumpSound.Play();
            _rBody2D.velocity = new Vector3(_rBody2D.velocity.x, _jumpForce);
        }

        AnimationUpdateState();
    }

    private void AnimationUpdateState()
    {
        _movementState state;

        if (IsFrozen &! HasRelic)
        {
            state = _movementState.idle;
        }
        else
        {
             if (_dirX > 0f) 
            {
                state = _movementState.running;
                _spriteRenderer.flipX = false;
            }
            else if (_dirX < 0)
            {
                state = _movementState.running;
                _spriteRenderer.flipX = true;
            }
            else
            {
                state = _movementState.idle;
            }

            if (_rBody2D.velocity.y > .1f)
            {
                state = _movementState.jumping;
            }
            else if (_rBody2D.velocity.y< - .1f)
            {
               state = _movementState.falling;
            }
        }

        if (IsFrozen && HasRelic)
        {
            _animator.Play("Player_Changing");
        }

        _animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void PlayWalkingSound()
    {
        _walkSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Freeze"))
        {
            IsFrozen = true;
            _animator.Play("Player_Idle");
            _rBody2D.bodyType = RigidbodyType2D.Static;
            Debug.Log("Frozen");
        }
    }
}
