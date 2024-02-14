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

    public void ActiveCanvasAction()
    {
        if (canvasTrigger != null)
        {
            canvasTrigger.SetActive(isTriggerEntered);
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
