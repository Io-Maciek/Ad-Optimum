using Assets.Scripts.EasterEggs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbActivate : Interactable
{
    public Sekret ParentSekret;
    bool wasEnabled = false;


    Light oswietlenie;
    Material material;
    public override void Awake()
    {
        base.Awake();
        oswietlenie = GetComponentInChildren<Light>();
        material = transform.Find("OrbColor").GetComponent<Renderer>().material;
    }
    public override Result<object, string> Action(params object[] args)
    {
        if (!wasEnabled)
        {
            wasEnabled = true;
            ParentSekret.Activate();

            // TODO aktywuj g³os i poni¿sze zrób animacjami
            material.color = Color.yellow;
            material.SetColor("_EmissionColor", Color.yellow);
            oswietlenie.intensity = 3.0f;
            oswietlenie.color = Color.white;
            oswietlenie.range = 10.0f;


            return Result<object, string>.Ok(wasEnabled.ToString());
        }
        else
        {
            return Result<object,string>.Err(wasEnabled.ToString());
        }
    }
}
