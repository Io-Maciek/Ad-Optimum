using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostawDrabine : MonoBehaviour
{
    Transform forDrabina;
    private void Awake()
    {
        forDrabina = transform.Find("drabinaParent");
    }
    private void OnTriggerEnter(Collider other)
    {
        var holding = other.GetComponent<BetterHolding>();
        if (holding!=null && other.gameObject.name == "ForestLadder")
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            holding.LetGo();
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().isKinematic = true;
            holding.enabled = false;
            other.GetComponent<Climbing>().enabled = true;
            other.transform.localEulerAngles = new Vector3(-10, 0, 0);
            other.transform.position = forDrabina.position;
        }
    }
}
