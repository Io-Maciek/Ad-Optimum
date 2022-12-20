using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReversal : MonoBehaviour
{
    float prev;
    float vol;

    bool wasSeen = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!wasSeen && other.tag == "Player")
        {
            Controller c = other.GetComponent<Controller>();
            prev = c.MainMusicSource.pitch;
            vol = c.MainMusicSource.volume;
            c.MainMusicSource.pitch = -prev+.2f;
            c.MainMusicSource.volume = vol + .1f;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!wasSeen && other.tag == "Player")
        {
            Controller c = other.GetComponent<Controller>();
            c.MainMusicSource.pitch = prev;
            c.MainMusicSource.volume = vol;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<BoxCollider>().size = Vector3.one * 2;
            wasSeen = true;
        }
    }
}
