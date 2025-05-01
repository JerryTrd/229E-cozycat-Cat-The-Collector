using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueWithSceneChange : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Button nextButton;

    [TextArea(2, 5)]
    public string message;
    public string sceneToLoad;

    private bool hasTriggered = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        nextButton.onClick.AddListener(GoToNextScene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            dialogueBox.SetActive(true);
            dialogueText.text = message;
            nextButton.gameObject.SetActive(true);
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene("GameWin");
    }
}
