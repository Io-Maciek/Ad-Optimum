using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// Class responsable for activating objects <see cref="Activator.LoveConnections"/> 
    /// using method <see cref="Activatable.SetTo(bool, object[])"/>.
    /// </summary>
    public abstract class Activator : MonoBehaviour
    {
        /// <value>
        /// List with all dependant <see cref="Activatable"/> objects, that this object <see cref="Activator"/> will process via method
        /// <see cref="Activatable.SetTo(bool, object[])"/>
        /// </value>
        public List<Activatable> LoveConnections = new List<Activatable>();

        void Awake()
        {
            if (LoveConnections.Count == 0)
            {
                Debug.LogWarning($"This object ({gameObject}) does not activate anything (LoveConnections value is 0)");
            }
        }
    }
}
