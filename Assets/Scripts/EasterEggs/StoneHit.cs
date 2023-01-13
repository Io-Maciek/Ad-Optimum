using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHit : Interactable
{
    GameObject playerObject;

    private void Start()
    {
        playerObject = FindObjectOfType<Controller>().gameObject;
        //Debug.Log(playerObject);
        Physics.IgnoreCollision(playerObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    public override Result<object, string> Action(params object[] args)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 diff = playerObject.transform.position;
        Vector3 diff2 = transform.position;
        Vector3 diff3 =( diff-diff2).normalized;

        //Debug.Log(diff3);
        rb.AddForce(diff3 * -27, ForceMode.Impulse);

        return Result<object, string>.Ok("");
    }
}
