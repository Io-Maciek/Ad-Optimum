using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairServer : MonoBehaviour
{
    GameObject server;
    public Material fixedMaterial;

    private void Start()
    {
        server = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "tablet (1)")
        {
            server.GetComponent<MeshRenderer>().material = fixedMaterial;
        }
    }

}
