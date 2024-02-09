using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private GameManager gameManager;
    private MotionController motionController;

    private Animator animator;
    public float bossHealth = 100;
    public bool isDeath;
    [SerializeField] float bossSpeed;
    private float Distance;
    public float start = 25f;

    private Transform player;

    public TextMeshPro bossTxt;
    private void Start()
    {
        motionController = GameObject.FindGameObjectWithTag("player").GetComponent<MotionController>();
        player = motionController.transform;
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        bossTxt.text = bossHealth.ToString();


    }

    public void BossDamage(float damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            if (!isDeath)
                BossDeath();
        }
    }


    void Update()
    {

        if (gameManager.bossBattlestate)
        {
            Distance = Vector3.Distance(transform.position, player.position);

            if (Distance < start)
            {
                Fight();
            }
        }
    }

    private void Fight()
    {
        if (gameManager.gameState)
        {
            //valla bütün tuþlara bastým, çalýþýyor mu? çalýþýyor.
            if ((player.position - transform.position).magnitude > 1)
            {
                Debug.Log("boss if");
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, player.position.x, Time.deltaTime), transform.position.y, Mathf.MoveTowards(transform.position.z, player.position.z, bossSpeed * Time.deltaTime));
            }
            else
            {
                Debug.Log("boss else");
                transform.position -= new Vector3(0, 0, Mathf.MoveTowards(0, -99, Time.deltaTime));
            }
            //text bu scripte baðlý objeyi takip eder(bayaðý kýsa yazdým dimi wetrqwetqwer).              
            transform.parent.GetChild(0).position = new Vector3(Mathf.Lerp(transform.parent.GetChild(0).position.x, transform.position.x, Time.deltaTime * 2),
                transform.parent.GetChild(0).position.y, Mathf.Lerp(transform.parent.GetChild(0).position.z, transform.position.z + 1, Time.deltaTime));

            animator.SetBool("run", true);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));



            //özür dilerim ama artýk proje o kadar çorba oldu ki, neyi nereye yazacaðýmý bilemedim.. bir daha proje yaparsam ilk neyi nereye yazacaðýmý not alacam 
            for (int i = 1; i < player.childCount; i++)         //bu fordaki etkilerinin genel olarak amacý: prisoner karakterleri rotasyonunu bossa çevirir ve ona doðru ilerler
            {
                player.GetChild(i).LookAt(new Vector3(transform.position.x, player.GetChild(i).position.y, transform.position.z));
                if ((player.GetChild(i).position - transform.position).magnitude > 20f)
                    player.GetChild(i).position = new Vector3(player.GetChild(i).position.x, player.GetChild(i).position.y, Mathf.MoveTowards(player.GetChild(i).position.z, transform.position.z, Time.deltaTime));
            }

            if (Distance < 3f)
            {
                animator.SetBool("hit", true);
                gameManager.permission = true;
                if (Distance > 2f)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.Translate(direction * 2f * Time.deltaTime);
                }
            }
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("hit", false);
        }
    }

    public void BossDeath()
    {
        isDeath = true;
        animator.SetBool("death", true);

        gameManager.permission = false;
        StartCoroutine(gameManager.GameWin());
        gameManager.bossBattlestate = false;
        bossTxt.transform.parent.gameObject.SetActive(false);
    }
}
