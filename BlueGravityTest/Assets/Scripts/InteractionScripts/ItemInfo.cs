using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [Header("Item infos")]
    [SerializeField] public int itemId;
    [SerializeField] private Text itemPrice;
    [SerializeField] public Text itemQuantity;
    [SerializeField] private GameObject shopManager;
    [SerializeField] public Sprite itemSprite;

    private void Update()
    {
        itemPrice.text = "Price: " + shopManager.GetComponent<ShopManager>().shopItems[2, itemId].ToString();
        itemQuantity.text = shopManager.GetComponent<ShopManager>().shopItems[3, itemId].ToString();
    }
}
