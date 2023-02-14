using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Error");
            }

            return _instance;
        }
    }

    public TextMeshProUGUI playerGemCountText;
    public Image selection;
    public void OpenShop(int gemCount)
    {
        playerGemCountText.SetText(gemCount.ToString());
    }
    public void UpdateShopSelection(RectTransform item)
    {
        selection.rectTransform.position = new Vector3(item.position.x + 8f, item.position.y - item.sizeDelta.y / 8, item.position.z);
    }
    private void Awake()
    {
        _instance = this;
    }
}
