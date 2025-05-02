using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 10;
    private AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shootSound;  
    [SerializeField] private AudioClip hitEnemySound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  
            }

            // เล่นเสียงเมื่อกระสุนชนกับศัตรู
            if (audioSource != null && hitEnemySound != null)
            {
                audioSource.PlayOneShot(hitEnemySound);  
            }
        }

        Destroy(gameObject);
    }
}

