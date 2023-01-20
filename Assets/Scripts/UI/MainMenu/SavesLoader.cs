using Assets.Scripts.Logic.GameSaves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SavesLoader : MonoBehaviour
{
    SaveShow[] saves;
    void Start()
    {
        transform.Find("goback").GetComponent<Button>().onClick.AddListener(back);
    }

    void back()
    {
        transform.parent.Find("Main").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void New()
    {
        saves = GetComponentsInChildren<SaveShow>().OrderBy(x => x.id).ToArray();
        var io = new IoGameSerialier();
        var loaded = io.Read().OrderBy(x => x.id).ToArray();

        foreach (var save in saves)
        {
            save.InitNew(null);
        }

        foreach (var s in saves)
        {
            foreach (var l in loaded)
            {
                if (s.id == l.id)
                    s.InitNew(l);
            }
        }
    }

    public void Load()
    {
        saves = GetComponentsInChildren<SaveShow>().OrderBy(x => x.id).ToArray();
        var io = new IoGameSerialier();
        var loaded = io.Read().OrderBy(x => x.id).ToArray();
        foreach (var s in saves)
        {
            s.GetComponent<Button>().onClick.RemoveAllListeners();
            s.Init(null);
        }

        foreach (var s in saves)
        {
            foreach (var l in loaded)
            {
                if (s.id == l.id)
                    s.Init(l);
            }
        }
    }
}
