////////////////////////////////////////////////////////////////////////////
// bl_GameManager
//
//
//                    Lovatto Studio 2016
////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.UI;

public class bl_GameManager : Singleton<bl_GameManager>
{
    [HideInInspector] public bool isPlaying = false;

    [Header("References")]
    [SerializeField] private Animator MainAnim;
    [SerializeField] private Image AudioIconImage;
    [SerializeField] private Sprite AudioOnSprite;
    [SerializeField] private Sprite AudioOffSprite;
    [SerializeField] private Animator GameOverAnim;
    [SerializeField] private GameObject PlayButton;//we need show only one time this, so will desactive after use it.
    [SerializeField] private Button[] PlayServiceButtons;
    [SerializeField] private GameObject[] ExtraDefenses;
    public Transform FloatingParent;

    [Header("Backgrounds")]
    [SerializeField] private SpriteRenderer BackgroundRender;
    [SerializeField] private Sprite[] ArrBackgroundRender;

    private bool audioOn = true;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        LoadSettings();
        foreach (Button b in PlayServiceButtons)
        {
            b.interactable = false;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            imgs[1].color = b.colors.disabledColor;
        }
        ChangeBackgroundRandom();
    }

    void ChangeBackgroundRandom()
    {
        int ran = Random.Range(0, ArrBackgroundRender.Length);
        BackgroundRender.GetComponent<SpriteRenderer>().sprite = ArrBackgroundRender[ran];

    }

    /// <summary>
    /// 
    /// </summary>
    void LoadSettings()
    {
        audioOn = bl_Utils.PlayerPrefsX.GetBool(KeyMaster.AudioEnable, true);
        AudioIconImage.sprite = (audioOn) ? AudioOnSprite : AudioOffSprite;
        AudioListener.pause = !audioOn;

        MainAnim.gameObject.SetActive(true);
        MainAnim.SetBool("show", true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Share()
    {
        StartCoroutine(bl_LovattoMobileUtils.TakeScreenShotAndShare(0));
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnGameOver()
    {
        isPlaying = false;
        GameStatus.COUNTDIE++;
        Admob.instance.ShowBanner();
        PlayButton.SetActive(false);
        MainAnim.gameObject.SetActive(true);
        MainAnim.SetBool("show", true);
        GameOverAnim.gameObject.SetActive(true);
        GameOverAnim.SetBool("show", true);
        bl_SpawnerManager.Instance.HideAll();
        bl_ScoreManager.Instance.Reset();
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayServiceLogin()
    {
        foreach (Button b in PlayServiceButtons)
        {
            b.interactable = true;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            imgs[1].color = b.colors.normalColor;
        }
    }

    /// <summary>
    /// /
    /// </summary>
    public void TryAgain()
    {
        if (isPlaying)
            return;
        Admob.instance.HideBanner();
        if ((GameStatus.COUNTDIE + 1) % 5 == 0)
        {
            Admob.instance.RequestInterstitial();
        }
        if (GameStatus.COUNTDIE % 5 == 0 && GameStatus.COUNTDIE != 0)
        {
            Admob.instance.ShowInterstitial();
        }
        bl_TimeManager.Instance.SetSlowMotion(false);
        MainAnim.SetBool("show", false);
        StartCoroutine(bl_Utils.AnimatorUtils.WaitAnimationLenghtForDesactive(MainAnim));
        GameOverAnim.SetBool("show", false);
        StartCoroutine(bl_Utils.AnimatorUtils.WaitAnimationLenghtForDesactive(GameOverAnim));
        bl_Planet.Instance.Reset();
        bl_SpawnerManager.Instance.Spawn();
        ChangeBackgroundRandom();
                bl_Planet.Instance.ChangePlayer();
        foreach (GameObject g in ExtraDefenses) { g.SetActive(true); }
        isPlaying = true;
        Admob.instance.RequestBanner();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isPlaying)
        {
            Admob.instance.HideBanner();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartGame()
    {
        isPlaying = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchAudio()
    {
        audioOn = !audioOn;
        AudioIconImage.sprite = (audioOn) ? AudioOnSprite : AudioOffSprite;
        AudioListener.pause = !audioOn;
        bl_Utils.PlayerPrefsX.SetBool(KeyMaster.AudioEnable, audioOn);
    }

    public static bl_GameManager Instance
    {
        get
        {
            return ((bl_GameManager)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }

    public void RateMe5Star()
    {
        Application.OpenURL("market://details?id=com.cotato.planet");
    }
}