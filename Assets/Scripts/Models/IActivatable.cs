using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public interface IActivatable
    {
        /// <summary>
        /// Starts when some connected gameobject fires it with values <c>true</c> or <c>false</c>.
        /// </summary>
        /// <param name="setValue">Whether to enable <see cref="Activatable"/> object or to disable it.</param>
        /// <param name="args">List of parameters that can be passed to method for processing.</param>
        /// <returns><see cref="Result{E}.Ok"/> if method executed properly or <see cref="Result{E}.Err(E)"/>
        /// when it encountered any error.</returns>
        public abstract Result<object, object> SetTo(bool setValue, params object[] args);
    }
}
