using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorOstatniSit : Interactable
{
    GameObject playerObject;
    public Transform sitter;

    public Monitor wyswietlacz;
    public override Result<object, string> Action(params object[] args)
    {
        if (playerObject == null)
            playerObject = args[0] as GameObject;

        StartCoroutine("SitOn");
        return Result<object, string>.Ok(null);
    }



    IEnumerator SitOn()
    {
        playerObject.transform.parent = wyswietlacz.transform;

        Controller _c = playerObject.GetComponent<Controller>();
        _c.movement.enabled = false;
        _c.interaction.enabled = false;
        _c.mouseMovement.enabled = false;
        yield return _c.StartCoroutine("CloseEye", 1.0f);

        playerObject.transform.position = sitter.position + Vector3.up * 0.5f;
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerObject.GetComponent<Rigidbody>().detectCollisions = false;
        playerObject.GetComponent<Rigidbody>().useGravity = false;
        //playerObject.transform.localRotation = Quaternion.Euler(0, 1, 0);
        _c.mouseMovement.enabled = true;

        yield return _c.StartCoroutine("OpenEye", 1.0f);
        _c.interaction.enabled = true;
        enabled = false;

        yield return wyswietlacz.StartCoroutine("Init");
    }




}
