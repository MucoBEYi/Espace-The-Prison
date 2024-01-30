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

    #region �arp��t���nda d��man �l�r
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("red") && battleManager.attackState && collision.transform.parent.childCount > 1)
        {
            battleManager.KillTheRed(collision.gameObject);
            

            Instantiate(blueParticle, transform.position, Quaternion.identity);

            playerManager.TextUpdate();

        }
    }
    #endregion
}

