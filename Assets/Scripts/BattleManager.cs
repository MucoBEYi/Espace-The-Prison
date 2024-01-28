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


    #region karakterler düþmanlarý takip eder ve eðer düþman kalmadýysa yapýlacak iþlemler
    public void PlayerOffence(Transform enemy, Transform _Player)
    {
        //savaþ yoksa diðer komutlarý çalýþtýrmaz
        if (!attackState)
            return;

        //savaþ baþladýysa playerler enemy içine girer
        if (enemy.GetChild(1).childCount > 0)
            for (int i = 0; i < _Player.transform.childCount; i++)
            {
                Vector3 _Distance = enemy.GetChild(1).GetChild(0).position - _Player.transform.GetChild(i).position;

                if (_Distance.magnitude < 5.5f)
                    //destan yazýlmýþ bu daðlarda, kanlarla kaplý savaþlarla! çiçek açmýþtý bir yamacýnda, hüzünlüydü tek baþýna. bir bayrak kalktý havaya "ZAFER!" diyen çýðlýklarla!
                    //çiçek solmuþtu gidenlerin kanýyla. son nefesini verirken kelimeler kurmaya çalýþtý cabasýyla. aðýzýndan çýkan söz çok derindi ama anlayana "yapacaðýnýz kodu g-"
                    _Player.transform.GetChild(i).position = Vector3.Lerp(_Player.transform.GetChild(i).position,
                         new Vector3(enemy.GetChild(1).GetChild(0).position.x, _Player.transform.GetChild(i).position.y, enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime);
            }
        //düþman bittiyse
        else
        {
            attackState = false;
            enemy.gameObject.SetActive(false);
            playerManager.FormatStickMan();


        }
    }
    #endregion

    #region düþmanlar karakteri takip eder
    public void EnemyOffence(Transform player, Transform enemy)
    {
        //savaþ yoksa diðer komutlarý çalýþtýrmaz
        if (!attackState)
            return;

        //2 taraftan birisi nefes alýyorsa:
        if (enemy.transform.childCount > 1 && player.transform.childCount > 1)
            for (int i = 0; i < enemy.transform.childCount; i++)
            {
                Vector3 _Distance = player.GetChild(1).position - enemy.transform.GetChild(i).position;

                if (_Distance.magnitude < 10.5f)    //player ile düþmanlarýn arasýndaki mesafe
                    enemy.transform.GetChild(i).position = Vector3.Lerp(enemy.transform.GetChild(i).position, player.GetChild(1).position, Time.deltaTime * 2);
            }

        #region oyun kaybedildiðinde çalýþacak kodlar
        else if (enemy.transform.childCount > 1)
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
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(target.gameObject);
        playerManager.stickmanList.Remove(target);

    }
    #endregion
}
