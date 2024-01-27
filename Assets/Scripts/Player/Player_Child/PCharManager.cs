using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PCharManager : MonoBehaviour
{
    private BattleManager battleManager;
    private PlayerManager playerManager;

    //mavi kan diyebiliriz :D
    [SerializeField] ParticleSystem blueParticle;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        playerManager = transform.parent.GetComponent<PlayerManager>();

    }

    #region çarpýþtýðýnda hem karakterimiz hem düþman ölür
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") && battleManager.attackState && other.transform.parent.childCount > 0)
        {
            battleManager.KillTheALL(other.gameObject);

            Instantiate(blueParticle, transform.position, Quaternion.identity);

            playerManager.TextUpdate();
        }
    }
    #endregion



}

