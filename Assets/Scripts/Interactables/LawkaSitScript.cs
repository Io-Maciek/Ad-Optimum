using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawkaSitScript : Interactable
{
    GameObject playerObject;

    bool isBeinSittinOn = false;

    Vector3 save_pos;
    public override Result<object, string> Action(params object[] args)
    {
        if (playerObject == null)
            playerObject = args[0] as GameObject;

        StartCoroutine("SitOn");
        return Result<object, string>.Ok(null);
    }


    protected virtual Vector3 WhereToSit()
    {
        return transform.position + Vector3.up * 0.5f;
    }


    IEnumerator SitOn()
    {
        Controller _c = playerObject.GetComponent<Controller>();
        _c.movement.enabled = false;
        _c.interaction.enabled = false;
        yield return _c.StartCoroutine("CloseEye", 1.0f);

        isBeinSittinOn = true;
        save_pos = playerObject.transform.position;
        playerObject.transform.position = WhereToSit();
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerObject.GetComponent<Rigidbody>().detectCollisions = false;
        playerObject.GetComponent<Rigidbody>().useGravity = false;
        //playerObject.transform.localRotation = Quaternion.Euler(0, 1, 0);

        yield return _c.StartCoroutine("OpenEye", 1.0f);
    }


    IEnumerator StandUpcomedy()
    {
        isBeinSittinOn = false;
        Controller _c = playerObject.GetComponent<Controller>();

        yield return _c.StartCoroutine("CloseEye", 1.0f);

        playerObject.GetComponent<Rigidbody>().detectCollisions = true;
        playerObject.GetComponent<Rigidbody>().useGravity = true;
        playerObject.transform.position = save_pos;
        //playerObject.transform.parent = null;



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
