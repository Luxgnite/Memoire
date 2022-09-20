using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public LayerMask playerLayer;
    public string playerTag;

    private GameObject playerGO;
    private bool displayedHelp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerGO = null;
        foreach (Collider coll in Physics.OverlapSphere(transform.position, 5, playerLayer))
        {
            if (coll.transform.tag.Equals(playerTag))
                playerGO = coll.gameObject; 
        }

        if (playerGO != null && !displayedHelp)
        {
            DisplayHelp(true);
            displayedHelp = true;
        }
        else if (playerGO == null && displayedHelp)
        {
            DisplayHelp(false);
            displayedHelp = false;
        }
    }

    void DisplayHelp(bool show)
    {
        InterfaceManager.Instance.DisplayHelpInteract(show, transform);
    }

    # region Gizmos
    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireSphere(transform.position, 5);

    }
    #endregion

}
