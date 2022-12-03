using System;
using static UnityEditor.Progress;

/// <summary>
/// Class that handles return values in <see href="https://www.rust-lang.org/">Rust</see>-Like style from methods in form of <see cref="Ok"/> and <see cref="Err(E)"/> using method <see cref="Match(System.Action, System.Action{E})"/>.
/// <para>
/// If we want to pass some object with the OK value, the class <see cref="Result{T, E}"/> should be used.
/// </para>
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
    /// Method that allows for conditional method initializer if passed value was OK.
    /// </summary>
    /// <param name="OkHandler">Starts if valies was passed via <see cref="Ok"/></param>
    public void Match(Action OkHandler)
    {
        if (IsOk)
        {
            OkHandler();
        }
    }

    /// <summary>
    /// Method that allows for conditional method initializer if passed value was ERR.
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
    public void Match(Action OkHandler, Action<E> ErrorHandler)
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


    /// <summary>
    /// Method that allows for conditional method initializer dependent on passed value - OK or ERR.
    /// </summary>
    /// <param name="OkHandler">Starts if value was passed via <see cref="Ok"/></param>
    /// <param name="ErrorHandler">Starts if valies was passed via <see cref="Err(E)"/></param>
    /// <returns>Object of type <typeparamref name="R"/></returns>
    public R Match<R>(Func<R> OkHandler, Func<E, R> ErrorHandler)
    {
        if (IsOk)
        {
            return OkHandler();
        }
        else
        {
            return ErrorHandler(ErrorItem);
        }
    }


    /// <summary>
    /// Gets ERR value <typeparamref name="E"/>. Implicit casting is allowed.
    /// <para>
    /// Throws <see cref="InvalidCastException"/> if its value is OK.
    /// </para>
    /// </summary>
    /// <exception cref="InvalidCastException"></exception>
    public E GetError()
    {
        if (!IsOk)
            return ErrorItem;
        else
            throw new InvalidCastException("Result does not have ERR value");
    }

    public static implicit operator E(Result<E> _this)
    {
        return _this.GetError();
    }
}

/// <summary>
/// Class that handles return values in <see href="https://www.rust-lang.org/">Rust</see>-Like style from methods in form of <see cref="Ok(T)"/> and <see cref="Err(E)"/> using method <see cref="Match(Action{E})"/>.
/// <para>
/// If OK value <typeparamref name="T"/> could be ignored, class <see cref="Result{E}"/> should be used.
/// </para>
/// <para>
/// Can also be parsed to <see cref="Result{E}"/> via methods <see cref="IgnoreOk"/> or implicit casting.
/// </para>
/// </summary>
public class Result<T, E>
{
    bool IsOk;
    E ErrorItem;
    T OkItem;

    private Result(T item)
    {
        IsOk = true;
        OkItem = item;
    }
    private Result(E item)
    {
        IsOk = false;
        ErrorItem = item;
    }


    /// <summary>
    /// Initializes class <see cref="Result{E}"/> with Ok value. Helps with conditional statement <see cref="Match(Action{E})"/> and <see cref="Match(Action, Action{E})"/>.
    /// </summary>
    public static Result<T, E> Ok(T ok)
    {
        return new Result<T, E>(ok);
    }

    /// <summary>
    /// Initializes class <see cref="Result{E}"/> with Err value. Helps with conditional statement <see cref="Match(Action{E})"/> and <see cref="Match(Action, Action{E})"/>.
    /// </summary>
    public static Result<T, E> Err(E err)
    {
        return new Result<T, E>(err);
    }

    /// <summary>
    /// Method that allows for conditional method initializer if passed value was ERR.
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
    /// Method that allows for conditional method initializer if passed value was OK.
    /// </summary>
    /// <param name="OkHandler">Starts if valies was passed via <see cref="Ok(T)"/></param>
    public void Match(Action<T> OkHandler)
    {
        if (IsOk)
        {
            OkHandler(OkItem);
        }
    }


    /// <summary>
    /// Method that allows for conditional method initializer dependent on passed values - OK or ERR.
    /// </summary>
    /// <param name="OkHandler">Starts if value was passed via <see cref="Ok(T)"/></param>
    /// <param name="ErrorHandler">Starts if valies was passed via <see cref="Err(E)"/></param>
    public virtual void Match(Action<T> OkHandler, Action<E> ErrorHandler)
    {
        if (IsOk)
        {
            OkHandler(OkItem);
        }
        else
        {
            ErrorHandler(ErrorItem);
        }
    }

    /// <summary>
    /// Method that allows for conditional method initializer dependent on passed values - OK or ERR.
    /// </summary>
    /// <param name="OkHandler">Starts if value was passed via <see cref="Ok(T)"/></param>
    /// <param name="ErrorHandler">Starts if valies was passed via <see cref="Err(E)"/></param>
    /// <returns>Object of type <typeparamref name="R"/></returns>
    public virtual R Match<R>(Func<T, R> OkHandler, Func<E, R> ErrorHandler)
    {
        if (IsOk)
        {
            return OkHandler(OkItem);
        }
        else
        {
            return ErrorHandler(ErrorItem);
        }
    }

    /// <summary>
    /// Allows for conversion to type <see cref="Result{E}"/> that ignores its OK value <typeparamref name="T"/>.
    /// </summary>
    public Result<E> IgnoreOk()
    {
        if (IsOk)
            return Result<E>.Ok();
        else
            return Result<E>.Err(ErrorItem);
    }

    public static implicit operator Result<E>(Result<T, E> _this)
    {
        return _this.IgnoreOk();
    }


    /// <summary>
    /// Gets ERR value <typeparamref name="E"/>. Implicit casting is allowed.
    /// <para>
    /// Throws <see cref="InvalidCastException"/> if its value is OK <typeparamref name="T"/>.
    /// </para>
    /// </summary>
    /// <exception cref="InvalidCastException"></exception>
    public E GetError()
    {
        if (!IsOk)
            return ErrorItem;
        else
            throw new InvalidCastException("Result does not have ERR value");
    }

    public static implicit operator E(Result<T, E> _this)
    {
        return _this.GetError();
    }


    /// <summary>
    /// Gets OK value <typeparamref name="T"/>. Implicit casting is allowed.
    /// <para>
    /// Throws <see cref="InvalidCastException"/> if its value is ERR <typeparamref name="E"/>.
    /// </para>
    /// </summary>
    /// <exception cref="InvalidCastException"></exception>
    public T GetOk()
    {
        if (IsOk)
            return OkItem;
        else
            throw new InvalidCastException("Result does not have OK value");
    }

    public static implicit operator T(Result<T, E> _this)
    {
        return _this.GetOk();
    }
}