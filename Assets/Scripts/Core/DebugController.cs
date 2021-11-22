using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// List of all the commands
    /// </summary>
    public List<object> commandList;
    #endregion

    #region Commands
    public static DebugCommand TEST_COMMAND;
    #endregion

    #region Private Fields
    /// <summary>
    /// Display state of the console ; true is displayed, false is not.
    /// </summary>
    bool showConsole;
    /// <summary>
    /// Text input by user in the text field
    /// </summary>
    string input;
    /// <summary>
    /// Input controller system
    /// </summary>
    Controls controls;
    #endregion

    private void Awake()
    {
        controls = new Controls();

        //Initialize Commands
        TEST_COMMAND = new DebugCommand("test_command", "A test command", "test", () =>
        {
            GameManager._instance.Test();
        });

        //Initialize Commands List
        commandList = new List<object>
        {
            TEST_COMMAND
        };
    }

    private void Start()
    {
        controls.Player.ToggleDebug.performed += ctx => OnToggleDebug();
        controls.Player.ConfirmInput.performed += ctx => OnReturn();
    }

    private void HandleInput()
    {
        for(int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if(input.Contains(commandBase.commandId))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }

    private void OnGUI()
    {
        //If console is not displayed, stop the function.
        if(!showConsole) { return; }

        //Else, we draw it on the UI
        //Input field of the console
        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

    }

    #region Input Callbacks
    /// <summary>
    /// Called when the ToggleDebug key is pressed
    /// </summary>
    /// <param name="value"></param>
    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    /// <summary>
    /// Called when the Return button is pressed
    /// </summary>
    public void OnReturn()
    {
        if(showConsole)
        {
            HandleInput();
            input = "";
        }
    }
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    #endregion
}
