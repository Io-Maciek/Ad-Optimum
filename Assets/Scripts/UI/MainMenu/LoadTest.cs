using Assets.Scripts.Logic.GameSaves;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadTest : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(click);
    }

    void click()
    {
        var io = new IoGameSerialier();
        var files = io.Read();
        if (files.Count > 0)
        {
            var _0 = files.Where(g => g.id == 0).First();
            if (_0 != null)
            {
                ApplicationModelInfo.GameSave = _0 ;
                SceneManager.LoadSceneAsync((int)ApplicationModelInfo.GameSave.SceneID);
            }
        }
    }
}
