using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [TextArea]
    [SerializeField] private string[] dialogueSentence;
    [SerializeField] private float typingSpeed;

    private bool inRange;
    private int index;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && inRange)
        {
            {
                dialogueText.text = string.Empty;

                GameManager.SetGameStatus(GameStatus.Dialogue);
                panel.SetActive(true);
                StartCoroutine(TypeText());
                inRange = false;
            }
        }
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in dialogueSentence[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ExitArea()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            dialogueText.text = string.Empty;
        }
    }
}
