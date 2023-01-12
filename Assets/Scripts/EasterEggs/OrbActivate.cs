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
    Animator animator;
    ParticleSystem pS;
    public override void Awake()
    {
        base.Awake();
        oswietlenie = GetComponentInChildren<Light>();
        material = transform.Find("OrbColor").GetComponent<Renderer>().material;
        animator = GetComponent<Animator>();
        pS = GetComponentInChildren<ParticleSystem>();
    }
    public override Result<object, string> Action(params object[] args)
    {
        if (!wasEnabled)
        {
            t = 0.0f;
            wasEnabled = true;
            ParentSekret.Activate();



            pS.Play();
            animator.SetBool("set", true);

            return Result<object, string>.Ok(wasEnabled.ToString());
        }
        else
        {
            return Result<object,string>.Err(wasEnabled.ToString());
        }
    }

    float t = 0.0f;

    void FixedUpdate()
    {
        if (wasEnabled && t<1)
        {
            Color lerpMat = Color.Lerp(material.color, Color.yellow, t);
            Color lerpEmiss = Color.Lerp(material.GetColor("_EmissionColor"), Color.yellow, t);

            material.SetColor("_EmissionColor", lerpEmiss);
            material.color = lerpMat;
            oswietlenie.color = lerpMat;


            t += Time.deltaTime / 77;
        }
    }

}
