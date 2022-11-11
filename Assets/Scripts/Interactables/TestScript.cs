using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : Interactable
{
    public override Result<string> Action(params object[] args)
    {
        Debug.Log("Jestem tu!");
        return Result<string>.Ok();
    }
}
