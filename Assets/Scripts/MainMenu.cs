using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;     
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BACK()
    {
        SceneManager.LoadScene("Main menu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // จะเห็นแค่ใน editor
    }
}
