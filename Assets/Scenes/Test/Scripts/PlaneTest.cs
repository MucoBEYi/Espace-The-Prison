using UnityEngine;

public class PlaneTest : MonoBehaviour
{
    public Plane plane;

    void Start()
    {
        // Düzlemi baþlangýçta oluþturun.
        plane = new Plane(Vector3.up, 0);
    }

    void Update()
    {
        // Nesnenin olduðu pozisyondan düzleme doðru bir ýþýn oluþturun.
        Ray ray = new Ray(transform.position, Vector3.down);

        // Iþýn ile düzlemin çarpýþmasýný kontrol edin.
        if (plane.Raycast(ray, out float hitDistance))
        {
            Debug.Log("Nesne düzlemin üzerinde!");
        }
        else
        {
            Debug.Log("Nesne düzlemin üzerinde deðil!");
        }
    }
}