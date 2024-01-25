using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCharManager : MonoBehaviour
{
    private BattleManager battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
    }

    #region çarpýþtýðýnda hem karakterimiz hem düþman ölür
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") && battleManager.attackState && other.transform.parent.childCount > 0)
        {
            battleManager.KillTheALL(other.gameObject, gameObject);

        }
    }
    #endregion

}

