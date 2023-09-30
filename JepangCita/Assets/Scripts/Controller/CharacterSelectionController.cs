using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject boyCharacter;

    [SerializeField]
    private GameObject girlCharacter;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    [SerializeField]
    private Button selectCharacterButton;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private GameObject formInput;

    [SerializeField]
    private TMP_InputField nameInputField;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private GameObject transition;

    private bool currentGirl = true;

    // Start is called before the first frame update
    void Start()
    {
        boyCharacter.GetComponent<Animator>().SetTrigger("IsSitting");
        girlCharacter.GetComponent<Animator>().SetTrigger("IsSitting");

        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);
        selectCharacterButton.onClick.AddListener(OnSelectCharacterButtonClick);
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);

        backButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        selectCharacterButton.gameObject.SetActive(true);

        if (currentGirl)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
        }
    }

    private void OnLeftButtonClick()
    {
        currentGirl = true;
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(true);
        cam.GetComponent<Animator>().SetTrigger("ToGirl");
    }

    private void OnRightButtonClick()
    {
        currentGirl = false;
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(false);
        cam.GetComponent<Animator>().SetTrigger("ToBoy");
    }

    private void OnSelectCharacterButtonClick()
    {
        backButton.gameObject.SetActive(true);

        if (currentGirl)
        {
            nameInputField.text = "Fulana";
            cam.GetComponent<Animator>().SetTrigger("FromGirlToTv");
        } 
        else
        {
            nameInputField.text = "Fulan";
            cam.GetComponent<Animator>().SetTrigger("FromBoyToLaptop");
        }

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        selectCharacterButton.gameObject.SetActive(false);
        StartCoroutine(DelayDisplayForm());
    }

    private void OnBackButtonClick()
    {
        backButton.gameObject.SetActive(false);
        selectCharacterButton.gameObject.SetActive(true);

        if (currentGirl)
        {
            cam.GetComponent<Animator>().SetTrigger("FromTvToGirl");
            rightButton.gameObject.SetActive(true);
        }
        else
        {
            cam.GetComponent<Animator>().SetTrigger("FromLaptopToBoy");
            leftButton.gameObject.SetActive(true);
        }

        formInput.SetActive(false);
    }

    private void OnSubmitButtonClick()
    {
        PlayerPrefsController.instance.SetCharacterName(nameInputField.text);
        if (currentGirl)
        {
            PlayerPrefsController.instance.SetCharacterSelection("Girl");
        }
        else
        {
            PlayerPrefsController.instance.SetCharacterSelection("Boy");
        }

        transition.GetComponent<Animator>().SetTrigger("Show");
        PlayerPrefsController.instance.SetNextScene("Gameplay");
    }
    
    private IEnumerator DelayDisplayForm()
    {
        yield return new WaitForSeconds(1.1f);
        formInput.SetActive(true);
    }
}
