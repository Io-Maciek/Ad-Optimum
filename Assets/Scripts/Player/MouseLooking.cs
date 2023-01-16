using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooking : MonoBehaviour
{
    new Transform camera;
    Vector2 velocity;

    [Range(0f, 10f)]
    public float sensitivity = 2.0f;

    void Start()
    {
        camera = transform.Find("PlayerCamera").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        velocity += rawFrameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);//keeps it in min/max 

        camera.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);//vertical - camera
        transform.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);//horizontal - player
    }// TODO cos tu jest mocno zbugowane | trzeba wylaczyc Animator -a
}
