using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GateManager;

public class ObjectPoolManager : MonoBehaviour
{
    PlayerManager playerManager;

    #region mavi karakter sýralama
    //kapalý tutacak liste
    private Queue<GameObject> poolBlueFalseObjects;

    //açýk tutan liste
    private List<GameObject> poolBlueTrueObjects;

    //kopyalanacak karakter objesi
    [SerializeField] private GameObject blueGO;

    #endregion 
    //kopyalanacak karakter sayýsý
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

    #region oyun baþladýðýnda görünmez karakter spawnlanýr
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

    #region objeler görünür olur ve poolBlueTrueObjects sýrasýna girer
    public GameObject GetStickman()
    {
        //poolblueObject sýrasýndan objeyi çýkarýr, bu objeyi _blueGO objesine atar.
        GameObject _blueGO = poolBlueFalseObjects.Dequeue();

        _blueGO.transform.parent = playerManager.transform;

        //_playerGO objesini aktif eder
        _blueGO.SetActive(true);

        //true listesine girer
        poolBlueTrueObjects.Add(_blueGO);

        //hesap iþlemi için bunu kullanýyorum
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




    #region objeler görünmez olur ve poolBlueFalseObjects sýrasýna girer
    public GameObject GiveBlueStickman(GameObject blueGO)
    {

        //ebeveyni artýk bu scripte baðlý objenin alt objesi olur
        blueGO.transform.parent = transform.GetChild(0);

        //_playerGO objesini deaktif eder
        blueGO.SetActive(false);

        //false listesine girer
        poolBlueFalseObjects.Enqueue(blueGO);

        //poolBlueTrueObjects sýrasýndan objeyi çýkarýr
        poolBlueTrueObjects.Remove(blueGO);

        //hesap iþlemi için bunu kullanýyorum
        playerManager.stickmanList.Remove(blueGO);

        return blueGO;
    }
    #endregion


    #region düþmanlar görünmez olur
    public GameObject GiveRedStickman(GameObject redGO)
    {
        Debug.Log("giveRedStickman");
        //ebeveyni artýk bu scripte baðlý objenin alt objesi olur
        redGO.transform.parent = transform.GetChild(1);

        //redGO görünmez olur
        redGO.SetActive(false);

        //false listesine girer
        poolRedFalseObjects.Enqueue(redGO);
        return redGO;
    }
    #endregion
}
