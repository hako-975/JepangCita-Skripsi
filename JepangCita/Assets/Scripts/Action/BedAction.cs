using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BedAction : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject canvas;

    private DateTimeController dateTimeController;

    [SerializeField]
    private GameObject bedCanvas;

    [SerializeField]
    private GameObject sleepPanel;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private Button sleepButton;

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

        dateTimeController = canvas.GetComponentInChildren<DateTimeController>();

        transition.gameObject.SetActive(false);

        bedCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);

        sleepButton.onClick.AddListener(SleepButton);
        closeButton.onClick.AddListener(CloseButton);
        cancelButton.onClick.AddListener(CloseButton);

    }

    private void SleepButton()
    {
        soundController.PositiveButtonSound(gameObject);
        // misi ketiga
        PlayerPrefsController.instance.SetMission(2, soundController);

        StartCoroutine(AnimationCloseSleepPanel());

        StartCoroutine(WaitShowActionButton());
    }

    private void CloseButton()
    {
        soundController.NegativeButtonSound(gameObject);

        StartCoroutine(AnimationCloseSleepPanel());
    }

    private void ActionButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationSleepPanel());
    }

    private IEnumerator AnimationSleepPanel()
    {
        Time.timeScale = 0f;
        sleepPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private IEnumerator AnimationCloseSleepPanel()
    {
        Time.timeScale = 1f;
        sleepPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    IEnumerator WaitShowActionButton()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<Animator>().SetBool("IsIdle", false);

        actionButton.gameObject.SetActive(false);
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        transition.gameObject.SetActive(true);
        transition.SetTrigger("Close");
        PlayerPrefsController.instance.SetHour(dateTimeController.gameHour = 23);

        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(3f);
        soundController.RoosterCrowingSound(gameObject);
        ChangeDate();
        player.GetComponentInChildren<Animator>().SetTrigger("IsYawning");

        transition.SetTrigger("Open");

        yield return new WaitForSeconds(4f);
        player.GetComponentInChildren<Animator>().SetBool("IsIdle", true);

        transition.gameObject.SetActive(false);

        canvas.GetComponent<CanvasGroup>().alpha = 1;

        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<CharacterController>().enabled = true;

        actionButton.gameObject.SetActive(true);
    }

    private void ChangeDate()
    {
        dateTimeController.gameDay++;

        if (dateTimeController.gameDay > DateTime.DaysInMonth(dateTimeController.gameYear, dateTimeController.gameMonth))
        {
            dateTimeController.gameDay = 1;
            dateTimeController.gameMonth++;
            // Check if the month exceeds 12
            if (dateTimeController.gameMonth > 12)
            {
                dateTimeController.gameMonth = 1;
                dateTimeController.gameYear++;
                PlayerPrefsController.instance.SetDateYear(dateTimeController.gameYear);
            }

            PlayerPrefsController.instance.SetDateMonth(dateTimeController.gameMonth);
        }

        PlayerPrefsController.instance.SetDateDay(dateTimeController.gameDay);
        PlayerPrefsController.instance.SetHour(dateTimeController.gameHour = 5);
        PlayerPrefsController.instance.SetMinute(dateTimeController.gameMinute = 55);

        dateTimeController.UpdateDateText(dateTimeController.gameYear, dateTimeController.gameMonth, dateTimeController.gameDay, dateTimeController.gameHour, dateTimeController.gameMinute);

        dateTimeController.UpdateSunRotation(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bedCanvas.SetActive(true);
            actionController.canvasTrigger = bedCanvas;
            actionController.isTriggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bedCanvas.SetActive(false);
            transition.gameObject.SetActive(false);
            sleepPanel.GetComponent<CanvasGroup>().alpha = 0;
            sleepPanel.GetComponent<CanvasGroup>().interactable = false;
            sleepPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

            canvas.GetComponent<CanvasGroup>().alpha = 1;
            actionController.isTriggerEntered = false;
        }
    }
}
