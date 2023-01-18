using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public Button btnNewGame;
    public Button btnLoadGame;
    public Button btnExitGame;
    public Button btnShowOpcje;
    public GameObject MenuMain;

    public GameObject opcjePrefab;
    public new AudioSource audio;


    void Start()
    {
        btnNewGame.onClick.AddListener(btnNew);
        btnLoadGame.onClick.AddListener(btnLoad);
        btnExitGame.onClick.AddListener(btnExit);
        btnShowOpcje.onClick.AddListener(btnOpcje);

        audio = FindObjectOfType<AudioSource>();
        audio.volume = 1.0f * PlayerPrefs.GetFloat("muzyka", 1.0f);


        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    void btnOpcje()
    {
        var x = Instantiate(opcjePrefab, transform);
        x.GetComponent<OpcjeMenus>().poprzedniEkran = MenuMain;
        MenuMain.SetActive(false);
    }

    void btnExit()
    {
        Application.Quit();
    }

    void btnNew()
    {
        ApplicationModelInfo.GameSave = GameSave.NowaGra(0);
        ApplicationModelInfo.GameSave.Save();
        SceneManager.LoadSceneAsync(1);
    }

    void btnLoad()
    {
        var io = new IoGameSerialier();
        var files = io.Read();
        if (files.Count > 0)
        {
            var _0 = files.Where(g => g.id == 0).First();
            if (_0 != null)
            {
                ApplicationModelInfo.GameSave = _0;
                SceneManager.LoadSceneAsync((int)ApplicationModelInfo.GameSave.SceneID);
            }
        }
    }
}
