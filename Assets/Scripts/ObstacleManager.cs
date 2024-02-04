using System.Collections;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerManager playerManager;
    private BattleManager battleManager;
    private ObjectPoolManager poolManager;
    private SoundManager soundManager;


    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        #region karakterimize çarparsa onu öldürür
        if (collision.collider.CompareTag("blue"))
        {
            if (gameManager.gameState)
            {
                #region karakter text ve liste güncellemesi

                playerManager.TextUpdate();
                #endregion
                StartCoroutine(FormatStickman());

                battleManager.KillTheBlue(collision.gameObject);
                soundManager.BattleSound();

                //particle metodu gelecek
                poolManager.BlueParticleActivate(collision.transform);

                if (playerManager.transform.childCount < 2) //eðer çöp adam kalmamýþsa.
                {
                    playerManager.transform.GetChild(0).gameObject.SetActive(false);
                    gameManager.LoseMenuActivity(); // Kaybettin Ekranýný aç
                    print("Obstacle tarafýndan öldürüldün");
                }
            }

        }
        #endregion
    }

    IEnumerator FormatStickman()
    {
        yield return new WaitForSeconds(1.2f);
        playerManager.FormatStickMan();
    }
}
