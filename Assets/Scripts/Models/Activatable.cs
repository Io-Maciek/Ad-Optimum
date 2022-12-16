using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;


/// <summary>
/// Intended to use by an object <see cref="Activator"/> that fires the <see cref="SetTo(bool, object[])"/> method.
/// </summary>
public abstract class Activatable : MonoBehaviour
{
    /// <value>
    /// Activation status of this game object.
    /// </value>
    public bool IsActive { get; private set; }

    void Awake()
    {
        if (gameObject.tag != "Activatable")
        {
            Debug.LogWarning($"Object {gameObject} has not set tag 'Activatable'");
            gameObject.tag = "Activatable";
        }
    }


    /// <summary>
    /// Starts when some connected gameobject fires it with values <c>true</c> or <c>false</c>.
    /// </summary>
    /// <param name="setValue">Whether to enable <see cref="Activatable"/> object or to disable it.</param>
    /// <param name="args">List of parameters that can be passed to method for processing.</param>
    /// <returns><see cref="Result{E}.Ok"/> if method executed properly or <see cref="Result{E}.Err(E)"/>
    /// when it encountered any error.</returns>
    public abstract Result<string> SetTo(bool setValue, params object[] args);
}
