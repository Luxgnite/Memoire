using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WordExtractorManager : MonoBehaviour
{
    public Canvas canvas;
    public new Camera camera;
    public TextMeshProUGUI text;

    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Awake()
    {
        text = null;
        camera = Camera.main;
        //Singleton ?

    }

    private void Update()
    {
        

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Mouse.current.position.ReadValue();

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            canvas.GetComponent<GraphicRaycaster>().Raycast(m_PointerEventData, results);

            foreach(RaycastResult result in results)
            {
                text = result.gameObject.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    if (TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera) != -1)
                    {
                        int index = TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera);
                        Debug.Log("Intersecting with " + text.textInfo.wordInfo[index].GetWord());
                    }
                    break;
                }
            }
        }
    }

    #region Input Management
    private void EnableInput()
    {

    }

    private void DisableInput()
    {

    }
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }
    #endregion
}
