using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;

    BattleManager battleManager;

    #region düþman prefabs ve Text

    [SerializeField] TextMeshPro CounterTxt;
    [SerializeField] GameObject enemyPrefabs;
    private float numberOfEnemyStickman;
    #endregion

    #region düþmanlarýn hizasý için gereken deðiþkenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    [SerializeField] Transform player;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();

        MakeEnemyStickman(UnityEngine.Random.Range(20, 35));

        FormatEnemyStickMan();
    }

    private void Update()
    {
        battleManager.EnemyOffence(player, transform);
        LookPlayer();
        EnemyAnimation(transform);
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

    #region kopyalanan düþmanlarýn rotasyonu

    private void LookPlayer()
    {
        Vector3 _PlayerDirection = player.position - transform.position;
        //düþmanlar player transformuna rotasyonunu çevirir.
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, quaternion.LookRotation(_PlayerDirection, Vector3.up), Time.deltaTime);

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

        TextUpdate();
    }
    #endregion

    #region düþmanlar için animasyon 
    private void EnemyAnimation(Transform enemy)
    {
        if (gameManager.gameState && battleManager.attackState)
            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", true);                 //getcomponentden daha iyi çözüm?


        //oyun durursa veya savaþ biterse 
        else if (!gameManager.gameState || !battleManager.attackState)
            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", false);
    }
    #endregion

    #region düþmanlarýn sayýsýný gösteren text güncellemesi
    public void TextUpdate()
    {
        CounterTxt.text = transform.childCount.ToString();
    }
    #endregion

}
