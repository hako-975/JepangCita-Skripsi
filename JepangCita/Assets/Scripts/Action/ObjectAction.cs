using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ObjectAction : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup soundMixer;

    [SerializeField]
    private AudioClip openClip;

    [SerializeField]
    private AudioClip closeClip;

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
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = soundMixer;

        if (open == false)
        {
            objectAnimatior.Play("Opening");
            StartCoroutine(PlayAndDestroy(openClip));
            open = true;
        }
        else
        {
            objectAnimatior.Play("Closing");
            StartCoroutine(PlayAndDestroy(closeClip));
            open = false;
        }
    }

    IEnumerator PlayAndDestroy(AudioClip clip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length+0.1f);
        Destroy(audioSource);
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
