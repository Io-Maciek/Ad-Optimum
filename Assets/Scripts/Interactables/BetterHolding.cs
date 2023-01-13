using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterHolding : Interactable
{
    GameObject player;
    Controller controller;

    bool isGrabbed = false;
    bool keyDown = false;

    [Range(0f, 3.0f)]
    public float howFar = 2.25f;
    [Range(-1f, 1f)]
    public float howDown = .2f;
    [Range(0.0f, 1.0f)]
    public float useScale = .8f;

    Vector3 defScale;

    private void Start()
    {
        defScale = transform.localScale;
    }

    public override Result<object, string> Action(params object[] args)
    {
        if (player == null)
        {
            player = args[0] as GameObject;
            controller = player.GetComponent<Controller>();
        }

        return use();
    }

    Result<object, string> use()
    {
        //transform.parent = player.transform;

        isGrabbed = true;
        controller.interaction.enabled = false;
        keyDown = true;
        GetComponent<Rigidbody>().useGravity = false;
        transform.localScale = defScale;

        Vector3 joeBidenPosition = player.transform.Find("PlayerCamera").transform.position;
        Vector3 covidDistance = player.transform.Find("PlayerCamera").transform.forward * howFar;
        Vector3 getDown = Vector3.down * howDown;
        transform.position = joeBidenPosition + covidDistance + getDown;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);

        return Result<object, string>.Ok(null);
    }

    void LetGo()
    {
        isGrabbed = false;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        controller.interaction.enabled = true;
        transform.localScale = defScale * useScale;
        GetComponent<Rigidbody>().useGravity = true;
        //transform.parent = null;
    }


    private void Update()
    {
        if (isGrabbed)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Vector3 joeBidenPosition = player.transform.Find("PlayerCamera").transform.position;
            Vector3 covidDistance = player.transform.Find("PlayerCamera").transform.forward * howFar;
            Vector3 getDown = Vector3.down * howDown  +   Vector3.right*.5f;
            transform.position = Vector3.Lerp(transform.position, joeBidenPosition + covidDistance + getDown, Time.deltaTime * 5f);

            //TODO ¿yj transform.LookAt(player.transform);
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
