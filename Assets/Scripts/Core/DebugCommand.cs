using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    #region Private Fields
    /// <summary>
    /// ID of the command
    /// </summary>
    private string _commandId;
    /// <summary>
    /// Description of the command
    /// </summary>
    private string _commandDescription;
    /// <summary>
    /// Format of the command
    /// </summary>
    private string _commandFormat;
    #endregion

    #region Public Fields
    /// <summary>
    /// ID of the command
    /// </summary>
    public string commandId { get { return _commandId; } }
    /// <summary>
    /// Description of the command
    /// </summary>
    public string commandDescription { get { return _commandDescription; } }
    /// <summary>
    /// Format of the command
    /// </summary>
    public string commandFormat { get { return _commandFormat; } }
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">ID of the command</param>
    /// <param name="description">Description of the command</param>
    /// <param name="format">Format of the command</param>
    public DebugCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    /// <summary>
    /// Function delegate to execute when command is called
    /// </summary>
    private Action command;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">ID of the command</param>
    /// <param name="description">Description of the command</param>
    /// <param name="format">Format of the command</param>
    /// <param name="command"></param>
    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}