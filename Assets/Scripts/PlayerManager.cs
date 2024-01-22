using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.Sqlite;

public class PlayerManager : MonoBehaviour
{
    //player transformu
    public Transform player;

    //stickman sayýsý
    private int numberOfStickmans;

    //karakter sayýsýný tutar. ek bilgi: player objesinin alt objesindeki textte tutar.
    [SerializeField] private TextMeshPro CounterTxt;

    //kopyalanacak obje
    [SerializeField] private GameObject stickMan;

    //*************************
    [Range(0f, 2f)]
    [SerializeField] float distanceFactor, radius;


    private void Start()
    {
        ///þuan bir iþ yapmýyor.
        player = transform;

        // 1 sayý eksiltmenin sebebi 'Count_Label' objesini görmezden gelmesi için(sanýrým)
        numberOfStickmans = transform.childCount - 1;

        CounterTxt.text = numberOfStickmans.ToString();
    }

    private void Update()
    {
        FormatStickMan();
    }

    //kopyalanan stickmanlarýn pozisyonu
    private void FormatStickMan()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            player.transform.GetChild(i).DOLocalMove(_NewPos, 1).SetEase(Ease.OutBack);
        }

    }


    //stickman kopyalama
    private void MakeStickMan(int number)
    {
        //int 1 den baþlamayýnca yanlýþ sonuç çýkýyor aw
        for (int i = 1; i < number; i++)
        {
            Instantiate(stickMan, transform.position, Quaternion.identity, transform);
        }
        numberOfStickmans = transform.childCount - 1;

        CounterTxt.text = numberOfStickmans.ToString();

        FormatStickMan();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gate"))
        {
            //gate_l nin boxcolliderini kapatýr(bunu unutmazsam destroy olarak deðiþtireceðim)
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            //gate_r nin boxcolliderini kapatýr
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            //çarptýðý objenin scriptini "_gateManager" olarak miras almamýza olanak saðlar
            GateManager _gateManager = other.GetComponent<GateManager>();

            if (_gateManager.multiply)
            {
                MakeStickMan(numberOfStickmans * _gateManager.randomNumber);
            }
            else
            {
                MakeStickMan(numberOfStickmans + _gateManager.randomNumber);
            }

        }
    }


}
