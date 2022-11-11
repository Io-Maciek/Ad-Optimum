using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Intended to use on object with 'Interactable' tag. Fires the <see cref="Action(object[])"/> method when player interacts with it.
/// </summary>
public abstract class Interactable : MonoBehaviour// TODO implement OK/Error returning for error handling
{
    void Awake()
    {
        if (gameObject.tag != "Interactable")
        {
            Debug.LogWarning($"Object {gameObject} has not set tag 'Interactable'");
            gameObject.tag = "Interactable";
        }
    }

    /// <summary>
    /// Starts when players interacts with object with game object with 'Interactable' tag.
    /// </summary>
    /// <param name="args">List of parameters that can be passed to method for processing.</param>
    public abstract Result<string> Action(params object[] args);
}
