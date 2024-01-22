using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MotionController : MonoBehaviour
{
    #region ray sistemi için gereken deðiþkenler
    //private yapýlacak
    [SerializeField] Camera cam;

    float _touchPos;

    //týklama kontrolü
    bool TouchControl;
    #endregion

    #region hareket hýz deðiþkenleri
    [SerializeField] float xSpeed, zSpeed;
    #endregion

    #region saða sola kaydýrma sýnýrý deðiþkeni
    [SerializeField] float xBorder = 3;
    #endregion
    private void Start()
    {
        //ya böyle tanýmlama þekli mi olur #@=!%
        cam = Camera.main;
    }

    void Update()
    {
        Swipe();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Motion(hareket) iþlemlemleri
    //kaydýrma
    void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            //týkladýðýnda
            if (_touch.phase == TouchPhase.Began)
            {
                TouchControl = true;
            }
            //elini kaldýrdýðýnda
            if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                TouchControl = false;

            }
        }


        if (TouchControl)
        {
            //AMACIN NE SENÝN YA
            Plane _plane = new(Vector3.up, 0);          //buradaki 0 ne iþe yarýyor bilmiyorum ama 9999 yapýnca karakter çok fazla hýzla saða veya sola gidiyor.

            //ekrandaki týklama pozisyonunu _ray a ekler(sanýrým)
            Ray _ray = cam.ScreenPointToRay(Input.GetTouch(0).position);        //ray bu týklama pozisyonunu dünya kordinatýna çeviriyor, origin ve direction olarak 2 farklý kordinat veriyor(sanýrým)


            //eðer _ray plane'ye çarparsa(sanýrým)
            if (_plane.Raycast(_ray, out float distance))   //out float distance kýsmý ise týkladýðýmýz pozisyona baðlý olarak deðer döndürüyor.
            {
                _touchPos = _ray.GetPoint(distance).x;      // "Çarpma noktasýný al ve x koordinatýný al"  chat gpt açýklamasý, hiç bir þey anlamadým.
            }
        }
    }
    //hareket komutu
    void Movement()
    {
        //saða ve sola geçiþ sýnýrý
        _touchPos = Mathf.Clamp(_touchPos, -xBorder, xBorder);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _touchPos, Time.fixedDeltaTime * xSpeed), 0.5f, transform.position.z + zSpeed * Time.fixedDeltaTime);
    }
    #endregion
}

//bu script genel olarak: týkladýðýmýz ekran pozisyonuna göre hareket eder. eðer en sað tarafa týklarsak çok hýzlý, ekranýn ortasýnýn birazcýk saðýna týklarsak çok yavaþ bir þekilde saða gider.
//bu týklamalar ray ile görünmez olan bir plane objesinin ne tarafýna týkladýðýmýza baðlý olarak iþliyor.

//bu a. koduðumun planesi o kadar görünmezki hiyeraþi bölümünde bile yok, nasýl oluyor anlamadým...