using UnityEngine;
using UnityEngine.UI;

public class ObjectAction : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject objectCanvas;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private Animator objectAnimatior;

    private ActionController actionController;

    private bool open;

    // Start is called before the first frame update
    void Start()
    {
        actionController = GetComponentInParent<ActionController>();

        open = false;

        objectCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);
    }

    private void ActionButton()
    {
        if (open == false)
        {
            objectAnimatior.Play("Opening");
            soundController.OpenSound(gameObject);
            open = true;
        }
        else
        {
            objectAnimatior.Play("Closing");
            soundController.CloseSound(gameObject);

            open = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectCanvas.SetActive(true);
            actionController.canvasTrigger = objectCanvas;
            actionController.isTriggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectCanvas.SetActive(false);
            actionController.isTriggerEntered = false;
        }
    }
}
