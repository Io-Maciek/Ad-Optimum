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
    public Animator anim { get; private set; }


    public PlayerUI playerUI;
    public GameObject eye_prefab;


    AudioSource audio;
    float music_def;

    private void Awake()
    {
        Camera=transform.Find("PlayerCamera").gameObject;
        anim = Camera.GetComponent<Animator>();
        anim.enabled = false;
        MainMusicSource = Camera.GetComponent<AudioSource>();
        mouseMovement = GetComponent<MouseLooking>();
        movement = GetComponent<Movement>();
        interaction = GetComponent<Interaction>();

        audio = Camera.GetComponent<AudioSource>();
        music_def = audio.volume;
    }


    public void Pauze(bool setPauze)
    {
            mouseMovement.enabled = !setPauze;
    }


    private GameObject _eye;
    public IEnumerator CloseEye(float time)
    {
        _eye = Instantiate(eye_prefab);
        _eye.GetComponent<Animator>().SetBool("close", true);
        yield return new WaitForSeconds(time);
    }

    public IEnumerator OpenEye(float time)
    {
        _eye.GetComponent<Animator>().SetBool("close", false);
        yield return new WaitForSeconds(time);
        Destroy(_eye);
    }

    public float CameraLook(float x)
    {
        return GetComponent<MouseLooking>().LookAt(x);
    }

    public void CameraSet(float x)
    {
        GetComponent<MouseLooking>().SetTo(x);
    }



    public void MusicSet(float new_value)
    {
        audio.volume = music_def*new_value;
    }
}
