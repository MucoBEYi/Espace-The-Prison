using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    #region atak kontrol
    public bool attackState;
    #endregion

    private Vector3 playerDistance;
    private Vector3 enemyDistance;

    #region karakterler düþmanlarý takip eder ve eðer düþman kalmadýysa yapýlacak iþlemler
    public void PlayerOffence(Transform enemyZone, Transform _Player)
    {
        //savaþ yoksa diðer komutlarý çalýþtýrmaz
        if (!attackState)
            return;

        //savaþ baþladýysa playerler enemy içine girer
        if (enemyZone.GetChild(0).childCount > 1)
            for (int i = 1; i < _Player.childCount; i++)
            {
                #region karakterin rotasyonunu düþmanlara çevirir
                for (int j = 1; j < enemyZone.GetChild(0).childCount; j++)
                {
                    enemyDistance = enemyZone.GetChild(0).GetChild(j).position - _Player.GetChild(i).position;
                }

                _Player.GetChild(i).rotation = Quaternion.Slerp(_Player.GetChild(i).rotation, Quaternion.LookRotation(enemyDistance, Vector3.up), Time.fixedDeltaTime * 10);

                #endregion

                #region karakterlerimiz düþmanlarý takip eder
                enemyZone.GetChild(0).GetChild(0).gameObject.SetActive(true);


                if (enemyDistance.magnitude < 10f)      //düþman ile arasýndaki mesafe
                    _Player.GetChild(i).position = Vector3.Lerp(_Player.GetChild(i).position, enemyZone.GetChild(0).GetChild(1).position, Time.fixedDeltaTime / 5);     //düþmana doðru ilerler


                //text 1. karakteri takip eder(bug fix)
                _Player.GetChild(0).position = new Vector3(_Player.GetChild(0).position.x, _Player.GetChild(0).position.y, Mathf.Lerp(_Player.GetChild(0).position.z, _Player.GetChild(1).position.z, Time.fixedDeltaTime));
                #endregion
            }
        //düþman bittiyse
        else
        {
            attackState = false;
            enemyZone.gameObject.SetActive(false);
            playerManager.FormatStickMan();

            print("düþman bitti");
        }
    }
    #endregion

    #region düþmanlar karakteri takip eder, rotasyonunu karaktere çevirir ve kaybedildiyse yapýlacak iþlemler
    public void EnemyOffence(Transform player, Transform enemy)          //player = playerManager
    {
        //savaþ yoksa diðer komutlarý çalýþtýrmaz
        if (!attackState)
            return;

        //2 taraftan birisi nefes alýyorsa:
        if (enemy.childCount > 1 && player.childCount > 1)
            for (int i = 1; i < enemy.childCount; i++)
            {
                #region düþmanlarýn rotasyonunu karaktere çevirir                
                for (int j = 1; j < player.childCount; j++)
                {
                    //düþmanlar player transformuna rotasyonunu çevirmek için gereken kod(hiç bir þey anlamadým pozisyon ile rotasyonu ayarlamak nasýl yaw)
                    playerDistance = player.GetChild(j).position - enemy.GetChild(i).position;
                }
                enemy.GetChild(i).rotation = Quaternion.Slerp(enemy.GetChild(i).rotation, quaternion.LookRotation(playerDistance, Vector3.up), Time.fixedDeltaTime * 10);
                #endregion


                #region karakteri takip eder
                if (playerDistance.magnitude < 10f)     //player ile düþmanlarýn arasýndaki mesafe
                {
                    enemy.GetChild(i).position = Vector3.Lerp(enemy.GetChild(i).position, player.GetChild(1).position, Time.fixedDeltaTime * 2);

                    //text 1. enemyi takip eder(bug fix)
                    enemy.GetChild(0).position = new Vector3(Mathf.Lerp(enemy.GetChild(0).position.x, enemy.GetChild(1).position.x, Time.fixedDeltaTime),
                        enemy.GetChild(0).position.y, Mathf.Lerp(enemy.GetChild(0).position.z, enemy.GetChild(1).position.z, Time.fixedDeltaTime));
                }
                #endregion
            }
        #region oyun kaybedildiðinde çalýþacak kodlar
        else if (enemy.childCount > 1)
        {
            #region oyunu bitirir
            GameManager gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
            gameManager.gameState = false;
            attackState = false;
            player.GetChild(0).gameObject.SetActive(false);
            gameManager.LoseMenu.SetActive(true);
            #endregion

            print("düþman kazandý");
        }
        #endregion
    }

    #endregion

    #region DÝE DÝE DÝE DÝE DÝE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public IEnumerator KillTheALL(GameObject target)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(target.gameObject);
        playerManager.stickmanList.Remove(target);

    }
    #endregion
}
