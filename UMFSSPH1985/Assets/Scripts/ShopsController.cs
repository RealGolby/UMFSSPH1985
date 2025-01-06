using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopsController : MonoBehaviour
{
    [Header("PotatoSell")]
    [SerializeField] Slider PSslider;

    PotatoManager potatoManager;

    [SerializeField] TextMeshProUGUI PSmoneyOutput;
    [SerializeField] TextMeshProUGUI PSkurz;
    [SerializeField] TMP_InputField PSinputAmount;

    [HideInInspector]public int PSsellingAmount;



    int moneyEarned;

    [Header("SeedBuy")]
    [SerializeField] Slider SBslider;
    [SerializeField] TMP_InputField SBinputAmount;
    [SerializeField] TextMeshProUGUI SBCost;

    [HideInInspector] public int SBbuyingAmount;

    int seedBought;

    [Header("Buttons")]
    [SerializeField] GameObject shopButtons;
    [SerializeField] GameObject shopMenu;
    
    [Header("UI")]
    [SerializeField] GameObject sellPotatoesUI;
    [SerializeField] GameObject buySeedsUI;

    [SerializeField] AudioSource ClickSound;
    void Start()
    {
        
        potatoManager = FindObjectOfType<PotatoManager>();
    }

    void Update()
    {
        UpdatePotatoSellValues();
        UpdateSeedBuyValues();
    }
    //Potatoes
    void UpdatePotatoSellValues()
    {
        PSslider.maxValue = potatoManager.PotatoCount;
        PSkurz.text = "1 potato = " + potatoManager.kurz;

        moneyEarned = (int)potatoManager.kurz * PSsellingAmount;
        PSmoneyOutput.text = moneyEarned + "€";
    }
    
    public void PSsliderChangeValue()
    {
        PSsellingAmount = (int)PSslider.value;
        PSinputAmount.text = PSslider.value.ToString();
    }
    public void PSinputAmountChangeValue()
    {
        if (int.Parse(PSinputAmount.text)>potatoManager.PotatoCount)
        {
            PSinputAmount.text = potatoManager.PotatoCount.ToString();
        }
        else
        {
            PSslider.value = int.Parse(PSinputAmount.text);
        }
    }
    public void sellButton()
    {
        ClickSound.Play();
        potatoManager.PotatoCount = potatoManager.PotatoCount - PSsellingAmount;
        PSslider.value = 0;
        PSinputAmount.text = "0";
        potatoManager.Money += moneyEarned;
    }


    //UI openers
    public void openBuySeedsUI()
    {
        ClickSound.Play();
        shopMenu.SetActive(true);
        buySeedsUI.SetActive(true);
        shopButtons.SetActive(false);
    }
    public void openSellPotatoesUI()
    {
        ClickSound.Play();
        shopMenu.SetActive(true);
        sellPotatoesUI.SetActive(true);
        shopButtons.SetActive(false);
    }


    //Seeds
    void UpdateSeedBuyValues()
    {
        SBslider.maxValue = potatoManager.Money / 3;
        SBCost.text = (SBbuyingAmount*3).ToString() + "€";
    }

    public void SBinputAmountChangeValue()
    {
        if (int.Parse(SBinputAmount.text)>potatoManager.Money/3)
        {
            SBinputAmount.text = (potatoManager.Money/3).ToString();
        }
        else
        {
            SBslider.value = int.Parse(SBinputAmount.text);
        }
    }
    public void SBsliderChangeValue()
    {
        SBbuyingAmount = (int)SBslider.value;
        SBinputAmount.text = SBslider.value.ToString();
    }
    public void BuyButton()
    {
        ClickSound.Play();
        potatoManager.Money -= SBbuyingAmount*3;
        potatoManager.SeedAmount += SBbuyingAmount;
        SBinputAmount.text = "0";
        SBslider.value = 0;
    }
}
