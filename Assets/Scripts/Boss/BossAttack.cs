using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    ObjectPoolManager poolManager; PlayerManager playerManager; BattleManager battleManager; SoundManager soundManager; GameManager gameManager;

    private void Start()
    {
        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (other.transform.childCount < 3) //eðer çöp adam kalmamýþsa.
            {
                playerManager.transform.GetChild(0).gameObject.SetActive(false);
                gameManager.LoseMenuActivity();
            }
            //3 tane karakterimizi öldürür
            for (int i = 0; i < 3; i++)
            {
                if (other.transform.GetChild(1) != null)                //sorun ne anlamadým.
                {
                    battleManager.KillTheBlue(other.transform.GetChild(1).gameObject);
                    soundManager.BattleSound();
                    poolManager.BlueParticleActivate(other.transform);
                }
            }
            playerManager.TextUpdate();
            playerManager.FormatStickMan();
        }
    }
}