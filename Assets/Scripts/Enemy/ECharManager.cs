using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECharManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private BattleManager battleManager;

    private ObjectPoolManager poolManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        enemyManager = transform.parent.GetComponent<EnemyManager>();
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();

    }

    #region düþmanlar karakterlerimizi öldürür
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("blue") && battleManager.attackState && collision.transform.parent.childCount > 1)
        {
            enemyManager.TextUpdate();

            battleManager.KillTheBlue(collision.gameObject);

            poolManager.RedParticleActivate(transform);


        }
    }
    #endregion

}
