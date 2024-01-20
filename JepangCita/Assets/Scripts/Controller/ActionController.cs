using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    GameObject[] canvasAction;

    public GameObject canvas;

    public void ActiveCanvasAction()
    {
        canvas.SetActive(true);
    }

    public void DeactiveCanvasAction()
    {
        for (int i = 0; i < canvasAction.Length; i++)
        {
            canvasAction[i].SetActive(false);
        }
    }
}
