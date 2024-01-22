using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RayTest : MonoBehaviour
{
    Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //týklama varsa
        if (Input.touchCount > 0)
        {
            //týklama inputunu tanýmý
            Touch _touch = Input.GetTouch(0);

            Vector3 a = _touch.position;

            //kameranýn açýsýndaki _touch pozisyonunu alýr ve onu dünya kordinatýna çevirir
            Ray _ray = cam.ScreenPointToRay(a);

            print(_ray + " ray");
            print(a);
        }
    }
}
