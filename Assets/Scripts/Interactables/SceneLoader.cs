using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            ApplicationModelInfo.GameSave.ProgressValue = 0;
            ApplicationModelInfo.GameSave.SceneID++;
            ApplicationModelInfo.GameSave.Save();
            SceneManager.LoadSceneAsync((int)ApplicationModelInfo.GameSave.SceneID);
        }
    }
}
