using UnityEngine;
using UnityEngine.UI;

public class ObjectAction : MonoBehaviour
{
    [SerializeField]
    private GameObject objectCanvas;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private Animator objectAnimatior;

    private bool open;

    // Start is called before the first frame update
    void Start()
    {
        open = false;

        objectCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);
    }

    private void ActionButton()
    {
        if (open == false)
        {
            objectAnimatior.Play("Opening");
            open = true;
        }
        else
        {
            objectAnimatior.Play("Closing");
            open = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectCanvas.SetActive(false);
        }
    }
}
