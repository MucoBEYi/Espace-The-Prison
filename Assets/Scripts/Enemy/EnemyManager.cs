using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;

    BattleManager battleManager;

    ObjectPoolManager poolManager;

    #region d��man say�s�, objesi ve Text

    [SerializeField] TextMeshPro CounterTxt;
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] int redStickmanNumber;
    #endregion

    #region d��manlar�n hizas� i�in gereken de�i�kenler
    [Range(0f, 2f)][SerializeField] float distanceFactor, radius;
    #endregion

    private Transform player;
    private bool state;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();

        poolManager = GameObject.FindGameObjectWithTag("poolManager").GetComponent<ObjectPoolManager>();

        poolManager.MakeRedStickman(redStickmanNumber, transform);

        player = GameObject.FindGameObjectWithTag("player").transform;

        CounterTxt.text = (transform.childCount - 1).ToString();

        FormatEnemyStickMan();
    }

    private void FixedUpdate()
    {
        if (state)
        {
            battleManager.EnemyOffence(player, transform);
            //bunun updateden kald�r�p, daha az performans harcayacak bir yere ta��nmas� gerekiyor.
            EnemyAnimation(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
            state = true;
    }



    #region kopyalanan d��man hizas�
    private void FormatEnemyStickMan()            //g�ze daha ho� g�r�ld��� i�in IEnumerator yap�ld�. E�ER AKS�L�K �IKARTIRSA GER� VO�D HAL�NE D�ND�R�LECEK
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            //UFUFU WEWEWE ONYETEN WEWEWE UG�M�M� OSAS
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            //UFUFU WEWEWE ONYETEN WEWEWE UG�M�M� OSAS
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var _NewPos = new Vector3(x, -0.4445743f, z);

            transform.GetChild(i).localPosition = _NewPos;
        }

    }
    #endregion

    #region d��manlar i�in animasyon 
    private void EnemyAnimation(Transform enemy)
    {
        if (gameManager.gameState && battleManager.attackState)
            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", true);                 //getcomponentden daha iyi ��z�m?


        //oyun durursa veya sava� biterse 
        else if (!gameManager.gameState || !battleManager.attackState)
            for (int i = 1; i < enemy.childCount; i++)
                enemy.GetChild(i).GetComponent<Animator>().SetBool("runner", false);
    }
    #endregion

    #region d��manlar�n say�s�n� g�steren text g�ncellemesi
    public void TextUpdate()
    {
        if (transform.childCount == 1)
            transform.GetChild(0).gameObject.SetActive(false);

        //ge� yans�tt��� i�in - 2 yap�yorum.
        CounterTxt.text = (transform.childCount - 2).ToString();

    }
    #endregion

}
