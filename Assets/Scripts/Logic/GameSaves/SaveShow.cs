using Assets.Scripts.Logic.GameSaves;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveShow : MonoBehaviour
{
    Text text;
    public int id;
    void Awake()
    {
        text = transform.Find("Opis").GetComponent<Text>();
    }

    GameSave load;
    public void Init(GameSave save)
    {
        if (save == null)
        {
            text.text = "Pusty";
            gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            load = save;
            GetComponent<Button>().onClick.AddListener(_load);
            gameObject.GetComponent<Button>().enabled = true;
            text.text = $"Scena: {save.SceneID}";
        }
    }

    void _load()
    {
        ApplicationModelInfo.GameSave = load;
        SceneManager.LoadSceneAsync((int)ApplicationModelInfo.GameSave.SceneID);
    }


    internal void InitNew(GameSave l)
    {
        gameObject.GetComponent<Button>().enabled = true;
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        if (l == null)
        {
            text.text = "Pusty";
        }
        else
        {
            text.text = $"Scena: {l.SceneID}";
        }
        gameObject.GetComponent<Button>().onClick.AddListener(_new);
    }

    void _new()
    {
        ApplicationModelInfo.GameSave = GameSave.NowaGra(this);
        SceneManager.LoadSceneAsync(1);
    }
}
