using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    [TextArea(2, 5)]
    public string message;
    public float displayTime = 3f;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            dialogueText.text = message;
            StartCoroutine(HideDialogueAfterTime());
        }
    }

    IEnumerator HideDialogueAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        dialogueBox.SetActive(false);
    }
}
