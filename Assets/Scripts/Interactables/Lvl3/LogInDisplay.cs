using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogInDisplay : Interactable
{
    public GameObject nextEkran;

    Monitor monitor;
    public override Result<object, string> Action(params object[] args)
    {
        if(monitor == null)
        {
            monitor = transform.parent.parent.parent.GetComponent<Monitor>();
        }

        if( ApplicationModelInfo.GameSave.SecretNumber.All(s => s == true)) // ukryte zakoñczenie
        {
            Debug.Log("Sekret!");
            monitor.EndSecret();
        }
        else // normalne
        {
            Debug.Log("Normalne");
            monitor.Next(nextEkran, 1);
        }


        return Result<string>.Ok();
    }
}
