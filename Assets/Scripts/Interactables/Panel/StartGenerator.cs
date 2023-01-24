using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGenerator : MonoBehaviour
{
    IActivatable aktywacja;

    ParticleSystem particle;
    bool hasEnergy = false;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        aktywacja = transform.parent.GetComponentInChildren<PanelClickEnd>() as IActivatable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasEnergy)
        {
            if (other.gameObject.name.StartsWith("Fuse"))
            {
                var hold = other.GetComponent<BetterHolding>();
                hold.LetGo();
                Destroy(other.gameObject);
                hasEnergy = true;
                particle.Play();
                aktywacja.SetTo(true);
            }
        }
    }
}
