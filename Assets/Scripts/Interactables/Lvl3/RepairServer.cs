using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairServer : MonoBehaviour
{
    GameObject server;
    public Material fixedMaterial;
    bool isRepaired = false;

    private void Start()
    {
        server = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRepaired && other.name == "tablet")
        {
            server.GetComponent<MeshRenderer>().material = fixedMaterial;
            isRepaired = true;
        }
    }

}
