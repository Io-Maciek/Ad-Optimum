using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : Interactable
{
    public bool isBeingHold { get; private set; } = false;
    GameObject playerObject;

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
            isBeingHold = true;
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
            GetComponent<Rigidbody>().detectCollisions = true;
            playerObject = null;
        }
    }


    void FixedUpdate()
    {
        if (isBeingHold)
        {
            Vector3 joeBidenPosition = (playerObject.transform.Find("PlayerCamera").transform.position
                + playerObject.transform.Find("PlayerCamera").transform.forward * 2);
            transform.position = joeBidenPosition;
        }
    }
}