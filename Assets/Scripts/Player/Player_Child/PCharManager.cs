using UnityEngine;

public class PCharManager : MonoBehaviour
{
    private BattleManager battleManager;
    private PlayerManager playerManager;
    private ObjectPoolManager poolManager;
    private BossManager bossManager;
    private GameManager gameManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        playerManager = transform.parent.GetComponent<PlayerManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

    }

    #region çarpýþtýðýnda düþman ölür
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") && battleManager.attackState && other.transform.parent.childCount > 1)
        {
            battleManager.KillTheRed(other.gameObject);

            playerManager.TextUpdate();

            poolManager.BlueParticleActivate(transform);
        }

        if (other.CompareTag("Boss") && gameManager.gameState)
        {
            bossManager = other.GetComponent<BossManager>();
            bossManager.BossDamage(2);
            bossManager.bossTxt.text = bossManager.bossHealth.ToString();
        }
    }
    #endregion

}

