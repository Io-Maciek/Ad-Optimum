using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic.GameSaves
{
    public class TestCubeREAD : MonoBehaviour
    {
        private void Start()
        {
/*            GameSave g = new GameSave() { id = 9, ProgressValue = 99, SayMyName = "Filename123", SceneName = "Scenatestowa1" };
            StreamWriter sw = new StreamWriter(Path.Combine(_GAME, "save2.io"));
            IoDeSer.IoFile.WriteToFile(g, sw);
            sw.Close();*/

            // TODO real main menu with save files
            var io = new IoGameSerialier();
            foreach (var item in io.Read())
            {
                Debug.Log(item);
            }

        }
    }
}
