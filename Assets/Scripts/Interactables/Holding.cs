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

    Vector3 defScale;

    private void Start()
    {
        defScale = transform.localScale;
    }

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
            GetComponent<Rigidbody>().isKinematic = true;

            isBeingHold = true;
            transform.localScale = defScale * useScale;
            Physics.IgnoreCollision(playerObject.GetComponent<Collider>(), GetComponent<Collider>(), true);



            Vector3 joeBidenPosition = playerObject.transform.Find("PlayerCamera").transform.position;
            Vector3 covidDistance = playerObject.transform.Find("PlayerCamera").transform.forward * howFar;
            Vector3 getDown = Vector3.down * howDown;
            transform.position = joeBidenPosition + covidDistance + getDown;
        }

        return Result<object, string>.Ok(gameObject);
    }

    public void LetItGo()
    {
        if (playerObject != null)
        {
            Vector3 afterSpeed = playerObject.GetComponent<Rigidbody>().velocity;
            //Vector3 afterSpeed2 = playerObject.GetComponent<Rigidbody>().angularVelocity;

            Physics.IgnoreCollision(playerObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
            isBeingHold = false;
            transform.parent = null;

            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            playerObject = null;
            transform.localScale = defScale;

            GetComponent<Rigidbody>().velocity = afterSpeed;// +(afterSpeed2*10);
        }
    }


    void FixedUpdate()
    {
/*        if (isBeingHold)
        {
            Vector3 joeBidenPosition = (playerObject.transform.Find("PlayerCamera").transform.position
                + playerObject.transform.Find("PlayerCamera").transform.forward * howFar)+Vector3.down*howDown;
            transform.position = joeBidenPosition;
            transform.rotation = new Quaternion(0,0,0,0);
        }*/
    }



}