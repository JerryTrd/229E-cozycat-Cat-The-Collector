using UnityEngine;

public class ConstantBounce : MonoBehaviour
{
    public float minBounceForce = 10f;
    public float maxBounceForce = 20f;
    public float damage = 1f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Sound Settings")]
    public AudioClip bounceSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // ต้องมี AudioSource ติดกับ object นี้
    }

    void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            float randomBounceForce = Random.Range(minBounceForce, maxBounceForce);
            rb.AddForce(Vector2.up * randomBounceForce, ForceMode2D.Impulse);

            if (audioSource != null && bounceSound != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage((int)damage);
        }
    }
}

