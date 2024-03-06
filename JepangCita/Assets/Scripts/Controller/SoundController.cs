using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField]
    private AudioClip startButtonSound;
    [SerializeField]
    private AudioClip positiveButtonSound;
    [SerializeField]
    private AudioClip negativeButtonSound;
    [SerializeField]
    private AudioClip resetButtonSound;
    [SerializeField]
    private AudioClip minimizeButtonSound;
    [SerializeField]
    private AudioClip trashButtonSound;


    [Header("Environment")]
    [SerializeField]
    private AudioClip missionSucceedSound;
    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;
    [SerializeField]
    private AudioClip shutdownSound;
    [SerializeField]
    private AudioClip schoolBellSound;
    [SerializeField]
    private AudioClip roosterCrowingSound;

    [Header("Human")]
    [SerializeField]
    private AudioClip footstepSound;
    [SerializeField]
    private AudioClip sitSound;

    bool beingHandled = false;

    [SerializeField]
    private AudioMixerGroup soundMixerGroup;
    [SerializeField]
    private AudioMixer soundMixer;

    private void AudioSourceComponent(GameObject gameObject, AudioClip audioClip)
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            AudioSource audioSourceAdd = gameObject.AddComponent<AudioSource>();
            audioSourceAdd.playOnAwake = false;
            audioSourceAdd.volume = 1f;
            audioSourceAdd.outputAudioMixerGroup = soundMixerGroup;
        }

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    
    public void StartButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, startButtonSound);
    }

    public void PositiveButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, positiveButtonSound);
    }

    public void NegativeButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, negativeButtonSound);
    }

    public void ResetButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, resetButtonSound);
    }

    public void MinimizeButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, minimizeButtonSound);
    }

    public void TrashButtonSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, trashButtonSound);
    }

    public void MissionSucceedSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, missionSucceedSound);
    }

    public void OpenSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, openSound);
    }

    public void CloseSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, closeSound);
    }

    public void ShutdownSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, shutdownSound);
    }

    public void SchoolBellSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, schoolBellSound);
    }

    public void RoosterCrowingSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, roosterCrowingSound);
    }

    public void FootstepSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, footstepSound);
    }

    public AudioSource WalkSound(GameObject gameObject)
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            AudioSource audioSourceAdd = gameObject.AddComponent<AudioSource>();
            audioSourceAdd.playOnAwake = false;
            audioSourceAdd.volume = 1f;
            audioSourceAdd.outputAudioMixerGroup = soundMixerGroup;
        }

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = footstepSound;
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);

        if (!beingHandled)
        {
            StartCoroutine(DelayWalkSound(audioSource));
        }

        return audioSource;
    }

    IEnumerator DelayWalkSound(AudioSource audioSource)
    {
        beingHandled = true;
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
        beingHandled = false;
    }

    public void SitSound(GameObject gameObject)
    {
        AudioSourceComponent(gameObject, sitSound);
    }
}
