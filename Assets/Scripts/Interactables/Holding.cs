using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : Interactable
{
    public bool isBeingHold { get; private set; } = false;
    GameObject playerObject;

    [Range(0f, 3.0f)]
    public float howFar = 2.25f;
    [Range(-1f, 1f)]
    public float howDown = .2f;
    [Range(0.0f, 1.0f)]
    public float useScale = .8f;

    public override Result<object, string> Action(params object[] args)
    {
        if (playerObject == null)
            playerObject = args[0] as GameObject;

        return Use();
    }

    Result<object, string> Use()
    {
        if (!isBeingHold)
        {
            transform.parent = playerObject.transform.Find("PlayerCamera");
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = false;
            GetComponent<Rigidbody>().isKinematic = true;
            isBeingHold = true;
            transform.localScale = new Vector3(useScale, useScale, useScale);
        }

        return Result<object, string>.Ok(gameObject);
    }

    public void LetItGo()
    {
        if (playerObject != null)
        {
            isBeingHold = false;
            transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().detectCollisions = true;
            playerObject = null;
            transform.localScale = Vector3.one;
        }
    }


    void FixedUpdate()
    {
        if (isBeingHold)
        {
            Vector3 joeBidenPosition = (playerObject.transform.Find("PlayerCamera").transform.position
                + playerObject.transform.Find("PlayerCamera").transform.forward * howFar)+Vector3.down*howDown;
            transform.position = joeBidenPosition;
        }
    }



}