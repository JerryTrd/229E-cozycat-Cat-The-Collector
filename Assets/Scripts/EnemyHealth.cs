using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private bool isDead = false;
    

    private Animator animator;
    private AudioSource audioSource;
    

    [SerializeField] private AudioClip hurtSound;   
    [SerializeField] private AudioClip deathSound;  

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        animator.SetTrigger("Hurt");


        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);  
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // เพิ่มคะแนนเมื่อศัตรูตาย
        ScoreManager.instance.AddScore(1);  // เพิ่ม 10 คะแนน (ปรับได้ตามต้องการ)

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject, deathSound != null ? deathSound.length : 1f);
    }

}
