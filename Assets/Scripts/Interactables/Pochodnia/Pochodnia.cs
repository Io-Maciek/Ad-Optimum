using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pochodnia : BetterHolding
{
    ParticleSystem[] _fire;
    Light _beam_me_up_scotty;
    bool IamFuming = false;
    Animator anime;

    protected override void Awake()
    {
        base.Awake();
        _fire = GetComponentsInChildren<ParticleSystem>();
        _beam_me_up_scotty = GetComponentInChildren<Light>();
        _beam_me_up_scotty.enabled = false;
        anime = GetComponent<Animator>();
    }

    public void FireItUp()
    {
        foreach (var particle in _fire) particle.Play();
        _beam_me_up_scotty.enabled = true;
    }

    public void Extinguish()
    {
        foreach (var particle in _fire) particle.Stop();
        _beam_me_up_scotty.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (isGrabbed)
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (IamFuming)
                    FireItUp();
                else
                    Extinguish();
                IamFuming = !IamFuming;
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                /*rb.detectCollisions = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.isKinematic = true;
                anime.enabled = true;
                anime.SetBool("GET_ANGRY", true);*/
                //rb.AddForce((-transform.right*20 + camera.transform.forward*15)*sensitivity);
            }
        }
    }
}