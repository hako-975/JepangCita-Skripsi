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

    private ActionController actionController;

    private GameObject player;

    private PlayerController playerController;

    bool isActiveDesk = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        actionController = GetComponentInParent<ActionController>();
        playerController = player.GetComponent<PlayerController>();

        deskCanvas.SetActive(false);
        deskPanel.SetActive(false);

        shutdownButton.onClick.AddListener(ShutdownButton);
        actionButton.onClick.AddListener(ActionButton);
    }

    void Update()
    {
        if (isActiveDesk)
        {
            deskCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private void ShutdownButton()
    {
        isActiveDesk = false;

        playerController.canMove = true;

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
        isActiveDesk = true;
        playerController.canMove = false;

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
        deskCanvas.SetActive(true);
        deskPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deskCanvas.SetActive(true);
            actionController.canvas = deskCanvas;
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
