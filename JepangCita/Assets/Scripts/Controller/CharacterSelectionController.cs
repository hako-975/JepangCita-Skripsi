using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    private bool currentGirl = true;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        
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
}
