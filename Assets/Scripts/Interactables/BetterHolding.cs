using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterHolding : Interactable
{
    GameObject player;
    new GameObject camera;
    Controller controller;
    Rigidbody rb;

    bool isGrabbed = false;
    bool keyDown = false;

    [Range(0f, 10.0f)]
    public float howFar = 2.25f;
    [Range(-1f, 1f)]
    public float howDown = .2f;
    [Range(-5, 5f)]
    public float howRight = 2f;

    [Range(0f, 200f)]
    public float sensitivity = 100f;

    public float MaxDistance = 5f;

    public Vector3 rotation;



    public override Result<object, string> Action(params object[] args)
    {
        if (player == null)
        {
            player = args[0] as GameObject;
            controller = player.GetComponent<Controller>();
            camera = player.transform.Find("PlayerCamera").gameObject;
            rb = GetComponent<Rigidbody>();
        }

        return use();
    }

    Result<object, string> use()
    {
        isGrabbed = true;
        controller.interaction.enabled = false;
        rb.useGravity = false;


        rb.drag = 10;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.transform.parent = camera.transform;

        transform.localEulerAngles = rotation;

        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(),true);
        keyDown = true;
        return Result<object, string>.Ok(null);
    }

    void LetGo()
    {
        isGrabbed = false;
        rb.useGravity = true;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        controller.interaction.enabled = true;

        rb.drag = 1;
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;


    }


    private void Update()
    {
        if (isGrabbed)
        {
            Vector3 tep = camera.transform.position + camera.transform.forward * howFar + Vector3.down * howDown;
            float dis = Vector3.Distance(transform.position, tep);
            if (dis > MaxDistance)
            {
                transform.position = tep;
            }
            else if (dis > 0.1f)
            {
                Vector3 move = tep - transform.position;
                rb.AddForce(move * sensitivity);
            }
            //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, tep2, Time.deltaTime * 2f);
        }
    }

    void FixedUpdate()
    {
        if (isGrabbed)
        {
            var key = Input.GetAxis("Use");

            if (key == 0.0)
                keyDown = false;

            if(!keyDown && key > .97)
                LetGo();
        }
    }
}
