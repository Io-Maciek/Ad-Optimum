using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class DeskOpen : Interactable
{
    GameObject desk;
    public Vector3 wektor = new Vector3(-0.865f, 0.66f, 0.569f);
    public override Result<object, string> Action(params object[] args)
    {
        desk = this.gameObject;
        desk.transform.localPosition = wektor;
        IsImportant = false;
        desk.transform.DetachChildren();
        GameObject.Find("tablet").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        return Result<string>.Ok();
    }
}
