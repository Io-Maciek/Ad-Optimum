using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingTrigger : MonoBehaviour
{
    public Vector3 tp_home { get; set; }
    public uint ProgressValue { get; private set; }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO saving if ProgressValue is higher
        }
    }
}
