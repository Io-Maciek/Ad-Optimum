using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that handles return values from methods in form of <see cref="Ok"/> and <see cref="Err(E)"/> using method <see cref="Match(System.Action{E})"/>.
/// </summary>
public class Result<E>
{
    bool IsOk;
    E ErrorItem;

    private Result() 
    {
        IsOk = true;
    }
    private Result(E item)
    {
        IsOk = false;
        ErrorItem = item;
    }


    /// <summary>
    /// Initializes class <see cref="Result{E}"/> with Ok value. Helps with conditional statement <see cref="Match(Action{E})"/> and <see cref="Match(Action, Action{E})"/>.
    /// </summary>
    public static Result<E> Ok()
    {
        return new Result<E>();
    }

    /// <summary>
    /// Initializes class <see cref="Result{E}"/> with Err value. Helps with conditional statement <see cref="Match(Action{E})"/> and <see cref="Match(Action, Action{E})"/>.
    /// </summary>
    public static Result<E> Err(E item)
    {
        return new Result<E>(item);
    }

    /// <summary>
    /// Method that allows for conditional method initializer if passed values was ERR.
    /// </summary>
    /// <param name="ErrorHandler">Starts if valies was passed via <see cref="Err(E)"/></param>
    public void Match(Action<E> ErrorHandler)
    {
        if (!IsOk)
        {
            ErrorHandler(ErrorItem);
        }
    }

    /// <summary>
    /// Method that allows for conditional method initializer dependent on passed value - OK or ERR.
    /// </summary>
    /// <param name="OkHandler">Starts if value was passed via <see cref="Ok"/></param>
    /// <param name="ErrorHandler">Starts if valies was passed via <see cref="Err(E)"/></param>
    public void Match(Action OkHandler,Action<E> ErrorHandler)
    {
        if (IsOk)
        {
            OkHandler();
        }
        else
        {
            ErrorHandler(ErrorItem);
        }
    }
}
