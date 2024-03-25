using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoHomeAction : MonoBehaviour
{
    [SerializeField]
    private AdsController adsController;

    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject goHomeCanvas;

    [SerializeField]
    private GameObject goHomePanel;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private Button goHomeButton;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button cancelButton;

    private GameObject player;

    private ActionController actionController;

    // Start is called before the first frame update
    void Start()
    {
        actionController = GetComponentInParent<ActionController>();

        transition.gameObject.SetActive(false);

        goHomeCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);

        goHomeButton.onClick.AddListener(GoHomeButton);
        closeButton.onClick.AddListener(CloseButton);
        cancelButton.onClick.AddListener(CloseButton);
    }

    private void GoHomeButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationCloseGoHomePanel());

        StartCoroutine(WaitShowActionButton());
    }

    private void CloseButton()
    {
        soundController.NegativeButtonSound(gameObject);

        StartCoroutine(AnimationCloseGoHomePanel());
    }

    private void ActionButton()
    {
        soundController.PositiveButtonSound(gameObject);
        adsController.ShowInterstitialAd();
        StartCoroutine(AnimationGoHomePanel());
    }

    private IEnumerator AnimationGoHomePanel()
    {
        Time.timeScale = 0f;
        goHomePanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private IEnumerator AnimationCloseGoHomePanel()
    {
        Time.timeScale = 1f;
        goHomePanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    IEnumerator WaitShowActionButton()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        actionButton.gameObject.SetActive(false);
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        transition.gameObject.SetActive(true);
        transition.SetTrigger("Close");

        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<CharacterController>().enabled = true;
        PlayerPrefsController.instance.DeleteKey("PositionRotationCharacter");
        PlayerPrefsController.instance.SetPositionRotationCharacter(new Vector3(-4.5f, 0.5f, 5.5f), new Quaternion(0f, 180f, 0f, 1f));

        PlayerPrefsController.instance.SetNextScene("Home");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goHomeCanvas.SetActive(true);
            actionController.canvasTrigger = goHomeCanvas;
            actionController.isTriggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goHomeCanvas.SetActive(false);
            transition.gameObject.SetActive(false);
            goHomePanel.GetComponent<CanvasGroup>().alpha = 0;
            goHomePanel.GetComponent<CanvasGroup>().interactable = false;
            goHomePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

            canvas.GetComponent<CanvasGroup>().alpha = 1;
            actionController.isTriggerEntered = false;
        }
    }
}
