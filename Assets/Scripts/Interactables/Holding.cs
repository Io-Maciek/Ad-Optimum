using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : Interactable
{
    public bool isBeingHold { get; set; } = false;
    GameObject playerObject;

    public override Result<object, string> Action(params object[] args)
    {
        playerObject = args[0] as GameObject;
        Debug.Log(playerObject);

        switch (isBeingHold)
        {
            //TODO go to default position to not rotate on start
            case false:
                transform.parent = playerObject.transform.Find("PlayerCamera");
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().detectCollisions = false;
                break;
        }

        isBeingHold = !isBeingHold;
        return Result<object, string>.Ok(gameObject);
    }

    void FixedUpdate()
    {
        if (isBeingHold)
        {
            //playerObject.transform.Find("PlayerCamera").transform
            Vector3 joeBidenPosition = (playerObject.transform.Find("PlayerCamera").transform.position
                + playerObject.transform.Find("PlayerCamera").transform.forward * 2);
            transform.position = joeBidenPosition;
        }
    }
}