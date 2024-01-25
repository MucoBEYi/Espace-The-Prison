using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    //karakteri hizaya sokmak için lazým (gene scriptleri çorba ettim yaw...)
    [SerializeField] Transform player;


    #region atak kontrol
    public bool attackState;
    #endregion


    #region karakterler düþmanlarý takip eder 
    public void PlayerOffence(Transform enemy, Transform _Player)
    {
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
        else
        {
            attackState = false;
            Alignment(player);
            enemy.gameObject.SetActive(false);
            StartCoroutine(playerManager.FormatStickMan());

        }
    }
    #endregion

    #region düþmanlar karakteri takip eder ve pozisyonunu playere çevirir
    public void EnemyOffence(Transform player, Transform enemy)
    {
        //savaþ yoksa diðer komutlarý çalýþtýrmaz
        if (!attackState)
            return;

        //savaþ baþladýysa enemyler playerin içine girer
        if (attackState && enemy.transform.childCount > 1)
        {

            Vector3 _PlayerDirection = player.position - enemy.position;

            for (int i = 0; i < enemy.transform.childCount; i++)
            {
                //düþmanlar player transformuna rotasyonunu çevirir.
                enemy.transform.GetChild(i).rotation = Quaternion.Slerp(enemy.transform.GetChild(i).rotation, quaternion.LookRotation(_PlayerDirection, Vector3.up), Time.deltaTime);

                Vector3 _Distance = player.GetChild(0).position - enemy.transform.GetChild(i).position;
                
                if (_Distance.magnitude < 10.5f)    //player ile düþmanlarýn arasýndaki mesafe
                {
                    enemy.transform.GetChild(i).position = Vector3.Lerp(enemy.transform.GetChild(i).position, player.GetChild(1).position, Time.deltaTime * 2);
                }
            }
        }
    }

    #endregion

    #region DÝE DÝE DÝE DÝE DÝE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void KillTheALL(GameObject red, GameObject blue)
    {
        Destroy(red.gameObject);
        Destroy(blue.gameObject);
    }
    #endregion



    //*********** BU KODUN YERÝ DEÐÝÞEBÝLÝR MUHTEMELEN BURAYA UYGUN DEÐÝL **********\\

    #region savaþ sonrasý rotasyon sýfýrlama giriþ
    public void Alignment(Transform _Player)
    {
        for (int i = 1; i < _Player.transform.childCount; i++)
        {
            _Player.transform.GetChild(i).rotation = Quaternion.Slerp(_Player.transform.GetChild(i).rotation, quaternion.identity, Time.deltaTime);
        }
    }

    #endregion




}
