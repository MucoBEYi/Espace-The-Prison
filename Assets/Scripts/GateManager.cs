using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GateManager : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] Transform player;

    #region geçit deðiþkenleri
    public TextMeshPro GateNo;

    //gatelerde yazacak sayý
    public int randomNumber;

    //yapýlan iþlemler
    public enum GateType { multiply, addition, extraction }

    GateType gateType;
    #endregion


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();

        playerManager = player.gameObject.GetComponent<PlayerManager>();

        GateNumber();
    }



    void GateNumber()
    {
        gateType = (GateType)Random.Range(0, 2);

        //switch case kullanýyorum xd
        switch (gateType)
        {
            case GateType.multiply:
                randomNumber = Random.Range(1, 5);
                GateNo.text = "X " + randomNumber.ToString();

                break;
            case GateType.addition:
                randomNumber = Random.Range(20, 50);
                GateNo.text = randomNumber.ToString();
                break;

            //ayarlayamadýðým için þimdilik iptal ettim.
            case GateType.extraction:
                randomNumber = Random.Range(-20, -50);
                GateNo.text = randomNumber.ToString();
                break;

            default:
                break;
        }
    }


    #region stickman kopyalama 
    public void MakeStickMan(GameObject stickMan, GameObject player)
    {
        #region eðer çarpma iþlemi olursa
        if (gateType == GateType.multiply)
        {
            randomNumber = (randomNumber * playerManager.stickmanList.Count) - playerManager.stickmanList.Count;

            for (int i = 0; i < randomNumber; i++)
            {
                GameObject _playerGO = Instantiate(stickMan, player.transform.position, Quaternion.identity, player.transform);
                playerManager.stickmanList.Add(_playerGO);
            }

        }

        #endregion

        #region eðer toplama iþlemi olursa
        else if (gateType == GateType.addition)
            for (int i = 0; i < randomNumber; i++)
            {
                GameObject _playerGO = Instantiate(stickMan, player.transform.position, Quaternion.identity, player.transform);
                playerManager.stickmanList.Add(_playerGO);
            }
        #endregion

        //playerin üstündeki text
        playerManager.TextUpdate();

        //stickmanlarýn pozisyonu
        playerManager.FormatStickMan();

        //BOZUK DÜZELTEBÝLEN DÜZELTSÝN
        /*  else if (gateType == GateType.extraction)
          {

              for (int i = 1; i < -randomNumber; i++)
              {
                  Transform pChar = player.transform.GetChild(i);
                  playerManager.stickmanList.Remove(pChar.gameObject);
                  Destroy(pChar.gameObject);
              }
          }*/
    }
    #endregion

}
