using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SofaAction : MonoBehaviour
{
    [SerializeField]
    private GameObject sofaCanvas;

    [SerializeField]
    private Button actionButton;

    private GameObject player;

    private Animator playerAnimator;

    private Transform playerTransform;

    private PlayerController playerController;

    private bool sit;

    // Start is called before the first frame update
    void Start()
    {
        sit = false;

        sofaCanvas.SetActive(false);

        actionButton.onClick.AddListener(ActionButton);
    }

    private void ActionButton()
    {
        actionButton.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();

        playerAnimator = player.GetComponentInChildren<Animator>();

        playerTransform = player.GetComponent<Transform>();

        if (sit == false)
        {
            playerController.canMove = false;

            player.GetComponent<CharacterController>().enabled = false;

            playerTransform.SetPositionAndRotation(new Vector3(-7f, 0.1f, playerTransform.position.z), Quaternion.Euler(new Vector3(0f, 90f, 0f)));

            StartCoroutine(WaitAnimStandToSit());

            playerAnimator.SetBool("IsSitting", true);

            sit = true;
        }
        else
        {
            playerController.canMove = true;

            playerAnimator.SetBool("IsSitting", false);

            StartCoroutine(WaitSitToStand());

            sit = false;
        }

        StartCoroutine(WaitShowActionButton());
    }

    IEnumerator WaitShowActionButton()
    {
        yield return new WaitForSeconds(2f);
        actionButton.gameObject.SetActive(true);
    }

    IEnumerator WaitSitToStand()
    {
        yield return new WaitForSeconds(1.25f);
        player.GetComponent<CharacterController>().enabled = true;

        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = new Vector3(-7f, 0.1f, playerTransform.position.z);

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

    IEnumerator WaitAnimStandToSit()
    {
        yield return new WaitForSeconds(1f);

        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = new Vector3(-7.5f, 0.1f, playerTransform.position.z);

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
            sofaCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sofaCanvas.SetActive(false);
        }
    }
}
