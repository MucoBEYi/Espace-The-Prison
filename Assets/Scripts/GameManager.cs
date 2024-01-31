using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;


    //true = oyun �al���yor
    public bool gameState;

    //oyun durdurdu�unda attack da pasif oluyor, oyunu tekrar ba�latt���nda e�er sava�ta ise tekrar aktif olmas� gerekiyor.
    public bool attackDebug;


    public GameObject StartMenu, LoseMenu, WinMenu, StopMenuContents, StopButton;
    private void Start()
    {
        gameState = false;

    }
    #region A.� metot i�levleri i�in yorum sat�r� ekler misin, anlarken zorland�m :D
    public void StartButtonClicked()
    {
        StopButton.SetActive(true);
        StartMenu.SetActive(!StartMenu.activeSelf);
        gameState = true;
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

    public void LoseMenuActivity() // Oyuncu �ld���nde Lose menusunu a�mak i�in
    {
        StopButton.SetActive(false);
        gameState = false;
        LoseMenu.SetActive(true);
    }

    #endregion

    
    public void GameWin() //oyun bitti�inde uygulanacak i�lem
    {
        StopButton.SetActive(false);
        WinMenu.SetActive(true);
        gameState = false;
        print("oyunu KAZANDIN");

    }

}