using UnityEngine;

public class ConstantBounce : MonoBehaviour
{
    public float minBounceForce = 10f;
    public float maxBounceForce = 20f;
    public float damage = 1f;  // ปริมาณดาเมจที่ทำ
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            float randomBounceForce = Random.Range(minBounceForce, maxBounceForce);
            rb.AddForce(Vector2.up * randomBounceForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // ตรวจจับการชน
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage((int)damage);  // แปลง float เป็น int ถ้า damage เป็น float
        }
    }


}
