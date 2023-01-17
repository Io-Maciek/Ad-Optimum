using Assets.Scripts.EasterEggs;
using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicReversal : Sekret
{
    float prev;
    float vol;

    public override void Activate()
    {
        ApplicationModelInfo.GameSave.AddSecret(sekret_id);
    }

    public override void Close()
    {
        GetComponent<Collider>().isTrigger = false;
        GetComponent<BoxCollider>().size = Vector3.one * 2;
    }

    public override void Open()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!seen && other.tag == "Player")
        {
            Controller c = other.GetComponent<Controller>();
            prev = c.MainMusicSource.pitch;
            //vol = c.MainMusicSource.volume;
            c.MainMusicSource.pitch = -prev+.2f;
            //c.MainMusicSource.volume = vol * 1.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!seen && other.tag == "Player")
        {
            Controller c = other.GetComponent<Controller>();
            c.MainMusicSource.pitch = prev;
            //c.MainMusicSource.volume = vol;
            Close();      
            seen = true;
        }
    }
}
