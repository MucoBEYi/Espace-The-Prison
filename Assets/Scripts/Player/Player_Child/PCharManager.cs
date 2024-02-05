using UnityEngine;

public class PCharManager : MonoBehaviour
{
    private BattleManager battleManager;
    private PlayerManager playerManager;
    private ObjectPoolManager poolManager;
    private BossManager bossManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        playerManager = transform.parent.GetComponent<PlayerManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();
        bossManager = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossManager>();
    }

    #region çarpýþtýðýnda düþman ölür
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("red") && battleManager.attackState && collision.transform.parent.childCount > 1)
        {
            battleManager.KillTheRed(collision.gameObject);
            
            if (collision.gameObject.activeInHierarchy == false)
                //düzensiz eksiltmeyi önlemek için buraya taþýndý
                battleManager.KillTheBlue(gameObject);

            playerManager.TextUpdate();

            poolManager.BlueParticleActivate(transform);
        }

        if (collision.collider.CompareTag("Boss"))
        {
            bossManager.BossDamage(5);
        }
    }
    #endregion
}

