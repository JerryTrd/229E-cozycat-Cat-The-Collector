using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    [Header("Health Settings")]
    public int maxHealth = 3;   // พลังชีวิตสูงสุด
    public int currentHealth;     // พลังชีวิตปัจจุบัน
    public GameObject heartContainer;  // 
    public GameObject heartPrefab;     // 

    [Header("Audio Settings")]
    public AudioClip deathSound;   // เสียงเมื่อเสียชีวิต
    private AudioSource audioSource;  // 

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        // ตั้งค่าพลังชีวิตเริ่มต้น
        currentHealth = maxHealth;

        // แสดงหัวใจ
        UpdateHealthUI();

        // เตรียมการเล่นเสียง
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip sprite
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Animator updates
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    void FixedUpdate()
    {
        // ตรวจสถานะอยู่บนพื้นก่อนปรับความเร็ว
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ใช้เฉพาะแกน X ไม่ทับค่า Y ที่ได้จาก Effector
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    // ฟังก์ชันลดพลังชีวิต
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // ตรวจเช็กพลังชีวิต
        if (currentHealth <= 0)
        {
            Die();  // เมื่อพลังชีวิตหมด
        }

        // อัปเดตหัวใจ
        UpdateHealthUI();
    }

    // ฟังก์ชันแสดงหัวใจ
    void UpdateHealthUI()
    {
        // ลบหัวใจทั้งหมดก่อน
        foreach (Transform child in heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // สร้างหัวใจใหม่ตามพลังชีวิต
        int heartsToShow = currentHealth / 1; // หัวใจ 1 ดวง 

        for (int i = 0; i < heartsToShow; i++)
        {
            Instantiate(heartPrefab, heartContainer.transform);
        }
    }

    // ฟังก์ชันเมื่อเสียชีวิต
    void Die()
    {
        // เล่นเสียงตาย
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        
        Debug.Log("Player is dead");
        {
            SceneManager.LoadScene("GameOver");
        }
        
         
    }
}
