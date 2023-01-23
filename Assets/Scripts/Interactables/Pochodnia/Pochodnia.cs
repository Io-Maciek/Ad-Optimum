using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pochodnia : BetterHolding
{
    ParticleSystem[] _fire;
    Light _beam_me_up_scotty;
    public bool IamFuming = false;
    public bool IsFiredUp = false;
    Animator anime;

    Vector3 vecTemp;
    float temp;
    protected override void Awake()
    {
        base.Awake();
        _fire = GetComponentsInChildren<ParticleSystem>();
        _beam_me_up_scotty = GetComponentInChildren<Light>();
        _beam_me_up_scotty.enabled = false;
        anime = GetComponent<Animator>();
        temp = howRight;
        vecTemp = specialRotation;
        if (IsFiredUp)
        {
            FireItUp();
        }
    }

    public void FireItUp()
    {
        if (!IamFuming)
        {
            IamFuming = true;
            foreach (var particle in _fire) particle.Play();
            _beam_me_up_scotty.enabled = true;
        }
    }

    public void Extinguish()
    {
        if (IamFuming)
        {
            IamFuming = false;
            foreach (var particle in _fire) particle.Stop();
            _beam_me_up_scotty.enabled = false;
        }
    }

    protected override Result<object, string> Use()
    {
        var x = base.Use();
        GetComponent<CapsuleCollider>().radius = .6f;
        GetComponent<CapsuleCollider>().height = 2f;

        return x;
    }

    public override void LetGo()
    {
        base.LetGo();
        GetComponent<CapsuleCollider>().radius = 1f;
        GetComponent<CapsuleCollider>().height = 3f;

        Extinguish();
    }


    protected override void Update()
    {
        if (isGrabbed)
        {
            if (Input.GetAxis("Fire2")>0.0f)
            {
                howRight = 0;
                transform.localEulerAngles = new Vector3(specialRotation.x, 0, 0);
                /*rb.detectCollisions = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.isKinematic = true;
                anime.enabled = true;
                anime.SetBool("GET_ANGRY", true);*/
                //rb.AddForce((-transform.right*20 + camera.transform.forward*15)*sensitivity);
            }
            else 
            { 
                howRight = temp;
                transform.localEulerAngles = specialRotation;
            }
        }
        base.Update();
    }
}
