using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float timeToMatch = 1;

    float timer;
    private void OnTriggerStay(Collider other)
    {
        var x = other.GetComponent<Pochodnia>();
        if (x != null)
        {
            timer += Time.deltaTime;
            if (timer >= timeToMatch)
                x.FireItUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var x = other.GetComponent<Pochodnia>();
        if (x != null)
        {
            timer = 0.0f;
        }
    }
}
