using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomAction : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject classroomCanvas;

    [SerializeField]
    private GameObject goPanel;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private Button goButton;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button cancelButton;

    private ActionController actionController;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        actionController = GetComponentInParent<ActionController>();

        transition.gameObject.SetActive(false);

        classroomCanvas.SetActive(false);


        goButton.onClick.AddListener(GoButton);
        closeButton.onClick.AddListener(CloseButton);
        cancelButton.onClick.AddListener(CloseButton);

    }

    private void GoButton()
    {
        soundController.PositiveButtonSound(gameObject);
        StartCoroutine(AnimationCloseGoPanel());
        StartCoroutine(AnimationGoButton());
    }

    IEnumerator AnimationGoButton()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        canvas.GetComponent<CanvasGroup>().alpha = 0;
        transition.gameObject.SetActive(true);
        transition.SetTrigger("Close");

        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<CharacterController>().enabled = true;
        PlayerPrefsController.instance.DeleteKey("PositionRotationCharacter");
        PlayerPrefsController.instance.SetNextScene("Classroom");
    }

    private void CloseButton()
    {
        soundController.NegativeButtonSound(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AnimationCloseGoPanel());
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(-4.5f, 0.5f, 5.5f);
        player.GetComponent<CharacterController>().enabled = true;
        actionController.DeactiveCanvasAction();
    }

    private IEnumerator AnimationGoPanel()
    {
        Time.timeScale = 0f;
        goPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private IEnumerator AnimationCloseGoPanel()
    {
        Time.timeScale = 1f;
        goPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            classroomCanvas.SetActive(true);
            actionController.canvasTrigger = classroomCanvas;
            actionController.isTriggerEntered = true;
            StartCoroutine(AnimationGoPanel());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            classroomCanvas.SetActive(false);
            transition.gameObject.SetActive(false);
            goPanel.GetComponent<CanvasGroup>().alpha = 0;
            goPanel.GetComponent<CanvasGroup>().interactable = false;
            goPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

            canvas.GetComponent<CanvasGroup>().alpha = 1;
            actionController.isTriggerEntered = false;
        }
    }
}