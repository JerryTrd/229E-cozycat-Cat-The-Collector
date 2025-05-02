using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    private static BGMusicManager instance;

    void Awake()
    {
        // ถ้ามี instance อยู่แล้วให้ทำลาย GameObject นี้ (ไม่ให้มี BG Music ซ้ำ)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // ถ้ายังไม่มี instance, ตั้งค่า instance นี้
        instance = this;

        // ทำให้ GameObject นี้ไม่ถูกทำลายเมื่อเปลี่ยน scene
        DontDestroyOnLoad(gameObject);
    }
}
