using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    new Transform camera;
    bool KeyPressedDown = false;

    public float interactionMaxDistance = 5.0f;
    public bool showDebugRay = false;

    GameObject usedObject = null;

    void Start()
    {
        camera = transform.Find("PlayerCamera").transform;
    }


    void Update()
    {
        if (!KeyPressedDown && Input.GetAxis("Use") >= .97)
        {
            KeyPressedDown = true;
            Debug.Log(usedObject);
            if (usedObject==null)
            {
                Ray ray = new Ray(camera.position, camera.forward);
                if (showDebugRay)
                    Debug.DrawRay(camera.position, camera.forward * interactionMaxDistance, Color.red, 2);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactionMaxDistance))
                {
                    GameObject obj = hit.collider.gameObject;
                    if (obj.tag == "Interactable")
                    {
                        Holding holder = obj.GetComponent<Holding>();
                        if (holder != null)
                        {
                            usedObject = holder.Action(gameObject).GetOk() as GameObject;
                        }
                        else
                        {
                            obj.GetComponent<Interactable>().Action(gameObject).Match(
                              (obj) =>
                              {
                              },
                              (err) =>
                              {
                                  Debug.LogWarning(err);
                              });
                        }
                    }
                }
            }
            else
            {
                usedObject.transform.parent = null;
                usedObject.GetComponent<Holding>().isBeingHold = false;
                usedObject.GetComponent<Rigidbody>().useGravity = true;
                usedObject.GetComponent<Rigidbody>().detectCollisions = true;
                usedObject = null;
            }
        }
        else if (KeyPressedDown && Input.GetAxis("Use") == 0.0)
        {
            KeyPressedDown = false;
        }
    }
}
