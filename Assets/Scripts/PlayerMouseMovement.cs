using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : MonoBehaviour
{
    [SerializeField]
    Transform player;
    public float sensitivity = 2;

    Vector2 velocity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        velocity += rawFrameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);//keeps it in min/max

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);//vertical - camera
        player.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);//horizontal - player
    }
}
