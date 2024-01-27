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
        enemyManager = transform.parent.GetComponent<EnemyManager>();

        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue") && battleManager.attackState && other.transform.parent.childCount > 0)
        {
            enemyManager.TextUpdate();
            battleManager.KillTheALL(other.gameObject);
            Instantiate(redParticle, transform.position, Quaternion.identity);
        }
    }
}
