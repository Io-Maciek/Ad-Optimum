using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{

    GameObject playerOBJ;
    bool canClimb = false;
    public float speed = 1;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            canClimb = true;
            playerOBJ = coll.gameObject;
            playerOBJ.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void OnCollisionExit(Collision coll2)
    {
        if (coll2.gameObject.tag == "Player")
        {
            canClimb = false;
            playerOBJ.GetComponent<Rigidbody>().useGravity = true;
            playerOBJ = null;
        }
    }
    void Update()
    {
        if (canClimb)
        {
            if (Input.GetAxisRaw("Vertical")>0.0f)
            {
                playerOBJ.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            if (Input.GetAxisRaw("Vertical") <0.0f)
            {
                playerOBJ.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
    }
}

