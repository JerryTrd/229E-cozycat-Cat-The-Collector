using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Target ที่กล้องจะตาม
    public float smoothSpeed = 0.125f;
    public Vector3 offset;    // ระยะห่างของกล้องจาก target

    void LateUpdate()
    {
        // ตรวจสอบว่า target ยังไม่เป็น null หรือถูกทำลาย
        if (target != null)
        {
            // คำนวณตำแหน่งใหม่ของกล้อง
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            Debug.LogWarning("Target is null or destroyed, camera will not follow.");
        }
    }
}
