using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool playerSpeakingFirst;

    [Header("Dialogue Objects - TMP")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject npcDialogueButton;

    [Header("Animations Controllers")]
    [SerializeField] private Animator speechBubblePlayerAnimator;
    [SerializeField] private Animator speechBubbleNpcAnimator;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialoguesSentences;
    [TextArea]
    [SerializeField] private string[] npcDialoguesSentences;

    private int playerIndex;
    private int npcIndex;
    private bool dialogueStarted;
    private float speechBubbleAnimationDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        GameManager.SetGameStatus(GameStatus.Dialogue);

        if (playerSpeakingFirst)
        {
            speechBubblePlayerAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay); 
            StartCoroutine(TypePlayerDialogue());
        }
        else
        {
            speechBubbleNpcAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypeNpcDialogue());
        }
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialoguesSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeNpcDialogue()
    {
        foreach (char letter in npcDialoguesSentences[npcIndex].ToCharArray())
        {
            npcDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        npcDialogueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        npcDialogueText.text = string.Empty;
        
        speechBubbleNpcAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        playerDialogueText.text = string.Empty;
        
        speechBubblePlayerAnimator.SetTrigger("Open");
        
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (playerIndex < playerDialoguesSentences.Length - 1)
        {
            if (dialogueStarted)
                playerIndex++;
            else
                dialogueStarted = true;

            playerDialogueText.text = string.Empty;
            StartCoroutine(TypePlayerDialogue());
        }
    }

    private IEnumerator ContinueNpcDialogue()
    {
        playerDialogueText.text = string.Empty;

        speechBubblePlayerAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        
        npcDialogueText.text = string.Empty;

        speechBubbleNpcAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (npcIndex < npcDialoguesSentences.Length - 1)
        {
            if (dialogueStarted)
                npcIndex++;
            else
                dialogueStarted = true;

            npcDialogueText.text = string.Empty;
            StartCoroutine(TypeNpcDialogue());
        }
    }

    public void ContinuePlayerButton()
    {
        npcDialogueButton.SetActive(false);

        if(playerIndex >= playerDialoguesSentences.Length -1 ) 
        {
            npcDialogueText.text = string.Empty;
            speechBubbleNpcAnimator.SetTrigger("Close");
            
            GameManager.SetGameStatus(GameStatus.Playing);
        }
        else
            StartCoroutine(ContinuePlayerDialogue());
    }

    public void ContinueNpcButton()
    {
        playerContinueButton.SetActive(false);

        if (npcIndex >= npcDialoguesSentences.Length - 1)
        {
            playerDialogueText.text = string.Empty;
            speechBubblePlayerAnimator.SetTrigger("Close");
            
            GameManager.SetGameStatus(GameStatus.Playing);
        }
        else
            StartCoroutine (ContinueNpcDialogue());
    }
}
