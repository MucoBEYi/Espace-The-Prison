using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Kamera referansýný tutan deðiþken
    Camera cam;

    private void Start()
    {
        // Oyun baþladýðýnda, ana kamera referansýný alýr.
        cam = Camera.main;
    }

    private void Update()
    {
        // Her frame'de MoveThePlayer metodu çaðrýlýr.
        MoveThePlayer();
    }

    // Oyuncunun dokunmatik ekran hareketlerine tepki verip vermeyeceðini belirleyen deðiþken
    bool moveByTouch;
    // Fare veya dokunmatik ekranýn baþlangýç pozisyonu ve oyuncunun baþlangýç pozisyonunu saklayan deðiþkenler
    Vector3 mouseStartPos, playerStartPos;

    // Oyuncunun hareketini kontrol eden metot
    void MoveThePlayer()
    {
        // Eðer kullanýcý fareye týkladýysa (veya dokunmatik ekran üzerinde bir dokunma varsa)
        if (Input.GetMouseButtonDown(0))
        {
            // Hareket etmesi için izin verilir
            moveByTouch = true;

            // Yere sabit bir düzlem tanýmlanýr (Vector3.up, yani y eksenine dik bir düzlem)
            // Bu düzlem, yere doðru bir düzlem oluþturur ve bu düzleme dik olan bir ýþýn oluþturulur.
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Fare düzleme çarparsa, çarpma mesafesi elde edilir.
            if (plane.Raycast(ray, out var distance))
            {
                // Düzlemden bir birim yukarýda olan nokta elde edilir ve bu nokta baþlangýç noktasý olarak saklanýr.
                mouseStartPos = ray.GetPoint(distance + 1);
                playerStartPos = transform.position;
            }
        }

        // Eðer kullanýcý fare tuþunu býraktýysa (veya dokunmatik ekran üzerindeki dokunmayý sonlandýrdýysa)
        if (Input.GetMouseButtonUp(0))
        {
            // Hareket etme izni kaldýrýlýr.
            moveByTouch = false;
        }

        // Eðer hareket izni varsa
        if (moveByTouch)
        {
            // Yukarýda tanýmlanan düzlem ve ýþýn kullanýlarak fare pozisyonu elde edilir.
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Düzleme çarpma gerçekleþirse, çarpma mesafesi elde edilir.
            if (plane.Raycast(ray, out var distance))
            {
                // Çarpma mesafesi üzerindeki noktayý elde eder ve bu noktayý fare pozisyonu olarak saklar.
                Vector3 mousePos = ray.GetPoint(distance + 1);

                // Fare baþlangýç pozisyonundan bu noktaya olan hareket vektörü elde edilir.
                Vector3 move = mousePos - playerStartPos;

                // Hareket vektörü, oyuncunun baþlangýç pozisyonuna eklenerek kontrol pozisyonu elde edilir.
                Vector3 control = playerStartPos + move;

                // Oyuncunun x pozisyonu, control pozisyonuna doðru yavaþça geçiþ yapar (Lerp metodu kullanýlýr).
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * 5), transform.position.y, transform.position.z);
            }
        }
    }
}