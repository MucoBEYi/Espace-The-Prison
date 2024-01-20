using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //karakter hareket ettireceðiz
    [SerializeField] private float xSpeed, zSpeed, x;

    private void Start()
    {

    }

    private void Update()
    {
        Swipe();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    void Swipe()
    {
        if (Input.touchCount > 0)
        {

            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {

            }
            else if (_touch.phase == TouchPhase.Moved)
            {
                x = _touch.deltaPosition.x / Screen.width;
                print(x + " moved çalýþýyor");
            }
            else if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                x = 0;
            }
        }
    }

    void Movement()
    {
        if (transform.position.x > -3 || transform.position.x < 3)
        {
            transform.position = new Vector3(xSpeed * x, 0, zSpeed * Time.fixedDeltaTime) + transform.position;
            print(transform.position.x);
        }
    }


}
