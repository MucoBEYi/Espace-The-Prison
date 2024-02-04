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
        #region karakterimize �arparsa onu �ld�r�r
        if (collision.collider.CompareTag("blue"))
        {
            if (gameManager.gameState)
            {
                #region karakter text ve liste g�ncellemesi

                playerManager.TextUpdate();
                #endregion
                StartCoroutine(FormatStickman());

                battleManager.KillTheBlue(collision.gameObject);
                soundManager.BattleSound();

                //particle metodu gelecek
                poolManager.BlueParticleActivate(collision.transform);

                if (playerManager.transform.childCount < 2) //e�er ��p adam kalmam��sa.
                {
                    playerManager.transform.GetChild(0).gameObject.SetActive(false);
                    gameManager.LoseMenuActivity(); // Kaybettin Ekran�n� a�
                    print("Obstacle taraf�ndan �ld�r�ld�n");
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
