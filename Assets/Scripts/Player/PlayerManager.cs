using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System;

public class PlayerManager : MonoBehaviour
{
    #region çaðýrdýðým classlar
    [Serializable]
    private class GetClass
    {
        public BattleManager battleManager;

        public AnimatorManager animatorManager;

        public GameManager gameManager;

        public ObjectPoolManager objectPoolManager;
    }

    [SerializeField] GetClass getClass;
    #endregion

    #region geçit için gereken deðiþkenler
    //kopyalanacak obje
    [SerializeField] private GameObject stickMan;

    //stickman sayýsýný tutan liste
    // public List<GameObject> stickmanList;

    //karakter sayýsýný tutar. ek bilgi: player objesinin alt objesindeki textte tutar.
    [SerializeField] private TextMeshPro CounterTxt;
    #endregion

    #region karakterlerin pozisyonlarý için gereken deðiþkenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    #region savaþacaðý düþmaný takip etmesi için deðiþken
    private Transform enemy;
    #endregion

    private void Start()
    {
        #region baþlangýç karakteri oluþturma, text güncelleme
        getClass.objectPoolManager.GetBlueStickman();
        TextUpdate();
        //text aktif oluyor(hiyeraþide kapattým)
        transform.GetChild(0).gameObject.SetActive(true);
        #endregion
    }

    private void FixedUpdate()
    {
        //bunun updateden kaldýrýp, daha az performans harcayacak bir yere taþýnmasý gerekiyor.
        getClass.animatorManager.PlayerAnimation(transform);

        #region player savaþ baþladýysa: düþmaný takip eder, düþmana doðru rotasyonunu çevirir.
        getClass.battleManager.PlayerOffence(enemy, transform);

        #endregion

    }

    #region stickman hizaya getirme iþlemi
    //kopyalanan stickmanlarýn pozisyonu
    public void FormatStickMan()
    {
        TextUpdate();
        Alignment();
        for (int i = 1; i < transform.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            transform.GetChild(i).DOLocalMove(_NewPos, 1.5f).SetEase(Ease.OutBack);

        }
        //text saða sola uçup kaçýyor onu önlemek baÐbýnda
        transform.GetChild(0).position = new Vector3(transform.position.x, transform.GetChild(0).position.y, transform.position.z);


    }
    #endregion

    #region karakterlerin rotasyon sýfýrlanmasý
    public void Alignment()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, quaternion.identity, Time.deltaTime);
        }
    }

    #endregion

    #region karakterlerin sayýsýný gösteren text güncellemesi
    public void TextUpdate()
    {
        CounterTxt.text = (transform.childCount - 1).ToString();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        #region karakter kopyalama
        if (other.gameObject.CompareTag("gate"))
        {
            //gate_l nin boxcolliderini kapatýr
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            //gate_r nin boxcolliderini kapatýr
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            //çarptýðý objenin scriptini "_gateManager" olarak miras almamýza olanak saðlar
            GateManager _gateManager = other.GetComponent<GateManager>();

            //geçitten geçince stickman kopyalanýr
            _gateManager.GetStickman(transform);
        }
        #endregion

        #region düþman gördüyse saldýrý moduna geçer
        if (other.CompareTag("playerDetected"))
        {
            getClass.battleManager.attackState = true;

            //düþmana bakmak için ama bu deðiþtirilecek
            enemy = other.transform;
        }
        #endregion

        #region finishe ulaþtýðýnda     yazacaðýmýz kodun fazlalýðýna göre bunun için ayrý script açabiliriz
        if (other.CompareTag("finish"))
        {
            getClass.gameManager.GameWin();
        }

        #endregion
    }
}