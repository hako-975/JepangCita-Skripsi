using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Cinemachine;

public class ClassroomController : MonoBehaviour
{
    private bool isHasClass = false;

    private int gameDay;
    private int gameMonth;
    private int gameYear;

    private int gameDayTemp;

    private string formattedDate;

    [SerializeField]
    private Animator transitionPanel;

    [SerializeField]
    private Animator dialogPanel;

    [SerializeField]
    private TextMeshProUGUI dialogNameText;
    [SerializeField]
    private TextMeshProUGUI dialogSentenceText;

    private GameObject player;
    private PlayerController playerController;
    private CinemachineFreeLook cinemachine;

    private bool isDialogPlayed = false;
    bool isFinishedTyping = false;

    [SerializeField]
    private ActionController actionController;
    [SerializeField]
    private GameObject joystick;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        cinemachine = player.GetComponentInChildren<CinemachineFreeLook>();

        gameDay = PlayerPrefsController.instance.GetDateDay();
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        gameYear = PlayerPrefsController.instance.GetDateYear();
        
        gameDayTemp = gameDay;

        for (int i = 0; i < 6; i++)
        {
            DateTime date = new DateTime(gameYear, gameMonth, gameDayTemp);

            string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));

            // Check if it's Monday, Tuesday, Wednesday, or Thursday
            if (dayName == "Senin" || dayName == "Selasa" || dayName == "Rabu" || dayName == "Kamis")
            {
                isHasClass = true;
            }

            gameDayTemp++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHasClass)
        {
            gameDay = PlayerPrefsController.instance.GetDateDay();

            DateTime date = new DateTime(gameYear, gameMonth, gameDay);

            string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));
            formattedDate = date.ToString("dd/MM/yyyy");
            
            // senin, jam 9 - jam 12
            if (dayName == "Senin" || dayName == "Rabu")
            {
                // set npc
                if (PlayerPrefsController.instance.GetHour() >= 8)
                {
                    //blm bikin npcGameObject.SetActive(true);
                }

                // mulai kelas
                if (PlayerPrefsController.instance.GetHour() == 9 && PlayerPrefsController.instance.GetMinute() == 0)
                {
                    // Hadir
                    PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "Hadir");
                }
                else if (PlayerPrefsController.instance.GetHour() >= 9 && PlayerPrefsController.instance.GetMinute() > 5)
                {
                    // Terlambat
                    PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "Terlambat");
                }
                else if (PlayerPrefsController.instance.GetHour() >= 12)
                {
                    // bolos
                    PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "Bolos");
                }

                // silahkan duduk untuk memulai kelas
                if (PlayerPrefsController.instance.GetHour() >= 9)
                {
                    // dialog panel
                    if (isDialogPlayed == false)
                    {
                        isDialogPlayed = true;
                        StartCoroutine(OpenDialogPanel("Sensei", PlayerPrefsController.instance.GetCharacterName() + ", silahkan duduk untuk memulai kelas."));
                    }

                    // if duduk
                    if (playerController.canMove == false)
                    {
                        StartCoroutine(CloseTransitionAndStartClass());
                        isHasClass = false;
                    }
                }
            }
        }
    }

    IEnumerator OpenDialogPanel(string nameText, string sentenceText)
    {
        dialogPanel.gameObject.SetActive(true);
        dialogPanel.SetTrigger("Show");
        yield return new WaitForSeconds(1f);
        dialogNameText.text = nameText;
        dialogSentenceText.text = "";

        int wordIndex = 0;

        while (isFinishedTyping == false)
        {
            dialogSentenceText.text += sentenceText[wordIndex];
            yield return new WaitForSeconds(0.05f);

            if (++wordIndex == sentenceText.Length)
            {
                isFinishedTyping = true;
                break;
            }
        }
    }

    IEnumerator CloseTransitionAndStartClass()
    {
        dialogPanel.SetTrigger("Hide");

        transitionPanel.gameObject.SetActive(true);
        transitionPanel.SetTrigger("Close");
        actionController.DeactiveCanvasAction();
        actionController.isMiddleClass = true;
        yield return new WaitForSeconds(3f);

        cinemachine.m_Orbits[0].m_Height = 2f;
        cinemachine.m_Orbits[0].m_Radius = 0f;

        cinemachine.m_Orbits[1].m_Height = 1.35f;
        cinemachine.m_Orbits[1].m_Radius = 1.5f;


        cinemachine.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = 1f;
        cinemachine.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.z = 0.5f;

        cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = 1.35f;

        cinemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));

        joystick.SetActive(false);

        yield return new WaitForSeconds(1f);
        transitionPanel.SetTrigger("Open");
    }

}
