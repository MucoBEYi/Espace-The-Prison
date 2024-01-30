using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GateManager : MonoBehaviour
{
    PlayerManager playerManager;

    ObjectPoolManager objectPoolManager;

    #region geçit deðiþkenleri
    public TextMeshPro GateNo;

    //gatelerde yazacak sayý
    public int randomNumber;

    //yapýlan iþlemler
    private enum GateType { multiply, addition, subtraction }

    private GateType gateType;
    #endregion



    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();

        objectPoolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();

        GetGateType();
    }


    //gelecek türe göre rasgele iþlem
    void GetGateType()
    {
        gateType = (GateType)Random.Range(0, 2);

        //switch case kullanýyorum xd
        switch (gateType)
        {
            case GateType.multiply:
                randomNumber = Random.Range(1, 5);
                GateNo.text = "X " + randomNumber.ToString();
                break;

            case GateType.subtraction:
                randomNumber = Random.Range(-20, -50);
                GateNo.text = randomNumber.ToString();
                break;

            //ayarlayamadýðým için þimdilik iptal ettim.
            case GateType.addition:
                randomNumber = Random.Range(20, 50);
                GateNo.text = randomNumber.ToString();
                break;
        }
    }


    #region stickman kopyalama 
    public void GetStickman()
    {
        //çarpma iþlemi gelirse
        if (gateType == GateType.multiply)
            randomNumber = (randomNumber * playerManager.stickmanList.Count) - playerManager.stickmanList.Count;
        //çýkarma iþlemi gelirse
        else if (gateType == GateType.subtraction)
        {
            //bu saðlýksýz çalýþýyor diye þuanda askýda
        }

        for (int i = 0; i < randomNumber; i++)
            objectPoolManager.GetStickman();


        #region text ve format güncellemesi
        playerManager.TextUpdate();

        playerManager.FormatStickMan();
        #endregion

    }
    #endregion

}
