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

        
        ScoreManager.instance.AddScore(1);

       
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        
        Destroy(gameObject, 0.1f);  
    }


}
