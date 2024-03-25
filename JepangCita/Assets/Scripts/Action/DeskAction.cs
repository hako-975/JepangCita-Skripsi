using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeskAction : MonoBehaviour
{
    [SerializeField]
    private AdsController adsController;

    [SerializeField]
    private SoundController soundController;

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
        if (actionController.deskIsActive)
        {
            deskCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private void ShutdownButton()
    {
        soundController.ShutdownSound(gameObject);

        StartCoroutine(WaitLaptopClose());
    }

    private IEnumerator WaitLaptopClose()
    {
        actionController.deskIsActive = false;
        playerController.canMove = true;

        canvas.GetComponent<CanvasGroup>().alpha = 1;
        canvas.GetComponent<CanvasGroup>().interactable = true;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        actionButton.gameObject.SetActive(true);
        deskPanel.SetActive(false);
        laptopAnimator.SetTrigger("LaptopClose");
        yield return new WaitForSeconds(0.5f);
        soundController.CloseSound(gameObject);
        yield return new WaitForSeconds(1f);
        actionCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }

    private void ActionButton()
    {
        soundController.OpenSound(gameObject);

        StartCoroutine(WaitDeskPanelOpen());
    }

    private IEnumerator WaitDeskPanelOpen()
    {
        actionController.deskIsActive = true;
        playerController.canMove = false;

        canvas.GetComponent<CanvasGroup>().alpha = 0;
        canvas.GetComponent<CanvasGroup>().interactable = false;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        mainCam.gameObject.SetActive(false);

        actionButton.gameObject.SetActive(false);

        actionCam.gameObject.SetActive(true);

        laptopAnimator.SetTrigger("LaptopOpen");
        yield return new WaitForSeconds(1f);
        deskCanvas.SetActive(true);
        deskPanel.SetActive(true);
        adsController.ShowInterstitialAd();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deskCanvas.SetActive(true);
            actionController.canvasTrigger = deskCanvas;
            actionController.isTriggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deskPanel.SetActive(false);
            deskCanvas.SetActive(false);
            actionController.isTriggerEntered = false;
        }
    }
}
