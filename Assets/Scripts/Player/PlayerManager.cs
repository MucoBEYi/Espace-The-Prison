using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.Mathematics;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    [SerializeField] PlayerAnimator playerAnimator;

    #region geçit için gereken deðiþkenler

    //stickman sayýsý
    private int numberOfStickmans;

    //karakter sayýsýný tutar. ek bilgi: player objesinin alt objesindeki textte tutar.
    [SerializeField] private TextMeshPro CounterTxt;

    //kopyalanacak obje
    [SerializeField] private GameObject stickMan;

    #endregion

    #region karakterlerin pozisyonlarý için gereken deðiþkenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    #region savaþacaðý düþmaný takip etmesi için deðiþken
    private Transform enemy;
    #endregion

    private void Start()
    {

        #region geçitle alakalý
        numberOfStickmans = transform.childCount - 1;       // 1 sayý eksiltmenin sebebi 'Count_Label' objesini görmezden gelmesi için(sanýrým)
        CounterTxt.text = numberOfStickmans.ToString();
        #endregion

    }

    private void Update()
    {
        //bu kod satýrý burada geçicidir. OnTriggerEnter e taþýnabilir(yada ucuza kaçýp burada býrakýrým :P)
        playerAnimator.PlayerAnimation(transform);

        #region player savaþ baþladýysa: düþmaný takip eder, düþman bittiyse atak durumu false döner. düþmana doðru rotasyonunu çevirir.
        battleManager.PlayerOffence(enemy, transform);
        LookEnemy();
        #endregion
    }

    #region düþmana doðru pozisyon alma iþlemi
    private Vector3 enemyDirection;
    //eðer savaþ baþladýysa karakterlerimiz düþmana doðru bakar
    void LookEnemy()
    {
        //savaþ baþlamadýysa diðer komutlara girmez
        if (!battleManager.attackState)
            return;

        //DÜZELTÝLDÝ AMA B DA ÇIKABÝLÝR
        //  Transform _Enemy = GameObject.FindGameObjectWithTag("enemyManager").transform.GetChild(0);
        //her saniye bu iþlemi yapmasýný istemediðim için böyle bir önlem aldým(performans arttýrmak baÐbýnda)
        if (enemyDirection == Vector3.zero)
            enemyDirection = enemy.position - transform.position;

        //karakter sayýsýný gösteren ve kameranýn dönmemesi için bu scripte baðlý objenin alt objelerine hükmediyorum
        for (int i = 1; i < transform.childCount; i++)
        {
            //transform rotation yapýsýný öðrenmem gerekiyor..
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 5);
        }

    }
    #endregion

    #region stickman hizaya getirme iþlemi
    //kopyalanan stickmanlarýn pozisyonu
    public void FormatStickMan()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            
            transform.GetChild(i).DOLocalMove(_NewPos, 1.5f).SetEase(Ease.OutBack);
        }
    }
    #endregion

    #region savaþ sonrasý karakterlerin rotasyon sýfýrlanmasý
    public void Alignment()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, quaternion.identity, Time.deltaTime);
        }
    }

    #endregion


    #region stickman kopyalama iþlemleri
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

        //stickmanlarýn pozisyonu
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
        #endregion

        #region düþman gördüyse saldýrý moduna geçer
        if (other.CompareTag("playerDetected"))
        {
            battleManager.attackState = true;

            //düþmana bakmak için transformunu buradan alýyorum(muhtemelen daha yere yerleþtirilebilirdi bu kod).
            enemy = other.transform;
        }
    }
    #endregion

}


//Transform = player kodunu çýkarttým hiç bir iþe yaramýyordu. gerekirse tekrar eklerim.