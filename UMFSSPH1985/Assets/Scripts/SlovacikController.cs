using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public enum currentTool { Hoe,WateringCan,Seeds}

enum PlayerState { move, swing, idle}

public class SlovacikController : MonoBehaviour
{
    [SerializeField] GameObject ticketMenu;

    GolbyController golbyController;
    PotatoManager potatoManager;

    [SerializeField] Volume renderingVolume;
    ColorAdjustments colorAdjustments;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    MusicController musicController;
    [SerializeField] GameObject crazyFlowerE;
    [SerializeField] GameObject crazyFlower;
    bool isInCrazyFlowerArea, isInBebeArea, isInPieceArea;
    bool flowerEaten;

    [SerializeField] GameObject bebeE;
    [SerializeField] GameObject Bebe;
    [SerializeField] AudioSource BebeSound;
    int potatoesDonated;

    bool firstMetro;

    bool firstPiece;


    bool crazyFlowerMsg,bebeMsg, metroMsg;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float sprintSpeed = 6f;
    float walk;
    float hWalk;
    float vWalk;
    SpriteRenderer sr;
    bool faceRight;

    [SerializeField]PlayerState currentState;

    [SerializeField] Texture2D mouseCursor;

    [SerializeField] GameObject plantBoxRight;
    [SerializeField] GameObject plantBoxLeft;

    Animator slovakAnim;

    [SerializeField]MetroController metroController;

    bool isInAnimation;

    [SerializeField] Image metroFade;

    [SerializeField] GameObject wellE;
    bool wellArea;

    bool isInPotatoShopArea;
    [SerializeField] GameObject potatoShopE;
    bool isInCdShopArea;
    [SerializeField] GameObject cdShopE;

    [Header("UIs")]
    [SerializeField] GameObject potatoShopUI;
    [SerializeField] GameObject shopButtons;
    [SerializeField] GameObject shopMenus;
    [SerializeField] GameObject sellPotatoesUI;
    [SerializeField] GameObject buySeedsUI;

    [SerializeField] GameObject cdShopUI;

    [Header("Hoe")]
    [Header("Tools")]
    [SerializeField] GameObject HoeGO;
    [SerializeField] BoxCollider2D hoeCollider2D;
    [SerializeField] SpriteRenderer hoeSR;
    [SerializeField] Animator hoeAnimator;
    [Header("WateringCan")]
    [SerializeField] GameObject WateringCanGO;
    [SerializeField] BoxCollider2D wateringcanCollider2D;
    [SerializeField] SpriteRenderer wateringcanSR;
    [Range(0,5)]public int waterAmount;
    [SerializeField] Slider waterSlider;
    [SerializeField] AudioSource waterFillSound;
    [Header("SeedsBag")]
    [SerializeField] GameObject SeedsBagGO;
    [SerializeField] BoxCollider2D seedsbagCollider2D;
    [SerializeField] SpriteRenderer seedsbagSR;

    [Header("Hotbar")]
    [SerializeField] RawImage Slot1;
    [SerializeField] RawImage Slot2;
    [SerializeField] RawImage Slot3;

    public currentTool selectedTool;

    public GameObject currentPiece;
    public int PiecesFound;
    void Start()
    {
        firstPiece = true;
        firstMetro = true;   
        potatoManager = FindObjectOfType<PotatoManager>();
        golbyController = FindObjectOfType<GolbyController>();
        musicController = FindObjectOfType<MusicController>();
        Cursor.visible = false;
        Cursor.SetCursor(mouseCursor, Vector2.zero,CursorMode.ForceSoftware);
        faceRight = true;
        plantBoxLeft.SetActive(false);
        Cursor.visible = true;
        slovakAnim = GetComponent<Animator>();
        hoeCollider2D.enabled = false;
        currentState = PlayerState.idle;
        sr = GetComponent<SpriteRenderer>();

        selectedTool = currentTool.Hoe;
        Slot1.color = new Color(1, 1, 0.7f);
    }

    void FixedUpdate()
    {
        //Debug.Log("hWalk: "+ hWalk);
        //Debug.Log("vWalk"+vWalk);
        if (currentState != PlayerState.swing && !isInAnimation && !golbyController.isGolbyTalking)
        {
            Move();
        }

    }

    private void Update()
    {
        OpenShop();
        Well();
        SwitchSlot();
        StartCoroutine(SwingHoe());
        CrazyFlower();
        slovacikBebe();
        MusicPiece();
    }

    void MusicPiece()
    {
        if (PiecesFound == 5)
        {
            PiecesFound++;
            musicController.addMusic(5);
            musicController.PlayMusic(5);
        }
        if (isInPieceArea && Input.GetKeyDown(KeyCode.E) && !golbyController.isGolbyTalking && currentPiece!= null)
        {
            PiecesFound++;
            Destroy(currentPiece);
        }
    }

    void slovacikBebe()
    {
        if (isInBebeArea && Input.GetKeyDown(KeyCode.E) && potatoesDonated != 69 && !golbyController.isGolbyTalking && potatoManager.PotatoCount >0)
        {
            BebeSound.Play();
            potatoManager.PotatoCount--;
            potatoesDonated++;
        }
        if (potatoesDonated == 69)
        {
            musicController.addMusic(2);
            musicController.PlayMusic(2);
            Bebe.SetActive(false);
            bebeE.SetActive(false);
            potatoesDonated++;
        }
    }

    void CrazyFlower()
    {
        if (isInCrazyFlowerArea && Input.GetKeyDown(KeyCode.E) && !flowerEaten && !golbyController.isGolbyTalking)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            StartCoroutine(crazyFlowerEffect());
            crazyFlower.SetActive(false);
            musicController.addMusic(4);
            musicController.PlayMusic(4);
        }
    }

    void OpenShop()
    {
        if (isInPotatoShopArea && Input.GetKeyDown(KeyCode.E))
        {
            potatoShopUI.SetActive(true);
            potatoShopE.SetActive(false);
        }

        if (isInCdShopArea && Input.GetKeyDown(KeyCode.E))
        {
            cdShopUI.SetActive(true);
            cdShopE.SetActive(false);
        }
    }

    void Well()
    {
        if (wellArea && selectedTool == currentTool.WateringCan && waterAmount != 5)
        {
            wellE.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && selectedTool == currentTool.WateringCan && wellArea)
        {
            if (waterAmount != 5)
            {
                waterFillSound.Play();
                waterAmount = 5;
                wellE.SetActive(false);
                //waterFillSound.Play();
            }
        }

        waterSlider.value = waterAmount;
    }

    void SwitchSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentState != PlayerState.swing )
        {
            disableTools();
            HoeGO.SetActive(true);
            selectedTool = currentTool.Hoe;
            ResetSlot();
            Slot1.color = new Color(1, 1, 0.7f);
            wellE.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentState != PlayerState.swing)
        {
            disableTools();
            WateringCanGO.SetActive(true);
            selectedTool = currentTool.WateringCan;
            ResetSlot();
            Slot2.color = new Color(1, 1, 0.7f);
            if (wellArea && waterAmount != 5)
            {
                wellE.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentState != PlayerState.swing)
        {
            disableTools();
            SeedsBagGO.SetActive(true);
            selectedTool = currentTool.Seeds;
            ResetSlot();
            Slot3.color = new Color(1, 1, 0.7f);
            wellE.SetActive(false);
        }
    }
    void disableTools()
    {
        HoeGO.SetActive(false);
        WateringCanGO.SetActive(false);
        SeedsBagGO.SetActive(false);
    }
    void ResetSlot()
    {
        Slot1.color = new Color(1,1,1);
        Slot2.color = new Color(1,1,1);
        Slot3.color = new Color(1,1,1);
    }

    //hoe controlls
    IEnumerator SwingHoe()
    {
        if (selectedTool == currentTool.Hoe)
        {
            if (currentState!=PlayerState.swing)
            {
                if (Input.GetKeyDown(KeyCode.Space) && faceRight)
                {
                    slovakAnim.SetBool("Walking", false);
                    hoeCollider2D.enabled = true;
                    hoeAnimator.SetBool("SwingRight", true);
                    yield return null;
                    hoeAnimator.SetBool("SwingRight", false);
                    currentState = PlayerState.swing;
                    yield return new WaitForSeconds(.5f);
                    currentState = PlayerState.idle;
                    hoeCollider2D.enabled = false;
                }
                if (Input.GetKeyDown(KeyCode.Space) && !faceRight)
                {
                    slovakAnim.SetBool("Walking", false);
                    hoeCollider2D.enabled = true;
                    hoeAnimator.SetBool("SwingLeft", true);
                    yield return null;
                    hoeAnimator.SetBool("SwingLeft", false);
                    currentState = PlayerState.swing;
                    yield return new WaitForSeconds(.5f);
                    currentState = PlayerState.idle;
                    hoeCollider2D.enabled = false;
                }
            }
        }
    }


    void Move()
    {
        hWalk = Input.GetAxisRaw("Horizontal");
        vWalk = Input.GetAxisRaw("Vertical");
        if (hWalk < 0) //walk left
        {
            plantBoxLeft.SetActive(true);
            plantBoxRight.SetActive(false);
            hoeSR.flipX = true;
            WateringCanGO.transform.localPosition = new Vector3(-.2f,-.15f);
            wateringcanSR.flipX = false;
            SeedsBagGO.transform.localPosition = new Vector2(-.2f,0);
            seedsbagSR.flipX = true;
            faceRight = false;
            sr.flipX = true;
        }
        if (hWalk > 0) // walk right
        {
            plantBoxLeft.SetActive(false);
            plantBoxRight.SetActive(true);
            hoeSR.flipX = false;
            WateringCanGO.transform.localPosition = new Vector3(.2f, -.15f);
            wateringcanSR.flipX = true;
            SeedsBagGO.transform.localPosition = new Vector2(.2f, 0);
            seedsbagSR.flipX = false;
            faceRight = true;
            sr.flipX = false;
        }
        Vector3 movement = new Vector3(hWalk * walk * Time.fixedDeltaTime, vWalk * walk * Time.fixedDeltaTime, 0);
        if (hWalk ==0 && vWalk == 0)
        {
            currentState = PlayerState.idle;
            slovakAnim.SetBool("Walking", false);
        }
        else
        {
            slovakAnim.SetBool("Walking",true);
            currentState = PlayerState.move;
        }
        transform.Translate(movement);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            slovakAnim.speed = 2f;
            walk = sprintSpeed;
        }
        else
        {
            slovakAnim.speed = 1f;
            walk = walkSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PotatoShopArea")
        {
            potatoShopUI.SetActive(false);
            shopButtons.SetActive(true);
            shopMenus.SetActive(false);
            sellPotatoesUI.SetActive(false);
            buySeedsUI.SetActive(false);
            isInPotatoShopArea = false;
            potatoShopE.SetActive(false);
        }
        if (collision.name == "CDShopArea")
        {
            cdShopUI.SetActive(false);
            isInCdShopArea = false;
            cdShopE.SetActive(false);
        }

        if (collision.name=="WaterArea")
        {
            wellE.SetActive(false);
            wellArea = false;
        }
        if (collision.name == "CrazyFlower")
        {
            isInCrazyFlowerArea = false;
            crazyFlowerE.SetActive(false);
        }
        if (collision.name == "SlovacikBebe")
        {
            bebeE.SetActive(false);
            isInBebeArea = false;
        }
        if (collision.name == "TicketBuyArea")
        {
            ticketMenu.SetActive(false);
        }
        if (collision.tag == "MusicDiskPiece")
        {
            collision.transform.GetChild(0).gameObject.SetActive(false);
            isInPieceArea = false;
            currentPiece = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "MusicDiskPiece")
        {
            currentPiece = collision.gameObject;
            collision.transform.GetChild(0).gameObject.SetActive(true);
            if (firstPiece)
            {
                firstPiece = false;
                StartCoroutine(golbyController.MusicPiece(golbyController.MusicPieceMsg));
                currentState = PlayerState.idle;
                slovakAnim.SetBool("Walking", false);
            }
            isInPieceArea = true;
        }

        if (collision.name == "TicketBuyArea")
        {
            ticketMenu.SetActive(true);
        }

        if (collision.name == "BebeGolbyTrigger")
        {
            if (!bebeMsg && potatoesDonated!=69)
            {
                StartCoroutine(golbyController.Bebe(golbyController.BebeMsg));
                bebeMsg = true;
                currentState = PlayerState.idle;
                slovakAnim.SetBool("Walking", false);
            }
        }

        if (collision.name == "SlovacikBebe")
        {
            bebeE.SetActive(true);
            isInBebeArea = true;
        }

        if (collision.name == "CrazyFlower" && !flowerEaten)
        {
            crazyFlowerE.SetActive(true);
            isInCrazyFlowerArea = true;
            if (!crazyFlowerMsg)
            {
                currentState = PlayerState.idle;
                slovakAnim.SetBool("Walking", false);
                StartCoroutine(golbyController.CrazyFlower(golbyController.CrazyFlowerMsg));
                crazyFlowerMsg = true;
            }

        }

        if (collision.name== "WaterArea")
        {
            wellArea = true;
        }

        if (collision.name =="PotatoShopArea")
        {
            isInPotatoShopArea = true;
            potatoShopE.SetActive(true);
        }
        if (collision.name == "CDShopArea")
        {
            isInCdShopArea = true;
            cdShopE.SetActive(true);
        }


        if (collision.name == "MetroEntrance")
        {
            if (firstMetro)
            {
                StartCoroutine(golbyController.Metro(golbyController.MetroMsg));
                firstMetro = false;
            }
            else
            {
                StartCoroutine(metroTeleport(metroController.isInMetro));
            }

        }
        else if (collision.name == "MetroExit")
        {

            StartCoroutine(metroTeleport(metroController.isInMetro));
        }
    }

    IEnumerator metroTeleport(bool isInMetro)
    {
        slovakAnim.SetBool("Walking", false);
        isInAnimation = true;
        metroController.metroFadeIn();
        yield return new WaitForSeconds(1f);
        metroFade.color = new Color(0,0,0,1);
        if (!isInMetro)
        {
            transform.position = new Vector3(-181, 0, 0);
            metroController.isInMetro = true;
        }
        else
        {
            transform.position = new Vector3(-20, -5, 0);
            metroController.isInMetro = false;
        }
        yield return new WaitForSeconds(.5f);
        metroController.metroFadeOut();
        yield return new WaitForSeconds(.6f);
        metroFade.color = new Color(0, 0, 0, 0);
        isInAnimation = false;

    }


    IEnumerator crazyFlowerEffect()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = audioClips[1];
        audioSource.Play();
        if (renderingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            float duration = 5f;
            float currentTime = 0f;
            while (currentTime < duration)
            {
                colorAdjustments.hueShift.value = Mathf.Lerp(0, 180, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(85f);
            yield return crazyFlowerEffectOut();
        }

    }
    IEnumerator crazyFlowerEffectOut()
    {
        if (renderingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            float duration = 2f;
            float currentTime = 0f;
            while (currentTime < duration)
            {
                colorAdjustments.hueShift.value = Mathf.Lerp(180, 0, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
            colorAdjustments.hueShift.value = 0;
        }
    }
}
