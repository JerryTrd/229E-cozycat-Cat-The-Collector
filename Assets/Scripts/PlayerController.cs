using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;  

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    [Header("Health Settings")]
    public int maxHealth = 3;   
    public int currentHealth;    
    public GameObject heartContainer;  
    public GameObject heartPrefab;     

    [Header("Audio Settings")]
    public AudioClip deathSound;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    private AudioSource audioSource;  

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    


    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;
    private Vector3 originalScale;
    private bool isWalkingSoundPlaying = false;
    private bool isInWater = false; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        originalScale = transform.localScale;

        
        currentHealth = maxHealth;

        
        UpdateHealthUI();

        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
       


        
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded)
        {
            if (!isWalkingSoundPlaying && walkSound != null)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
                isWalkingSoundPlaying = true;
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                audioSource.Stop();
                isWalkingSoundPlaying = false;
            }
        }

       
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        
        if (Input.GetButtonDown("Jump") && (isGrounded || isInWater))

        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                isWalkingSoundPlaying = false;
            }

            
            if (jumpSound != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(jumpSound);
            }
        }



        
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

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        
        if (currentHealth <= 0)
        {
            Die();  
        }

        
        UpdateHealthUI();
    }

    
    void UpdateHealthUI()
    {
        
        foreach (Transform child in heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        
        int heartsToShow = currentHealth / 1; 

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Killzone"))
        {
            TakeDamage(currentHealth);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
        }
        if (other.CompareTag("Killzone"))
        {
            TakeDamage(currentHealth); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
        }
    }

}
