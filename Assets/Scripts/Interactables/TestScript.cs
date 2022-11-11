using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : Interactable<bool>
{
    public override bool Action(params object[] args)
    {
        Debug.Log("Jestem tu!");
        return true;
    }
}
