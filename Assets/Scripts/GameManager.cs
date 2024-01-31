using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;


    //true = oyun çalýþýyor
    public bool gameState;

    //oyun durdurduðunda attack da pasif oluyor, oyunu tekrar baþlattýðýnda eðer savaþta ise tekrar aktif olmasý gerekiyor.
    public bool attackDebug;


    public GameObject StartMenu, LoseMenu, WinMenu, StopMenuContents, StopButton;
    private void Start()
    {
        gameState = false;

    }
    #region A.Þ metot iþlevleri için yorum satýrý ekler misin, anlarken zorlandým :D
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

    public void LoseMenuActivity() // Oyuncu öldüðünde Lose menusunu açmak için
    {
        StopButton.SetActive(false);
        gameState = false;
        LoseMenu.SetActive(true);
    }

    #endregion

    
    public void GameWin() //oyun bittiðinde uygulanacak iþlem
    {
        StopButton.SetActive(false);
        WinMenu.SetActive(true);
        gameState = false;
        print("oyunu KAZANDIN");

    }

}