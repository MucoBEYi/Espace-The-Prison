using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PCharManager : MonoBehaviour
{
    private BattleManager battleManager;
    private PlayerManager playerManager;
    private ObjectPoolManager poolManager;

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

            poolManager.BlueParticleActivate(transform);

            playerManager.TextUpdate();

        }
    }
    #endregion
}

