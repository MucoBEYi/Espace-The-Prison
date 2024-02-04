using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OriginalMovementController : MonoBehaviour
{
    // Kamera referans�n� tutan de�i�ken
    Camera cam;

    private void Start()
    {
        // Oyun ba�lad���nda, ana kamera referans�n� al�r.
        cam = Camera.main;
    }

    private void Update()
    {
        // Her frame'de MoveThePlayer metodu �a�r�l�r.
        MoveThePlayer();
    }

    // Oyuncunun dokunmatik ekran hareketlerine tepki verip vermeyece�ini belirleyen de�i�ken
    bool TouchControl;
    // Fare veya dokunmatik ekran�n ba�lang�� pozisyonu ve oyuncunun ba�lang�� pozisyonunu saklayan de�i�kenler
    Vector3 mouseStartPos, playerStartPos;

    // Oyuncunun hareketini kontrol eden metot
    void MoveThePlayer()
    {
        // E�er kullan�c� fareye t�klad�ysa (veya dokunmatik ekran �zerinde bir dokunma varsa)
        if (Input.GetMouseButtonDown(0))
        {
            // Hareket etmesi i�in izin verilir
            TouchControl = true;

            // Yere sabit bir d�zlem tan�mlan�r (Vector3.up, yani y eksenine dik bir d�zlem)
            // Bu d�zlem, yere do�ru bir d�zlem olu�turur ve bu d�zleme dik olan bir ���n olu�turulur.
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Fare d�zleme �arparsa, �arpma mesafesi elde edilir.
            if (plane.Raycast(ray, out var distance))
            {
                // D�zlemden bir birim yukar�da olan nokta elde edilir ve bu nokta ba�lang�� noktas� olarak saklan�r.
                mouseStartPos = ray.GetPoint(distance + 1);
                playerStartPos = transform.position;
            }
        }

        // E�er kullan�c� fare tu�unu b�rakt�ysa (veya dokunmatik ekran �zerindeki dokunmay� sonland�rd�ysa)
        if (Input.GetMouseButtonUp(0))
        {
            // Hareket etme izni kald�r�l�r.
            TouchControl = false;
        }

        // E�er hareket izni varsa
        if (TouchControl)
        {
            // Yukar�da tan�mlanan d�zlem ve ���n kullan�larak fare pozisyonu elde edilir.
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // D�zleme �arpma ger�ekle�irse, �arpma mesafesi elde edilir.
            if (plane.Raycast(ray, out var distance))
            {
                // �arpma mesafesi �zerindeki noktay� elde eder ve bu noktay� fare pozisyonu olarak saklar.
                Vector3 mousePos = ray.GetPoint(distance + 1);

                // Fare ba�lang�� pozisyonundan bu noktaya olan hareket vekt�r� elde edilir.
                Vector3 move = mousePos - playerStartPos;

                // Hareket vekt�r�, oyuncunun ba�lang�� pozisyonuna eklenerek kontrol pozisyonu elde edilir.
                Vector3 control = playerStartPos + move;

                control.x = Mathf.Clamp(control.x, -7, 7);

                // Oyuncunun x pozisyonu, control pozisyonuna do�ru yava��a ge�i� yapar (Lerp metodu kullan�l�r).
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * 5), transform.position.y, transform.position.z);
            }
        }
    }
}