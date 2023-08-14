using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class DialogueManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject shopPanel;

    [Header("Dialogue TMP")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("NPC Dialogue Image")]
    [SerializeField] private Image imagePlaceHolder;
    [SerializeField] private Sprite dialogueImage;

    [Header("NPC Name")]
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private string npcNameText;

    [Header("Text Configurations")]
    [SerializeField] private float typingSpeed;
    [SerializeField] private GameObject nextButton;

    [Header("Dialogue Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] dialogueSentence;

    
    private bool inRange;
    private int index;

    private void Start()
    {
        GameManager.SetGameStatus(GameStatus.Dialogue);
        panel.SetActive(true);
        StartCoroutine(TypeText());
    }

    private void ResetText()
    {
        dialogueText.text = string.Empty;
        index = 0;
        panel.SetActive(false);
    }

    public void NextLine()
    {
        if(index < dialogueSentence.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeText());
        }
        else
        {
            shopPanel.SetActive(true);
            ResetText();
        }
    }

    private IEnumerator TypeText()
    {
        imagePlaceHolder.sprite = dialogueImage;
        npcName.text = npcNameText; 
        
        nextButton.SetActive(false);
        GetRandomClip();
        audioSource.Play();

        foreach (char letter in dialogueSentence[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        audioSource.Stop();
        nextButton.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = false;
            ResetText();
        }
    }

    private void GetRandomClip()
    {
        audioSource.time = Random.Range(0f, audioSource.clip.length);
    }
}
