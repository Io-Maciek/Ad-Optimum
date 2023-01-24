using Assets.Scripts.Logic.GameSaves;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelClickEnd : Interactable, IActivatable
{
    GameObject ErrorPanel;
    bool hasEnergy = false;

    private void Start()
    {
        ErrorPanel = transform.Find("Err").gameObject;
    }

    public override Result<object, string> Action(params object[] args)
    {
        if (hasEnergy)
        {
            var _c = (args[0] as GameObject).GetComponent<Controller>();
            StartCoroutine("load", _c);

            return Result<string>.Ok();
        }
        else
        {
            return Result<object, string>.Err("Nie ma wystarczaj¹cej energii.");
        }
    }




    public Result<object, object> SetTo(bool setValue, params object[] args)
    {
        hasEnergy = setValue;
        IsImportant = setValue;
        ErrorPanel.SetActive(!setValue);

        return Result<object>.Ok();
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
