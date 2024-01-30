using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECharManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private BattleManager battleManager;
    //kan
    [SerializeField] ParticleSystem redParticle;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        enemyManager = transform.parent.GetComponent<EnemyManager>();

    }

    #region d��manlar karakterlerimizi �ld�r�r
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("blue") && battleManager.attackState && collision.transform.parent.childCount > 1)
        {
            battleManager.KillTheBlue(collision.gameObject);

            //particle �retir
            Instantiate(redParticle, transform.position, Quaternion.identity);

            enemyManager.TextUpdate();

        }
    }
    #endregion

}
