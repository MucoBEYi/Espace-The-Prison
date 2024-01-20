using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManagerM : MonoBehaviour
{
    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        MoveThePlayer();
    }

    bool moveByTouch;
    Vector3 mouseStartPos, playerStartPos;

    void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveByTouch = true;

            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1);
                playerStartPos = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;
        }
        if (moveByTouch)
        {
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                Vector3 mousePos = ray.GetPoint(distance + 1);
                Vector3 move = mousePos - playerStartPos;

                Vector3 control = playerStartPos + move;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * 5), transform.position.y, transform.position.z);

            }
        }
    }
}
