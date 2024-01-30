using System.Collections;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private PlayerManager playerManager;
    private BattleManager battleManager;

    [SerializeField] ParticleSystem blueParticle;


    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        #region karakterimize �arparsa onu �ld�r�r
        if (collision.collider.CompareTag("blue"))
        {
            #region karakter text ve liste g�ncellemesi
            playerManager.stickmanList.Remove(collision.gameObject);
            playerManager.TextUpdate();
            #endregion
            StartCoroutine(FormatStickman());

            battleManager.KillTheBlue(collision.gameObject);
            //mavi efekt ��kar �len karakterin �zerinde
            Instantiate(blueParticle, collision.transform.position, Quaternion.identity);

            if (playerManager.transform.childCount < 2) //e�er ��p adam kalmam��sa.
            {
                print("�ld�n");
                gameManager.LoseMenuActivity(); // Kaybettin Ekran�n� a�
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
