using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AudioSource MainMusicSource { get; private set; }
    public GameObject Camera { get; private set; }

    private void Awake()
    {
        Camera=transform.Find("PlayerCamera").gameObject;
        MainMusicSource = Camera.GetComponent<AudioSource>();
    }
}
