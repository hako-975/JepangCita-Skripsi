using UnityEngine;

public class ActionController : MonoBehaviour
{
    public GameObject[] canvasAction;

    [HideInInspector]
    public GameObject canvasTrigger;

    [HideInInspector]
    public bool isTriggerEntered = false;

    [HideInInspector]
    public bool deskIsActive = false;

    [HideInInspector]
    public bool isMiddleClass = false;

    public void ActiveCanvasAction()
    {
        if (canvasTrigger != null)
        {
            if (isMiddleClass == false)
            {
                canvasTrigger.SetActive(isTriggerEntered);
            }
        }
    }

    public void DeactiveCanvasAction()
    {
        for (int i = 0; i < canvasAction.Length; i++)
        {
            canvasAction[i].SetActive(false);
        }
    }
}
