using System.Collections;
using UnityEngine;

enum PotatoQuality { poor, normal, great, golden }

public class FieldController : MonoBehaviour
{
    [SerializeField] float potatoGrowthAvarage;

    public float GrowthTime;
    float growingTime = 0;
    float timeRemaining;

    public int GoldMoneyAmount;

    SpriteRenderer sr;

    [SerializeField] float potatoGrowthTimeMin, potatoGrowthTimeMax;

    public bool Growing;
    public bool Harvestable;
    public bool canPlant;

    [SerializeField] Sprite dirt;
    [SerializeField] Sprite seeds;
    [SerializeField] Sprite seedsGold;
    [SerializeField] Sprite[] potatoPoor, potatoNormal, potatoGreat, potatoGolden;

    [SerializeField] BoxCollider2D plantableArea;
    

    PotatoManager potatoManager;

    [SerializeField] Sprite[] potatoSprite;

    [SerializeField]public float PoorChance = 60, NormalChance = 82, GreatChance = 98, GoldenChance = 100;

    [SerializeField] GameObject dirtOverlay;

    ParticleSystem goldenParticleSystem;

    PotatoQuality potatoQuality;

    SlovacikController slovacikController;

    [SerializeField] bool watered;
    [SerializeField] bool planted;

    [SerializeField] SpriteRenderer dirtSR;
    void Start()
    {
        watered = false;
        Harvestable = false;

        potatoManager = FindObjectOfType<PotatoManager>();
        slovacikController = FindObjectOfType<SlovacikController>();
        goldenParticleSystem = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = dirt;
    }

    void Update()
    {
        if (slovacikController.selectedTool == currentTool.Seeds)
        {
            if (!Growing && !Harvestable && canPlant && !planted)
            {
                dirtOverlay.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && potatoManager.SeedAmount > 0)
                {
                    planted = true;
                    potatoManager.SeedAmount--;
                    dirtOverlay.SetActive(false);                  
                    potatoRandomizer();
                    GrowthTime = Random.Range(potatoGrowthTimeMin, potatoGrowthTimeMax);
                    if (potatoQuality == PotatoQuality.golden)
                    {
                        sr.sprite = seedsGold;
                    }
                    else
                    {
                        sr.sprite = seeds;
                    }
                    timeRemaining = GrowthTime - growingTime;
                    Debug.Log("quality: " + potatoQuality + " | time: " + GrowthTime);
                }

            }
        }
        if (watered && planted)
        {
            planted = false;
            StartCoroutine(potatoGrow());
            StartCoroutine(AddTimer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sklizen potata
        if (collision.CompareTag("Hoe"))
        {
            if (Harvestable)
            {
                potatoManager.PotatoCount++;
                sr.sprite = dirt;
                Harvestable = false;
                Growing = false;
                Debug.Log("Potato sklizene");
                goldenParticleSystem.Stop();
                growingTime = 0;
                watered = false;
                dirtSR.color = Color.white;
                switch (potatoQuality)
                {
                    case PotatoQuality.poor:
                        potatoManager.PotatoCount += (Random.Range(potatoManager.PoorPotatoAmountMin, potatoManager.PoorPotatoAmountMax));
                        break;
                    case PotatoQuality.normal:
                        potatoManager.PotatoCount += (Random.Range(potatoManager.NormalPotatoAmountMin, potatoManager.NormalPotatoAmountMax));
                        break;
                    case PotatoQuality.great:
                        potatoManager.PotatoCount += (Random.Range(potatoManager.GreatPotatoAmountMin, potatoManager.GreatPotatoAmountMin));
                        break;
                    case PotatoQuality.golden:
                        GoldMoneyAmount = Random.Range(potatoManager.GoldPotatoMoneyMin, potatoManager.GoldPotatoMoneyMax);
                        potatoManager.Money += GoldMoneyAmount;
                        Debug.Log("golden! + " + GoldMoneyAmount);
                        break;
                }
            }
        }

        plantableArea = collision.GetComponent<BoxCollider2D>();
        if (plantableArea.CompareTag("Plant"))
        {
            canPlant = true;
        }
        if (plantableArea.CompareTag("WateringCan"))
        {
            if (planted)
            {
                if (slovacikController.waterAmount > 0)
                {
                    dirtSR.color = new Color(200f/255f,250f/255f,255f/255f,255f/255f);
                    watered = true;
                    slovacikController.waterAmount--;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        plantableArea = collision.GetComponent<BoxCollider2D>();
        if (plantableArea.CompareTag("Plant"))
        {
            canPlant = false;
            dirtOverlay.SetActive(false);
        }
    }

    void potatoRandomizer()
    {
        int random = Random.Range(0, 101);
        if (random <= PoorChance && random > 0) // Poor
        {
            potatoQuality = PotatoQuality.poor;
            potatoSprite = potatoPoor;
            potatoGrowthAvarage = potatoManager.PoorPotatoGrowthAvarage;
            potatoGrowthTimeMin = potatoGrowthAvarage - potatoManager.poorPotatoGrowthDifference;
            potatoGrowthTimeMax = potatoGrowthAvarage + potatoManager.poorPotatoGrowthDifference;
        }
        else if (random <= NormalChance && random > PoorChance) // Normal
        {
            potatoQuality = PotatoQuality.normal;
            potatoSprite = potatoNormal;
            potatoGrowthAvarage = potatoManager.NormalPotatoGrowthAvarage;
            potatoGrowthTimeMin = potatoGrowthAvarage - potatoManager.normalPotatoGrowthDifference;
            potatoGrowthTimeMax = potatoGrowthAvarage + potatoManager.normalPotatoGrowthDifference;
        }
        else if (random <= GreatChance && random > NormalChance) // Great
        {
            potatoQuality = PotatoQuality.great;
            potatoSprite = potatoGreat;
            potatoGrowthAvarage = potatoManager.GreatPotatoGrowthAvarage;
            potatoGrowthTimeMin = potatoGrowthAvarage - potatoManager.greatPotatoGrowthDifference;
            potatoGrowthTimeMax = potatoGrowthAvarage + potatoManager.greatPotatoGrowthDifference;
        }
        else if (random <= GoldenChance && random > GreatChance) //Gold
        {
            goldenParticleSystem.Play();
            potatoQuality = PotatoQuality.golden;
            potatoSprite = potatoGolden;
            potatoGrowthAvarage = potatoManager.GoldPotatoGrowthAvarage;
            potatoGrowthTimeMin = potatoGrowthAvarage - potatoManager.goldPotatoGrowthDifference;
            potatoGrowthTimeMax = potatoGrowthAvarage + potatoManager.goldPotatoGrowthDifference;
        }
    }

    //potato grow
    IEnumerator potatoGrow()
    {
        //timeRemaining = GrowthTime - growingTime;

        Growing = true;

        /*if (potatoQuality == PotatoQuality.golden)
        {
            sr.sprite = seedsGold;
        }
        else
        {
            sr.sprite = seeds;
        }*/

        for (int i = 0; i < potatoSprite.Length; i++)
        {
            yield return new WaitForSeconds(GrowthTime / 4);
            sr.sprite = potatoSprite[i];
        }
        Growing = false;
        Harvestable = true;
        StopCoroutine(AddTimer());
    }
    IEnumerator AddTimer()
    {
        while (growingTime < GrowthTime)
        {
            yield return new WaitForSeconds(1);
            growingTime++;
        }

    }
}
