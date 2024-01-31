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
        #region karakterimize çarparsa onu öldürür
        if (collision.collider.CompareTag("blue"))
        {
            #region karakter text ve liste güncellemesi

            playerManager.TextUpdate();
            #endregion
            StartCoroutine(FormatStickman());

            battleManager.KillTheBlue(collision.gameObject);

            //partikýl metodu gelecek
            poolManager.BlueParticleActivate(collision.transform);

            if (playerManager.transform.childCount < 2) //eðer çöp adam kalmamýþsa.
            {
                playerManager.transform.GetChild(0).gameObject.SetActive(false);
                gameManager.LoseMenuActivity(); // Kaybettin Ekranýný aç
                print("Obstacle tarafýndan öldürüldün");
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
