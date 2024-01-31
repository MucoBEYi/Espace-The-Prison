using UnityEngine;

public class MotionController : MonoBehaviour
{
    private GameManager gameManager;

    private BattleManager battleManager;

    #region ray sistemi i�in gereken de�i�kenler

    private float _touchPos;

    //t�klama kontrol�
    private bool TouchControl;
    #endregion

    #region hareket h�z de�i�kenleri
    [SerializeField] float xSpeed, roadSpeed;

    //yolun transformu
    [SerializeField] Transform roads;
    #endregion

    #region sa�a sola kayd�rma s�n�r� de�i�keni
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
        //e�er oyun aktif de�ilse di�er komutlara girmez.
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
                case TouchPhase.Began: //ekrana dokunmaya ba�land���nda de�i�keni true yap
                    TouchControl = true;
                    break;

                case TouchPhase.Moved: //Dokunma devam ediyorsa parma��n hareketine g�re g�ncelleme yap
                    if (TouchControl)
                    {
                        _touchPos += _touch.deltaPosition.x * swipeSpeed * sensibility * Time.deltaTime;
                    }
                    break;

                case TouchPhase.Ended: // dokunma bitti�inde de�i�keni false yap
                case TouchPhase.Canceled:
                    TouchControl = false;
                    break;
            }
        }
    }

    void Movement()
    {
        //sava� ba�larsa
        if (battleManager.attackState)
        {
            roads.position = new(roads.position.x, roads.position.y, roads.position.z + 0 * Time.fixedDeltaTime);

            #region sa�a ve sola ge�i� s�n�r�


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

            #region sa�a ve sola ge�i� s�n�r�
            if (transform.childCount < 50)
                _touchPos = Mathf.Clamp(_touchPos, -xBorder, xBorder);
            else
                _touchPos = Mathf.Clamp(_touchPos, -2, 2);
            #endregion

            //player sa�a sola kayd�rma
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _touchPos, Time.fixedDeltaTime * xSpeed), transform.position.y, transform.position.z);
        }
    }
}

//bu script genel olarak: t�klad���m�z ekran pozisyonuna g�re hareket eder. e�er en sa� tarafa t�klarsak �ok h�zl�, ekran�n ortas�n�n birazc�k sa��na t�klarsak �ok yava� bir �ekilde sa�a gider.
//bu t�klamalar ray ile g�r�nmez olan bir plane objesinin ne taraf�na t�klad���m�za ba�l� olarak i�liyor.

//bu a. kodu�umun planesi o kadar g�r�nmezki hiyera�i b�l�m�nde bile yok, nas�l oluyor anlamad�m...