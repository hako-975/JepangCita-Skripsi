using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TableAction : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject tableCanvas;

    [SerializeField]
    private Button actionButton;

    private GameObject player;

    private Animator playerAnimator;

    private Transform playerTransform;

    private PlayerController playerController;

    private ActionController actionController;

    private bool sit;

    // Start is called before the first frame update
    void Start()
    {
        actionController = GetComponentInParent<ActionController>();

        sit = false;

        tableCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);
    }

    private void ActionButton()
    {
        StartCoroutine(WaitShowActionButton());
    }

    IEnumerator WaitShowActionButton()
    {
        actionButton.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<Animator>().SetBool("IsIdle", false);

        playerController = player.GetComponent<PlayerController>();

        playerAnimator = player.GetComponentInChildren<Animator>();

        playerTransform = player.GetComponent<Transform>();

        if (sit == false)
        {
            StartCoroutine(WaitStandToSit());

            playerAnimator.SetBool("IsSitting", true);

            sit = true;
        }
        else
        {
            playerAnimator.SetBool("IsSitting", false);

            StartCoroutine(WaitSitToStand());

            sit = false;
        }

        yield return new WaitForSeconds(2f);
        actionButton.gameObject.SetActive(true);
    }

    IEnumerator WaitSitToStand()
    {
        yield return new WaitForSeconds(1f);
        playerTransform.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));

        soundController.FootstepSound(gameObject);

        player.GetComponent<CharacterController>().enabled = true;

        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = new Vector3(transform.position.x + 0.525f, 0.25f, transform.position.z);

        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        playerAnimator.SetBool("IsIdle", true);

        playerController.canMove = true;

        playerTransform.position = targetPosition;
    }

    IEnumerator WaitStandToSit()
    {
        playerAnimator.SetBool("IsIdle", false);

        playerController.canMove = false;

        player.GetComponent<CharacterController>().enabled = false;

        playerTransform.SetPositionAndRotation(new Vector3(transform.position.x + 0.525f, 0.25f, transform.position.z), Quaternion.Euler(new Vector3(0f, -90f, 0f)));

        yield return new WaitForSeconds(1f);
        playerTransform.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));

        soundController.SitSound(gameObject);

        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = new Vector3(transform.position.x + 0.525f, 0.25f, transform.position.z);

        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        playerTransform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tableCanvas.SetActive(true);
            actionController.canvasTrigger = tableCanvas;
            actionController.isTriggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tableCanvas.SetActive(false);
            actionController.isTriggerEntered = false;
        }
    }
}
