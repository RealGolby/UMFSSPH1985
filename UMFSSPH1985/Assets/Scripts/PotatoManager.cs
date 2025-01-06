using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PotatoManager : MonoBehaviour
{
    public int PotatoCount;
    [SerializeField]  TextMeshProUGUI potatoCount;
    [Space(10)]
    public int SeedAmount;
    [SerializeField]   TextMeshProUGUI seedAmount;
    [Space(10)]
    public int Money;
    [SerializeField]   TextMeshProUGUI moneyAmount;

    public float kurz = 7f;


    public int PoorPotatoAmountMin = 1, PoorPotatoAmountMax = 3;
    public int NormalPotatoAmountMin = 3, NormalPotatoAmountMax = 5;
    public int GreatPotatoAmountMin = 5, GreatPotatoAmountMax = 7;

    public int GoldPotatoMoneyMin = 70, GoldPotatoMoneyMax = 101;




    public float PoorPotatoGrowthAvarage = 15;
    public float NormalPotatoGrowthAvarage = 22;
    public float GreatPotatoGrowthAvarage = 31;
    public float GoldPotatoGrowthAvarage = 51;

    [SerializeField]public float poorPotatoGrowthDifference = 5;
    [SerializeField]public float normalPotatoGrowthDifference = 7;
    [SerializeField]public float greatPotatoGrowthDifference = 9;
    [SerializeField]public float goldPotatoGrowthDifference = 11;





    private void Start()
    {
        SeedAmount = 15;
        
        
        /* //Possible SAVING? :think:
        List<FieldController> fc = new List<FieldController>(FindObjectsOfType<FieldController>());
        foreach (var item in fc)
        {
            
        }
        */
    }

    void Update()
    {
        potatoCount.text = "Potatoes:" + PotatoCount;
        seedAmount.text = SeedAmount.ToString();
        moneyAmount.text = "Money:" + Money;
    }
}
