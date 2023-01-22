using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class MeltIceActivator : MonoBehaviour
{

    public Vector3 czyZmiejszac = new Vector3(0, 0, 0);
    private void OnTriggerStay(Collider other)
    {
        var x = other.GetComponent<Pochodnia>();
        if (x != null && x.IamFuming)
        {
            if (GetComponent<Animator>()!=null)
                if(GetComponent<Animator>().enabled)
                    GetComponent<Animator>().enabled = false;
            

            transform.localScale = new Vector3(transform.localScale.x - (Mathf.Abs(czyZmiejszac.x) * Time.deltaTime),
                                                transform.localScale.y - (Mathf.Abs(czyZmiejszac.y) * Time.deltaTime),
                                                transform.localScale.z - (Mathf.Abs(czyZmiejszac.z) * Time.deltaTime));
            if (transform.localScale.x <= 0.01f || transform.localScale.y <= 0.01f || transform.localScale.z <= 0.01f)
            {
                Destroy(gameObject);
            }
            transform.localPosition = new Vector3(transform.localPosition.x + (czyZmiejszac.z * Time.deltaTime),
                                                    transform.localPosition.y + (czyZmiejszac.y * Time.deltaTime),
                                                    transform.localPosition.z + (czyZmiejszac.x * Time.deltaTime));
        }
    }
}
