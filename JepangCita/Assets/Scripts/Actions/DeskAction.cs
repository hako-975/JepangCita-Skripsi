using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeskAction : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;

    [SerializeField]
    private Camera actionCam;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject deskCanvas;

    [SerializeField]
    private GameObject deskPanel;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private Button shutdownButton;

    [SerializeField]
    private Animator laptopAnimator;

    // Start is called before the first frame update
    void Start()
    {
        deskCanvas.SetActive(false);
        deskPanel.SetActive(false);

        shutdownButton.onClick.AddListener(ShutdownButton);
        actionButton.onClick.AddListener(ActionButton);
    }

    private void ShutdownButton()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 1;
        canvas.GetComponent<CanvasGroup>().interactable = true;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        actionButton.gameObject.SetActive(true);
        deskPanel.SetActive(false);
        laptopAnimator.SetTrigger("LaptopClose");

        StartCoroutine(WaitLaptopClose());
    }

    private IEnumerator WaitLaptopClose()
    {
        yield return new WaitForSeconds(1f);
        actionCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }

    private void ActionButton()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        canvas.GetComponent<CanvasGroup>().interactable = false;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        mainCam.gameObject.SetActive(false);
        
        actionButton.gameObject.SetActive(false);

        actionCam.gameObject.SetActive(true);
        laptopAnimator.SetTrigger("LaptopOpen");
        StartCoroutine(WaitDeskPanelOpen());
    }

    private IEnumerator WaitDeskPanelOpen()
    {
        yield return new WaitForSeconds(1f);
        deskPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deskCanvas.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deskPanel.SetActive(false);
            deskCanvas.SetActive(false);
        }
    }
}
