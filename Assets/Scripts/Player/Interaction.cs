using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    new Transform camera;
    bool KeyPressedDown = false;

    public float interactionMaxDistance = 5.0f;
    public bool showDebugRay = false;

    Controller controller;

    void Start()
    {
        camera = transform.Find("PlayerCamera").transform;
        controller = GetComponent<Controller>();
    }


    void Update()
    {
        Interactable obj;
        ThrowRaycast(out obj);

        if (!KeyPressedDown && Input.GetAxis("Use") >= .97)
        {
            KeyPressedDown = true;
            if (showDebugRay)
                Debug.DrawRay(camera.position, camera.forward * interactionMaxDistance, Color.red, 2);

            if (obj != null)
            {
                obj.GetComponent<Interactable>().Action(gameObject).IgnoreOk().Match(
                  (err) =>
                  {
                      Debug.LogWarning(err);
                  });
            }
        }
        else if (KeyPressedDown && Input.GetAxis("Use") == 0.0)
        {
            KeyPressedDown = false;
        }
    }

    private void ThrowRaycast(out Interactable obj)
    {
        obj = null;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionMaxDistance))
        {
            obj = hit.collider.gameObject.GetComponent<Interactable>();
            ColorUI(obj);
        }
        else
        {
            controller.playerUI.crosshair.GetComponent<RawImage>().color = Color.white;
        }
    }

    private void ColorUI(Interactable obj)
    {
        if (obj == null)
        {
            controller.playerUI.crosshair.GetComponent<RawImage>().color = Color.white;
        }
        else
        {
            if (obj.IsImportant)
            {
                controller.playerUI.crosshair.GetComponent<RawImage>().color = Color.green;
            }
            else
            {
                controller.playerUI.crosshair.GetComponent<RawImage>().color = Color.white;

            }
        }
    }
}
