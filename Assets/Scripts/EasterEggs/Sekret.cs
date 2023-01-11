using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EasterEggs
{
    public abstract class Sekret : MonoBehaviour
    {
        public const int ILOSC_SEKRETOW = 1;


        public bool seen { get; set; }= false;
        public short sekret_id;


        public abstract void Open();
        public abstract void Close();
        public abstract void Activate();
    }
}
