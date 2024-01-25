using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;

    BattleManager battleManager;

    #region düþman prefabs ve Text

    [SerializeField] TextMeshPro CounterTxt;
    [SerializeField] GameObject enemyPrefabs;
    #endregion

    #region düþmanlarýn hizasý için gereken deðiþkenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    [SerializeField] Transform player;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();

        MakeEnemyStickman(Random.Range(20,35));

        FormatEnemyStickMan();
    }

    private void Update()
    {
        battleManager.EnemyOffence(player, transform);

    }

    #region kopyalanan düþman hizasý
    private void FormatEnemyStickMan()            //göze daha hoþ görüldüðü için IEnumerator yapýldý. EÐER AKSÝLÝK ÇIKARTIRSA GERÝ VOÝD HALÝNE DÖNDÜRÜLECEK
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UGÝMÝMÝ OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            transform.GetChild(i).localPosition = _NewPos;
        }

    }
    #endregion

    #region stickman kopyalama iþlemleri
    //kopyalanacak düþman
    void MakeEnemyStickman(int randomEnemy)
    {

        for (int i = 0; i < randomEnemy; i++)
        {
            Instantiate(enemyPrefabs, transform.position, new Quaternion(0, 180, 0, 1), transform);
        }

        CounterTxt.text = (transform.childCount).ToString();            //adam dengesiz transform.childcount - 1 yapýyor ama countertxt yi bu scriptin alt objesi yapmýyor, benden daha kötü matematiði var ama cos sin biliyor. rez alýn tekrar alt objesi yapacak, baþka þansý yok, çünkü text adamlarý takip etmiyor.
    }
    #endregion


    #region düþmanlar için animasyon
    private IEnumerator EnemyAnimation(Transform enemy)
    {
        if (gameManager.gameState)
        {
            //düþman ile karakterimiz anlýk bakýþýyor(eðer beðenilmezse çýkarýlacak).
            yield return new WaitForSeconds(0.35f);


            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", true);                 //getcomponentden daha iyi çözüm?

        }
        //oyun durursa
        else if (!gameManager.gameState)
            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", false);
    }
    #endregion



    private void OnTriggerEnter(Collider other)
    {
        //telefonu yormamasý için updateye atmadým taþýdým
        if (other.CompareTag("blue"))
        {
            StartCoroutine(EnemyAnimation(transform));
          
        }

    }

}
