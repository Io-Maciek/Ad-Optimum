using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawkaSitScript : Interactable
{
    GameObject playerObject;

    bool isBeinSittinOn = false;


    public override Result<object, string> Action(params object[] args)
    {
        if (playerObject == null)
            playerObject = args[0] as GameObject;

        StartCoroutine("SitOn");
        return Result<object, string>.Ok(null);
    }



    IEnumerator SitOn()
    {
        Controller _c = playerObject.GetComponent<Controller>();
        _c.movement.enabled = false;
        _c.interaction.enabled = false;
        yield return _c.StartCoroutine("CloseEye", 1.0f);

        playerObject.transform.rotation = transform.rotation;
        isBeinSittinOn = true;
        playerObject.transform.parent = transform;
        playerObject.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerObject.GetComponent<Rigidbody>().detectCollisions = false;
        playerObject.GetComponent<Rigidbody>().useGravity = false;

        yield return _c.StartCoroutine("OpenEye", 1.0f);
    }


    IEnumerator StandUpcomedy()
    {
        isBeinSittinOn = false;
        Controller _c = playerObject.GetComponent<Controller>();

        yield return _c.StartCoroutine("CloseEye", 1.0f);

        playerObject.GetComponent<Rigidbody>().detectCollisions = true;
        playerObject.GetComponent<Rigidbody>().useGravity = true;
        playerObject.transform.localPosition = transform.forward * 2 + Vector3.up;
        playerObject.transform.parent = null;



        yield return _c.StartCoroutine("OpenEye", 1.0f);
        _c.movement.enabled = true;
        _c.interaction.enabled = true;
    }


    private void FixedUpdate()
    {
        if (isBeinSittinOn && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("StandUpcomedy");
        }
    }


}
