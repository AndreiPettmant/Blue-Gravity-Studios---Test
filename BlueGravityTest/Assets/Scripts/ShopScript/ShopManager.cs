using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Objects")]
    [SerializeField] public int[,] shopItems = new int[5, 5];
    [SerializeField] public Text shopCoinsText;
    [SerializeField] private GameObject shop;
    [SerializeField] private Button[] buyButtons;
    [SerializeField] private Button[] sellButtons;

    [Header("Item Infos")]
    [SerializeField] private int[] itemIds = {};
    [SerializeField] private int[] prices = {};
    [SerializeField] private int[] quantities = {};


    public int totalCoins = 0;
    private float shopCoins;

    private void SetupShopItems()
    {
        for (int i = 0; i < itemIds.Length; i++)
        {
            shopItems[1, itemIds[i]] = itemIds[i];
            shopItems[2, itemIds[i]] = prices[i];
            shopItems[3, itemIds[i]] = quantities[i];
        }
    }

    private void Start()
    {
        shopCoinsText.text = "Coins: " + shopCoins.ToString();

        foreach (Button button in buyButtons)
        {
            button.interactable = false;
        }
        foreach (Button button in sellButtons)
        {
            button.interactable = false;
        }

        SetupShopItems();
    }

    public void BuyItem()
    {
        GameObject buyButton = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (totalCoins >= shopItems[2, buyButton.GetComponent<ItemInfo>().itemId] && shopItems[3, buyButton.GetComponent<ItemInfo>().itemId] >= 1)
        {
            totalCoins -= shopItems[2, buyButton.GetComponent<ItemInfo>().itemId];
            shopItems[3, buyButton.GetComponent<ItemInfo>().itemId]--;;
            shopCoinsText.text = "Coins: " + totalCoins.ToString();
            buyButton.GetComponent<ItemInfo>().itemQuantity.text = buyButton.GetComponent<ItemInfo>().itemId.ToString();
        }

        CheckButton();
    }

    public void SellItem()
    {
        GameObject sellButton = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (shopItems[3, sellButton.GetComponent<ItemInfo>().itemId] == 0)
        {
            totalCoins += shopItems[2, sellButton.GetComponent<ItemInfo>().itemId];
            shopItems[3, sellButton.GetComponent<ItemInfo>().itemId]++;
            shopCoinsText.text = "Coins: " + totalCoins.ToString();
            sellButton.GetComponent<ItemInfo>().itemQuantity.text = sellButton.GetComponent<ItemInfo>().itemId.ToString();
        }
    }

    public void CloseShop()
    {
        shop.SetActive(false);
        GameManager.SetGameStatus(GameStatus.Playing);
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        GameManager.SetGameStatus(GameStatus.Dialogue);
    }

    public void AddCoins(int coinsQuantity)
    {
        totalCoins += coinsQuantity;
        shopCoinsText.text = "Coins: " + totalCoins.ToString();
        CheckButton();
    }

    private void CheckButton()
    {
        for (int i = 0; i < buyButtons.Length; i++)
        {
            int itemPrice = prices[i];  // Obtém o preço do item atual
            ItemInfo itemInfoComponent = buyButtons[i].GetComponent<ItemInfo>();
            Text itemQuantityText = itemInfoComponent.itemQuantity;

            if (totalCoins >= itemPrice && itemQuantityText.text == "1")
            {
                buyButtons[i].interactable = true;
            }
        }
    }
}

