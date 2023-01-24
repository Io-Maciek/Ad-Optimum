using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChooser : Interactable
{
    Monitor monitor;
    bool was_clicked = false;
    public override Result<object, string> Action(params object[] args)
    {
        if (monitor == null)
        {
            monitor = transform.parent.parent.parent.GetComponent<Monitor>();
            if (!was_clicked)
            {
                was_clicked = true;
                monitor.StartCoroutine("NextNormal");
            }
        }


        return Result<string>.Ok();
    }
}
