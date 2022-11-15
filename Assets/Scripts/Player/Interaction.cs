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


    void Start()
    {
        camera = transform.Find("PlayerCamera").transform;
    }


    void Update()
    {
        if (!KeyPressedDown && Input.GetAxis("Use") >= .97)
        {
            KeyPressedDown = true;
            Ray ray = new Ray(camera.position, camera.forward);
            if (showDebugRay)
                Debug.DrawRay(camera.position, camera.forward * interactionMaxDistance, Color.red, 2);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionMaxDistance))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "Interactable")
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
        else if (KeyPressedDown && Input.GetAxis("Use") == 0.0)
        {
            KeyPressedDown = false;
        }
    }
}
