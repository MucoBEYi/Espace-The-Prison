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
        #region karakterimize çarparsa onu öldürür
        if (collision.collider.CompareTag("blue"))
        {
            #region karakter text ve liste güncellemesi
            playerManager.stickmanList.Remove(collision.gameObject);
            playerManager.TextUpdate();
            #endregion
            StartCoroutine(FormatStickman());

            battleManager.KillTheBlue(collision.gameObject);
            //mavi efekt çýkar ölen karakterin üzerinde
            Instantiate(blueParticle, collision.transform.position, Quaternion.identity);

            if (playerManager.transform.childCount < 2) //eðer çöp adam kalmamýþsa.
            {
                print("Öldün");
                gameManager.LoseMenuActivity(); // Kaybettin Ekranýný aç
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
