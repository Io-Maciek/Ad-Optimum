using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInDisplay : Interactable
{
    public override Result<object, string> Action(params object[] args)
    {
        Debug.Log("ZALOGOWANO");

        return Result<string>.Ok();
    }
}
