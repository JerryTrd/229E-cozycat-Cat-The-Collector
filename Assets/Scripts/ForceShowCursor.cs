using UnityEngine;

public class ForceShowCursor : MonoBehaviour
{
    void LateUpdate() // ใช้ LateUpdate เพื่อให้สั่งหลังทุกสคริปต์อื่น
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
