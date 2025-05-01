using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D customCursorTexture;
    public Vector2 hotSpot = new Vector2(0, 0); 

    void Start()
    {
        // ตั้งค่า cursor เมื่อเริ่มเกม
        Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        // ป้องกันไม่ให้แสดง cursor แบบเดิมเมื่อเคอร์เซอร์อยู่ในพื้นที่เกม
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }

        // ตั้งค่า cursor ตามตำแหน่งเมาส์
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -100f; // หรือค่าติดลบมาก ๆ ให้อยู่หน้าทุกอย่าง
        transform.position = mousePos;

    }
}
