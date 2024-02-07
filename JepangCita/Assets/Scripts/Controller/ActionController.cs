using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    GameObject[] canvasAction;

    public GameObject canvas;

    [HideInInspector]
    public bool isActionActive = false;

    public void ActiveCanvasAction()
    {
        canvas.SetActive(true);
        isActionActive = true;
    }

    public void DeactiveCanvasAction()
    {
        for (int i = 0; i < canvasAction.Length; i++)
        {
            canvasAction[i].SetActive(false);
        }
     
        isActionActive = false;
    }
}
