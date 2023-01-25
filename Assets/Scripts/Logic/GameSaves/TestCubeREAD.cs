 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic.GameSaves
{
    [Obsolete]
    public class TestCubeREAD : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 1.0f;


            var io = new IoGameSerialier();
            foreach (var item in io.Read())
            {
                Debug.Log(item);
            }
        }
    }
}
