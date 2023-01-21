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
            var _c = other.GetComponent < Controller > ();

            _c.StartCoroutine("CloseEye", 1.0f);
            StartCoroutine("load", _c);


        }
    }

    IEnumerator load(Controller _c)
    {
        _c.movement.enabled = false;
        _c.mouseMovement.enabled = false;
        yield return _c.StartCoroutine("CloseEye", 1.0f);
        _c.movement.enabled = true;
        _c.mouseMovement.enabled = true;
        ApplicationModelInfo.GameSave.ProgressValue = 0;
        ApplicationModelInfo.GameSave.SceneID++;
        ApplicationModelInfo.GameSave.Save();
        SceneManager.LoadSceneAsync((int)ApplicationModelInfo.GameSave.SceneID);
    }
}
