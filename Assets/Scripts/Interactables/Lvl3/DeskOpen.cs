using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class DeskOpen : Interactable
{
    GameObject desk;
    public override Result<object, string> Action(params object[] args)
    {
        desk = this.gameObject;
        desk.transform.localPosition = new Vector3(-0.865f, 0.66f, 0.569f);
        IsImportant = false;
        desk.transform.DetachChildren();
        return Result<string>.Ok();
    }
}
