using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnNewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(click);
    }

    void click()
    {
        ApplicationModelInfo.GameSave = GameSave.NowaGra(0);
        ApplicationModelInfo.GameSave.Save();
        SceneManager.LoadSceneAsync(1);
    }
}
