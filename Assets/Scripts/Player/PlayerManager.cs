using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System;

public class PlayerManager : MonoBehaviour
{
    #region �a��rd���m classlar
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

    #region ge�it i�in gereken de�i�kenler
    //kopyalanacak obje
    [SerializeField] private GameObject stickMan;

    //stickman say�s�n� tutan liste
    // public List<GameObject> stickmanList;

    //karakter say�s�n� tutar. ek bilgi: player objesinin alt objesindeki textte tutar.
    [SerializeField] private TextMeshPro CounterTxt;
    #endregion

    #region karakterlerin pozisyonlar� i�in gereken de�i�kenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    #region sava�aca�� d��man� takip etmesi i�in de�i�ken
    private Transform enemy;
    #endregion

    private void Start()
    {
        #region ba�lang�� karakteri olu�turma, text g�ncelleme
        getClass.objectPoolManager.GetBlueStickman();
        TextUpdate();
        //text aktif oluyor(hiyera�ide kapatt�m)
        transform.GetChild(0).gameObject.SetActive(true);
        #endregion
    }

    private void FixedUpdate()
    {
        //bunun updateden kald�r�p, daha az performans harcayacak bir yere ta��nmas� gerekiyor.
        getClass.animatorManager.PlayerAnimation(transform);

        #region player sava� ba�lad�ysa: d��man� takip eder, d��mana do�ru rotasyonunu �evirir.
        getClass.battleManager.PlayerOffence(enemy, transform);

        #endregion

    }

    #region stickman hizaya getirme i�lemi
    //kopyalanan stickmanlar�n pozisyonu
    public void FormatStickMan()
    {
        TextUpdate();
        Alignment();
        for (int i = 1; i < transform.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UG�M�M� OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UG�M�M� OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            transform.GetChild(i).DOLocalMove(_NewPos, 1.5f).SetEase(Ease.OutBack);

        }
        //text sa�a sola u�up ka��yor onu �nlemek ba�b�nda
        transform.GetChild(0).position = new Vector3(transform.position.x, transform.GetChild(0).position.y, transform.position.z);


    }
    #endregion

    #region karakterlerin rotasyon s�f�rlanmas�
    public void Alignment()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, quaternion.identity, Time.deltaTime);
        }
    }

    #endregion

    #region karakterlerin say�s�n� g�steren text g�ncellemesi
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
            //gate_l nin boxcolliderini kapat�r
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            //gate_r nin boxcolliderini kapat�r
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            //�arpt��� objenin scriptini "_gateManager" olarak miras almam�za olanak sa�lar
            GateManager _gateManager = other.GetComponent<GateManager>();

            //ge�itten ge�ince stickman kopyalan�r
            _gateManager.GetStickman(transform);
        }
        #endregion

        #region d��man g�rd�yse sald�r� moduna ge�er
        if (other.CompareTag("playerDetected"))
        {
            getClass.battleManager.attackState = true;

            //d��mana bakmak i�in ama bu de�i�tirilecek
            enemy = other.transform;
        }
        #endregion

        #region finishe ula�t���nda     yazaca��m�z kodun fazlal���na g�re bunun i�in ayr� script a�abiliriz
        if (other.CompareTag("finish"))
        {
            getClass.gameManager.GameWin();
        }

        #endregion
    }
}