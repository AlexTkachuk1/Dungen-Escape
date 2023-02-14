using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel;
    private int currentSelectionId = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(true);
            LootSystem playerLootSystem = other.GetComponent<LootSystem>();
            var playerGems = playerLootSystem.Gems;
            UIManager.Instance.OpenShop(playerGems);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }
    public void SelectItem(RectTransform item)
    {
        UIManager.Instance.UpdateShopSelection(item);
    }
    public void GetCurrentSelectionId(int itemSelected)
    {
        currentSelectionId = itemSelected;
    }
    public void BuySelectedItem()
    {
        LootSystem.Instance.BuySelectedItem(currentSelectionId);
    }
}
