using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GateManager;

public class ObjectPoolManager : MonoBehaviour
{
    PlayerManager playerManager;

    #region mavi karakter s�ralama
    //kapal� tutacak liste
    private Queue<GameObject> poolBlueFalseObjects;

    //a��k tutan liste
    private List<GameObject> poolBlueTrueObjects;

    //kopyalanacak karakter objesi
    [SerializeField] private GameObject blueGO;

    #endregion 
    //kopyalanacak karakter say�s�
    [SerializeField] private int poolSize;



    private Queue<GameObject> poolRedFalseObjects;






    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        poolBlueFalseObjects = new Queue<GameObject>();
        poolBlueTrueObjects = new List<GameObject>();
        poolRedFalseObjects = new Queue<GameObject>();

        MakeStickman();
    }

    #region oyun ba�lad���nda g�r�nmez karakter spawnlan�r
    void MakeStickman()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject _blueGO = Instantiate(blueGO, transform.GetChild(0));
            _blueGO.SetActive(false);

            //false listesine girer
            poolBlueFalseObjects.Enqueue(_blueGO);
        }
    }
    #endregion

    #region objeler g�r�n�r olur ve poolBlueTrueObjects s�ras�na girer
    public GameObject GetStickman()
    {
        //poolblueObject s�ras�ndan objeyi ��kar�r, bu objeyi _blueGO objesine atar.
        GameObject _blueGO = poolBlueFalseObjects.Dequeue();

        _blueGO.transform.parent = playerManager.transform;

        //_playerGO objesini aktif eder
        _blueGO.SetActive(true);

        //true listesine girer
        poolBlueTrueObjects.Add(_blueGO);

        //hesap i�lemi i�in bunu kullan�yorum
        playerManager.stickmanList.Add(_blueGO);
        if (poolBlueFalseObjects.Count <= 1)
            MakeStickmanPrecaution();

        return _blueGO;
    }
    #endregion



    void MakeStickmanPrecaution()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject _blueGO = Instantiate(blueGO, transform.GetChild(0));
            _blueGO.SetActive(false);

            //false listesine girer
            poolBlueFalseObjects.Enqueue(_blueGO);
        }
    }




    #region objeler g�r�nmez olur ve poolBlueFalseObjects s�ras�na girer
    public GameObject GiveBlueStickman(GameObject blueGO)
    {

        //ebeveyni art�k bu scripte ba�l� objenin alt objesi olur
        blueGO.transform.parent = transform.GetChild(0);

        //_playerGO objesini deaktif eder
        blueGO.SetActive(false);

        //false listesine girer
        poolBlueFalseObjects.Enqueue(blueGO);

        //poolBlueTrueObjects s�ras�ndan objeyi ��kar�r
        poolBlueTrueObjects.Remove(blueGO);

        //hesap i�lemi i�in bunu kullan�yorum
        playerManager.stickmanList.Remove(blueGO);

        return blueGO;
    }
    #endregion


    #region d��manlar g�r�nmez olur
    public GameObject GiveRedStickman(GameObject redGO)
    {
        Debug.Log("giveRedStickman");
        //ebeveyni art�k bu scripte ba�l� objenin alt objesi olur
        redGO.transform.parent = transform.GetChild(1);

        //redGO g�r�nmez olur
        redGO.SetActive(false);

        //false listesine girer
        poolRedFalseObjects.Enqueue(redGO);
        return redGO;
    }
    #endregion
}
