using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : Interactable
{
    public override Result<object,string> Action(params object[] args)
    {
        Debug.Log("Jestem tu!");
        GetComponent<Rigidbody>().AddForce(Vector3.up * 3, ForceMode.VelocityChange);
        return Result<object, string>.Ok(gameObject);
    }
}
