using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    private SoundManager soundManager;

    //true = oyun çalýþýyor
    public bool gameState;

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



    public GameObject StartMenu, LoseMenu, WinMenu, StopMenuContents, StopButton, settings;
    private void Start()
    {
        gameState = false;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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
        StartMenu.SetActive(!StartMenu.activeSelf);
        gameState = true;
    }

    public void SettingsOpen()
    {
        StopMenuContents.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsClose()
    {
        settings.SetActive(false);
        StopMenuContents.SetActive(true);
        soundManager.SaveSound(soundManager.soundVolume);
        soundManager.SaveSong(soundManager.songVolume);
        PlayerPrefs.Save();
    }

    public void RetryButtonClicked()
    {
        gameState = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StopButton.SetActive(true);
    }
    public void WinButtonClicked()
    {
        gameState = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StopButtonClicked()
    {
        StopButton.SetActive(false);
        attackDebug = battleManager.attackState;
        gameState = false;
        battleManager.attackState = false;
        StopMenuContents.SetActive(true);

    }
    public void StopMenuButtonClicked()
    {
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
        yield return new WaitForSeconds(2f);
        StopButton.SetActive(false);
        WinMenu.SetActive(true);
        soundManager.WinSound();
        print("oyunu KAZANDIN");
    }

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
        yield return new WaitForSecondsRealtime(4f);      //kameranýn playere gelme süresiyle eþit yapýn!!
        StartMenu.SetActive(true);
    }

}