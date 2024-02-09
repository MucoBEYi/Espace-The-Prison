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


    //true = oyun �al���yor
    public bool gameState, bossBattlestate, permission;         //bossManager scriptinden buraya ta��nd�.

    //oyun durdurdu�unda attack da pasif oluyor, oyunu tekrar ba�latt���nda e�er sava�ta ise tekrar aktif olmas� gerekiyor.
    public bool attackDebug;


    #region S�re de�i�kenleri   ek: kazan�nca, kalan prisoner say�s�
    private float elapsedTime = 0f;
    private int seconds = 0;
    private int minutes = 0;
    [SerializeField] TMP_Text TimeDisplayLose;
    [SerializeField] TMP_Text TimeDisplayWin;
    //karakter say�s�
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

        //start butonunu belirenen s�re sonra �a��r�r
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


    //************************************************ D�ZENLENECEK!
    public void RetryButtonClicked()
    {
        LoseMenu.SetActive(false);
        ResetAll();
        Destroy(currentLevel);
        currentLevel = Instantiate(levelManager.levels[currentLevelIndex]);

    }

    //************************************************************************************************************* D�ZENLENECEK!
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

    public void LoseMenuActivity() // Oyuncu �ld���nde Lose menusunu a�mak i�in
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

    #region yeni seviyeye ge�i�
    //yeni seviyeyi olu�turur ve �nceki seviyeyi destroy eder.
    void CreateNextLevel()
    {
        if (currentLevelIndex >= levelManager.levels.Length)
        {
            Debug.Log("hangi i�sizlikle b�t�n seviyeleri bitirdiniz bilmiyorum ama tebrikler.");
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

    //her �eyi s�f�rlar(next butona bas�ld���nda)
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

    //zaman sayac�
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
        yield return null;      //kameran�n playere gelme s�resiyle e�it yap�n!!
        StartMenu.SetActive(true);
    }


}