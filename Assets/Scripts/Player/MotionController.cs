using UnityEngine;

public class MotionController : MonoBehaviour
{
    private GameManager gameManager;

    private BattleManager battleManager;

    #region ray sistemi için gereken deðiþkenler

    private float _touchPos;

    //týklama kontrolü
    private bool TouchControl;
    #endregion

    #region hareket hýz deðiþkenleri
    [SerializeField] float xSpeed, roadSpeed;

    //yolun transformu
    [SerializeField] Transform roads;
    #endregion

    #region saða sola kaydýrma sýnýrý deðiþkeni
    private float xBorder = 3f;
    #endregion

    [SerializeField] float swipeSpeed = 2f;
    [SerializeField] float sensibility = 0.1f;


    private void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        battleManager = GameObject.FindGameObjectWithTag("battleManager").GetComponent<BattleManager>();

    }

    void Update()
    {
        Swipe();
    }

    private void FixedUpdate()
    {
        //eðer oyun aktif deðilse diðer komutlara girmez.
        if (!gameManager.gameState)
        {
            return;
        }

        Movement();
    }

    void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            switch (_touch.phase)
            {
                case TouchPhase.Began: //ekrana dokunmaya baþlandýðýnda deðiþkeni true yap
                    TouchControl = true;
                    break;

                case TouchPhase.Moved: //Dokunma devam ediyorsa parmaðýn hareketine göre güncelleme yap
                    if (TouchControl)
                    {
                        _touchPos += _touch.deltaPosition.x * swipeSpeed * sensibility * Time.deltaTime;
                    }
                    break;

                case TouchPhase.Ended: // dokunma bittiðinde deðiþkeni false yap
                case TouchPhase.Canceled:
                    TouchControl = false;
                    break;
            }
        }
    }

    void Movement()
    {
        //savaþ baþlarsa
        if (battleManager.attackState)
        {
            roads.position = new(roads.position.x, roads.position.y, roads.position.z + 0 * Time.fixedDeltaTime);

            #region saða ve sola geçiþ sýnýrý


            if (transform.childCount < 50)
                _touchPos = Mathf.Clamp(_touchPos, -xBorder, xBorder);
            else
                _touchPos = Mathf.Clamp(_touchPos, -2, 2);
            #endregion

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _touchPos, Time.fixedDeltaTime / 3), transform.position.y, transform.position.z);

        }
        else
        {
            roads.position = new(roads.position.x, roads.position.y, roads.position.z + -roadSpeed * Time.fixedDeltaTime);

            #region saða ve sola geçiþ sýnýrý
            if (transform.childCount < 50)
                _touchPos = Mathf.Clamp(_touchPos, -xBorder, xBorder);
            else
                _touchPos = Mathf.Clamp(_touchPos, -2, 2);
            #endregion

            //player saða sola kaydýrma
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _touchPos, Time.fixedDeltaTime * xSpeed), transform.position.y, transform.position.z);
        }
    }
}

//bu script genel olarak: týkladýðýmýz ekran pozisyonuna göre hareket eder. eðer en sað tarafa týklarsak çok hýzlý, ekranýn ortasýnýn birazcýk saðýna týklarsak çok yavaþ bir þekilde saða gider.
//bu týklamalar ray ile görünmez olan bir plane objesinin ne tarafýna týkladýðýmýza baðlý olarak iþliyor.

//bu a. koduðumun planesi o kadar görünmezki hiyeraþi bölümünde bile yok, nasýl oluyor anlamadým...