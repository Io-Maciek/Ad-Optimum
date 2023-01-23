using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOff : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<Pochodnia>().IamFuming)
            {
                other.GetComponent<Pochodnia>().Extinguish();
            }
        }
        catch
        {

        }

    }

}
