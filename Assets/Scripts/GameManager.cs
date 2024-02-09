using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    private SoundManager soundManager;

    private ObjectPoolManager poolManager;


    //true = oyun çalýþýyor
    public bool gameState, bossBattlestate, permission;         //bossManager scriptinden buraya taþýndý.

    //oyun durdurduðunda attack da pasif oluyor, oyunu tekrar baþlattýðýnda eðer savaþta ise tekrar aktif olmasý gerekiyor.
    public bool attackDebug;


    #region Süre deðiþkenleri   ek: kazanýnca, kalan prisoner sayýsý
    private float elapsedTime = 0f;
    private int seconds = 0;
    private int minutes = 0;
    [SerializeField] TMP_Text TimeDisplayLose;
    [SerializeField] TMP_Text TimeDisplayWin;
    //karakter sayýsý
    [SerializeField] Transform player;
    [SerializeField] TMP_Text prisonerCountTxt;
    #endregion



    #region Level Test
    [SerializeField] private LevelManager levelManager;
    private int currentLevelIndex;
    private GameObject currentLevel;
    #endregion



    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel", currentLevelIndex);

        currentLevel = Instantiate(levelManager.levels[currentLevelIndex]);
    }



    public GameObject StartMenu, LoseMenu, WinMenu, StopMenuContents, StopButton, settings;
    private void Start()
    {
        gameState = false;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();

        //start butonunu belirenen süre sonra çaðýrýr
        StartCoroutine(StartButtonActive());
    }

    private void Update()
    {
        if (gameState)
            TimeCounter();
    }


    #region butonlar
    public void StartButtonClicked()
    {
        StopButton.SetActive(true);
        gameState = true;
        battleManager.attackState = attackDebug;
        bossBattlestate = false;
        StartMenu.SetActive(!StartMenu.activeSelf);

    }

    public void SettingsOpen()
    {
        StopMenuContents.SetActive(false);
        settings.SetActive(true);
    }

    public void RestartGame()
    {
        currentLevelIndex = 0;
        Time.timeScale = 1;
        StopMenuContents.SetActive(false);
        ResetAll();
        Destroy(currentLevel);
        currentLevel = Instantiate(levelManager.levels[currentLevelIndex]);

    }

    public void SettingsClose()
    {
        settings.SetActive(false);
        StopMenuContents.SetActive(true);
        soundManager.SaveSound(soundManager.soundVolume);
        soundManager.SaveSong(soundManager.songVolume);
        PlayerPrefs.Save();
    }


    //************************************************ DÜZENLENECEK!
    public void RetryButtonClicked()
    {
        LoseMenu.SetActive(false);
        ResetAll();
        Destroy(currentLevel);
        currentLevel = Instantiate(levelManager.levels[currentLevelIndex]);

    }

    //************************************************************************************************************* DÜZENLENECEK!
    public void WinButtonClicked()
    {
        ResetAll();
        CreateNextLevel();

    }
    public void StopButtonClicked()
    {
        //bunu kullanmaktan nefret ediyorum!
        Time.timeScale = 0f;

        StopButton.SetActive(false);
        attackDebug = battleManager.attackState;
        gameState = false;
        battleManager.attackState = false;
        StopMenuContents.SetActive(true);

    }
    public void StopMenuButtonClicked()
    {
        Time.timeScale = 1;
        StopMenuContents.SetActive(false);
        gameState = true;
        battleManager.attackState = attackDebug;
        StopButton.SetActive(true);
    }

    public void LoseMenuActivity() // Oyuncu öldüðünde Lose menusunu açmak için
    {
        TimeDisplayLose.text = (minutes.ToString("00") + ":" + seconds.ToString("00"));
        StopButton.SetActive(false);
        gameState = false;
        LoseMenu.SetActive(true);
    }

    #endregion


    public IEnumerator GameWin()
    {
        TimeDisplayWin.text = (minutes.ToString("00") + ":" + seconds.ToString("00"));
        prisonerCountTxt.text = "Survivors: " + (player.childCount - 1).ToString();
        gameState = false;
        yield return new WaitForSeconds(0.5f);
        StopButton.SetActive(false);
        WinMenu.SetActive(true);
        soundManager.WinSound();
        print("oyunu KAZANDIN");
    }

    #region yeni seviyeye geçiþ
    //yeni seviyeyi oluþturur ve önceki seviyeyi destroy eder.
    void CreateNextLevel()
    {
        if (currentLevelIndex >= levelManager.levels.Length)
        {
            Debug.Log("hangi iþsizlikle bütün seviyeleri bitirdiniz bilmiyorum ama tebrikler.");
            return;
        }

        Destroy(currentLevel);

        currentLevelIndex++;

        if (currentLevelIndex < levelManager.levels.Length)
        {
            currentLevel = Instantiate(levelManager.levels[currentLevelIndex]);
        }
        else
            print("bi bok oldu ama bende bilmiyorum");

    }

    //her þeyi sýfýrlar(next butona basýldýðýnda)
    void ResetAll()
    {
        WinMenu.SetActive(false);
        StartCoroutine(StartButtonActive());
        while (player.childCount > 1)
            poolManager.GiveBlueStickman(player.GetChild(1).gameObject);
        poolManager.GetBlueStickman();
        player.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = (player.childCount - 1).ToString();
        player.position = new Vector3(0, 0.5f, 0);
        player.GetChild(0).gameObject.SetActive(true);
        player.GetChild(0).position = new Vector3(player.position.x, player.GetChild(0).position.y, player.position.z);
        permission = false;
        gameState = false;
        Time.timeScale = 1;
        minutes = 0;
        PlayerPrefs.SetInt("CurrentLevel", currentLevelIndex);
        PlayerPrefs.Save();
    }

    #endregion

    //zaman sayacý
    private void TimeCounter()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            seconds++;

            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }
        }
    }


    IEnumerator StartButtonActive()
    {
        yield return null;      //kameranýn playere gelme süresiyle eþit yapýn!!
        StartMenu.SetActive(true);
    }


}