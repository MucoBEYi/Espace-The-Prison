using UnityEngine;

public class BossManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] MotionController motionController;

    private Animator animator;
    [SerializeField] float bossHealth = 100;
    public bool isDeath;
    [SerializeField] float bossSpeed;
    private float Distance;
    public float start = 25f;
    public bool bossBattlestate;

    public Transform player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
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

        if (bossBattlestate)
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
                transform.position = Vector3.MoveTowards(transform.position, player.position, bossSpeed * Time.deltaTime);
            else
                transform.position -= new Vector3(0, 0, Mathf.Lerp(0, -1, Time.deltaTime / 10));


            animator.SetBool("run", true);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            gameManager.StopButton.SetActive(false);
            if (Distance < 3f)
            {
                animator.SetBool("hit", true);
                motionController.permission = true;
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

        motionController.permission = false;
        StartCoroutine(gameManager.GameWin());
        bossBattlestate = false;
    }
}
