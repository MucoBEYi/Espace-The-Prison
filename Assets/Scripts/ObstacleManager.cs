using System.Collections;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private PlayerManager playerManager;
    private BattleManager battleManager;
    private ObjectPoolManager poolManager;


    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        #region karakterimize �arparsa onu �ld�r�r
        if (collision.collider.CompareTag("blue"))
        {
            #region karakter text ve liste g�ncellemesi

            playerManager.TextUpdate();
            #endregion
            StartCoroutine(FormatStickman());

            battleManager.KillTheBlue(collision.gameObject);

            //partik�l metodu gelecek
            poolManager.BlueParticleActivate(collision.transform);

            if (playerManager.transform.childCount < 2) //e�er ��p adam kalmam��sa.
            {
                playerManager.transform.GetChild(0).gameObject.SetActive(false);
                gameManager.LoseMenuActivity(); // Kaybettin Ekran�n� a�
                print("Obstacle taraf�ndan �ld�r�ld�n");
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
