using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private TextMeshProUGUI itemTextDialogue;
    [SerializeField] private Button closeButton;
    [SerializeField] ShopManager shopManager;
    [SerializeField] private AudioSource audioSource;

    [TextArea]
    [SerializeField] private string prizeSentence;
    [TextArea]
    [SerializeField] private string noPrizeSentence;

    [SerializeField] private bool hasPrize;
    [SerializeField] private int prizeQuantity;


    private void Start()
    {
        closeButton.onClick.AddListener(CloseItemPanel);
    }

    public void OpenItemPanel()
    {
        if (hasPrize)
        {
            audioSource.Play();
            itemTextDialogue.text = prizeSentence;
            shopManager.AddCoins(prizeQuantity);
            hasPrize = false;
        }
        else
        {
            itemTextDialogue.text = noPrizeSentence;
        }

        GameManager.SetGameStatus(GameStatus.Dialogue);
        itemPanel.SetActive(true);

    }

    public void CloseItemPanel()
    {
        itemPanel.SetActive(false);
        itemTextDialogue.text = string.Empty;
        GameManager.SetGameStatus(GameStatus.Playing);
    }
}
