using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GateManager : MonoBehaviour
{
    PlayerManager playerManager;

    ObjectPoolManager objectPoolManager;

    #region ge�it de�i�kenleri
    public TextMeshPro GateNo;

    //gatelerde yazacak say�
    public int randomNumber;

    //yap�lan i�lemler
    private enum GateType { multiply, addition, subtraction }

    private GateType gateType;
    #endregion



    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();

        objectPoolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();

        GetGateType();
    }


    //gelecek t�re g�re rasgele i�lem
    void GetGateType()
    {
        gateType = (GateType)Random.Range(0, 2);

        //switch case kullan�yorum xd
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

            //ayarlayamad���m i�in �imdilik iptal ettim.
            case GateType.addition:
                randomNumber = Random.Range(20, 50);
                GateNo.text = randomNumber.ToString();
                break;
        }
    }


    #region stickman kopyalama 
    public void GetStickman(Transform player)
    {
        //�arpma i�lemi gelirse
        if (gateType == GateType.multiply)
            randomNumber = (player.transform.childCount - 1) * randomNumber - player.transform.childCount + 1;
        //��karma i�lemi gelirse
        else if (gateType == GateType.subtraction)
        {
            //bu sa�l�ks�z �al���yor diye �uanda ask�da
        }

        for (int i = 0; i < randomNumber; i++)
            objectPoolManager.GetBlueStickman();


        #region text ve format g�ncellemesi
        playerManager.TextUpdate();

        playerManager.FormatStickMan();
        #endregion

    }
    #endregion

}
