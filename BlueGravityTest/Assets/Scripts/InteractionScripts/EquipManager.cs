using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    [Header("Button Controls")]
    [SerializeField] private Button[] equipButtons = new Button[4];
    [SerializeField] private Button[] unequipButtons = new Button[4];

    [Header("Body Parts")]
    [SerializeField] private SpriteRenderer[] bodyPartRenderers = new SpriteRenderer[4];
    [SerializeField] private Sprite[] defaultSprites = new Sprite[4];
    [SerializeField] private Sprite[] equippedSprites = new Sprite[4];

    private void Start()
    {
        SetupEquipButtons();
    }

    private void SetupEquipButtons()
    {
        for (int i = 0; i < equipButtons.Length; i++)
        {
            int index = i;
            equipButtons[i].onClick.AddListener(() => EquipPart(index));
            unequipButtons[i].onClick.AddListener(() => UnequipPart(index));
        }
    }

    public void EquipPart(int index)
    {
        if (index >= 0 && index < equippedSprites.Length)
        
            bodyPartRenderers[index].sprite = equippedSprites[index];
            UpdateBodyPartRenderer(index);
    }

    public void UnequipPart(int index)
    {
        if (index >= 0 && index < equippedSprites.Length)
        {
            bodyPartRenderers[index].sprite = defaultSprites[index];
        }
    }

    private void UpdateBodyPartRenderer(int index)
    {
        if (bodyPartRenderers[index] != null)
        {
            bodyPartRenderers[index].sprite = equippedSprites[index];
        }
    }
}
