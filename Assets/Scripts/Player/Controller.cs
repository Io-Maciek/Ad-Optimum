using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Controller : MonoBehaviour
{
    public AudioSource MainMusicSource { get; private set; }
    public GameObject Camera { get; private set; }
    public MouseLooking mouseMovement { get; private set; }
    public Movement movement { get; private set; }
    public Interaction interaction { get; private set; }


    public GameObject eye_prefab;

    private void Awake()
    {
        Camera=transform.Find("PlayerCamera").gameObject;
        MainMusicSource = Camera.GetComponent<AudioSource>();
        mouseMovement = GetComponent<MouseLooking>();
        movement = GetComponent<Movement>();
        interaction = GetComponent<Interaction>();
    }


    public void Pauze(bool setPauze)
    {
            mouseMovement.enabled = !setPauze;
    }


    private GameObject _eye;
    public IEnumerator CloseEye(float time)
    {
        mouseMovement.enabled = false;
        _eye = Instantiate(eye_prefab);
        _eye.GetComponent<Animator>().SetBool("close", true);
        yield return new WaitForSeconds(time);
        mouseMovement.enabled = true;
    }

    public IEnumerator OpenEye(float time)
    {
        mouseMovement.enabled = false;
        _eye.GetComponent<Animator>().SetBool("close", false);
        yield return new WaitForSeconds(time);
        Destroy(_eye);
        mouseMovement.enabled = true;
    }
}
