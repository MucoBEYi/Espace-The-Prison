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

    }

    #region çarpýþtýðýnda düþman ölür
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("red") && battleManager.attackState && collision.transform.parent.childCount > 1)
        {
            battleManager.KillTheRed(collision.gameObject);

            battleManager.KillTheBlue(gameObject);




            playerManager.TextUpdate();

            poolManager.BlueParticleActivate(transform);
        }

        if (collision.collider.CompareTag("Boss"))
        {
            bossManager = collision.collider.GetComponent<BossManager>();
            bossManager.BossDamage(2);
            bossManager.bossTxt.text = bossManager.bossHealth.ToString();
        }
    }
    #endregion
}

