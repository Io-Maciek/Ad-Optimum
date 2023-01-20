using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class used for holding a physics object.
/// </summary>
public class BetterHolding : Interactable
{
    protected GameObject player;
    protected new GameObject camera;
    protected Controller controller;
    protected Rigidbody rb;

    protected bool isGrabbed = false;
    bool keyDown = false;


    [Range(0f, 10.0f)]
    public float howFar = 2.25f;
    [Range(-1f, 1f)]
    public float howDown = .2f;
    [Range(-2f, 2f)]
    public float howRight = 0.0f;

    [Tooltip("Smothly drags object to the player. Take in consideration the mass of an object!")]

    public float sensitivity = 100f;

    [Tooltip("Teleports object to player after this distance. Take in consideration the size of an object!")]
    public float MaxDistance = 5f;


    [Space]
    [Tooltip("Enables \"Special Rotation\"")]
    public bool overrideRotation = false;
    [Tooltip("Forces object to rotate differently that it was picked up. Only if \"Override Rotation\" is on")]
    public Vector3 specialRotation = Vector3.zero;
    public bool _debug = false;



    public override Result<object, string> Action(params object[] args)
    {
        if (player == null)
        {
            player = args[0] as GameObject;
            controller = player.GetComponent<Controller>();
            camera = player.transform.Find("PlayerCamera").gameObject;
            rb = GetComponent<Rigidbody>();
        }

        return Use();
    }

    protected virtual Result<object, string> Use()
    {
        controller.playerUI.crosshair.SetActive(false);
        isGrabbed = true;
        controller.interaction.enabled = false;
        rb.useGravity = false;


        rb.drag = 10;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.transform.parent = camera.transform;

        if(overrideRotation)
            transform.localEulerAngles = specialRotation;

        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(),true);
        keyDown = true;
        return Result<object, string>.Ok(null);
    }

    public virtual void LetGo()
    {
        controller.playerUI.crosshair.SetActive(true);
        isGrabbed = false;
        rb.useGravity = true;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        controller.interaction.enabled = true;

        rb.drag = 1;
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
    }


    protected virtual void Update()
    {
        if (isGrabbed)
        {
            Vector3 tep = camera.transform.position + camera.transform.forward  * howFar + Vector3.down * howDown + camera.transform.right* howRight;
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

            if (_debug && overrideRotation)
                transform.localEulerAngles = specialRotation;
            //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, tep2, Time.deltaTime * 2f);
        }
    }

    protected virtual void FixedUpdate()
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
