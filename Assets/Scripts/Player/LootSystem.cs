using UnityEngine;

public class LootSystem : MonoBehaviour
{
    private static LootSystem _instance;
    public static LootSystem Instance
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
    private enum Items
    {
        FlameSwordButton = 0,
        BootsOfFlight = 1,
        KeyToCustle = 2
    }

    [SerializeField]
    private int FlameSwordButtonCosts, BootsOfFlightCosts, KeyToCustleCosts;

    [SerializeField]
    private int gems;
    public int Gems { get; set; }
    bool FlameSwordButton { get; set; }
    bool BootsOfFlight { get; set; }
    bool KeyToCustle { get; set; }

    private void Start()
    {
        Gems = gems;
        FlameSwordButton = false;
        BootsOfFlight = false;
        KeyToCustle = false;
    }
    public void PickUpDiamonds()
    {
        Gems += 1;
        Debug.Log(Gems);
    }

    public void BuySelectedItem(int itemId)
    {
        switch ((Items)itemId)
        {
            case Items.FlameSwordButton:
                if (!FlameSwordButton && Gems > FlameSwordButtonCosts)
                {
                    Gems -= FlameSwordButtonCosts;
                    FlameSwordButton = true;
                }
                break;
            case Items.BootsOfFlight:
                if (!BootsOfFlight && Gems > BootsOfFlightCosts)
                {
                    Gems -= BootsOfFlightCosts;
                    BootsOfFlight = true;
                }
                break;
            case Items.KeyToCustle:
                if (!KeyToCustle && Gems > KeyToCustleCosts)
                {
                    Gems -= KeyToCustleCosts;
                    KeyToCustle = true;
                }
                break;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
