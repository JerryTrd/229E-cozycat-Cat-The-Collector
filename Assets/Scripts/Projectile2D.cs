using UnityEngine;
using UnityEngine.UI;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject targetMarker;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] Text eggCountText;

    private int eggCount = 0;

    void Start()
    {
        // หลีกเลี่ยงการชนระหว่างไข่ (Layer "Bullet") กับ Player (Layer "Player")
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullet"), true);
    }

    void Update()
    {
        UpdateTargetMarker();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.CompareTag("Throwable"))
            {
                // เก็บไข่
                hit.collider.gameObject.SetActive(false); // ซ่อน
                eggCount++;
                UpdateEggUI();
            }
            else if (eggCount > 0)
            {
                // ยิงไข่
                Fire();
                eggCount--;
                UpdateEggUI();
            }
        }
    }

    void Fire()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(2f, 2f, 2f);  // ปรับ Scale 
        bullet.gameObject.SetActive(true);
        bullet.isKinematic = false;
        bullet.gravityScale = 1f;

        Vector2 targetPosition = targetMarker.transform.position;
        float timeToTarget = 1f; // ใช้เวลาที่คำนวณ

        // คำนวณความเร็วของโปรเจกไทล์
        Vector2 velocity = CalculateProjectileVelocity(shootPoint.position, targetPosition, timeToTarget);
        bullet.velocity = velocity;
    }

    void UpdateTargetMarker()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetMarker.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        // คำนวณความเร็วในแนวนอน (X)
        float velocityX = distance.x / time;

        // คำนวณความเร็วในแนวดิ่ง (Y) ด้วยการพิจารณาแรงโน้มถ่วง
        float velocityY = (distance.y + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time * time) / time;

        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);

        return projectileVelocity;
    }

    void UpdateEggUI()
    {
        if (eggCountText != null)
            eggCountText.text = "Eggs: " + eggCount.ToString();
    }
}
