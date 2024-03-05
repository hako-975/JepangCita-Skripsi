﻿using System;
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
    private bool isDialogPlayedEndClass = false;
    private bool isFinishedTyping = false;
    private bool isStartedClass = false;

    [SerializeField]
    private ActionController actionController;
    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private TextMeshPro boardText;

    // Start is called before the first frame update
    void Start()
    {
        isStartedClass = false;
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

            StartClassroom(dayName);
        }
    }

    private void StartClassroom(string currentDay)
    {
        // senin dan rabu, jam 9 - jam 12
        if (currentDay == "Senin" || currentDay == "Rabu")
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
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Hadir");
            }
            else if (PlayerPrefsController.instance.GetHour() >= 9 && PlayerPrefsController.instance.GetMinute() > 5)
            {
                // Terlambat
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Terlambat");
            }
            else if (PlayerPrefsController.instance.GetHour() >= 12)
            {
                // bolos
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Bolos");
            }

            // silahkan duduk untuk memulai kelas jam 9 - jam 12
            if (PlayerPrefsController.instance.GetHour() >= 9 && PlayerPrefsController.instance.GetHour() <= 12)
            {
                // dialog panel
                if (isDialogPlayed == false)
                {
                    isDialogPlayed = true;
                    StartCoroutine(OpenDialogPanel("Sensei", PlayerPrefsController.instance.GetCharacterName() + ", silahkan duduk untuk memulai kelas.", false, false));
                }

                // if duduk
                if (playerController.canMove == false)
                {
                    if (isStartedClass == false)
                    {
                        isStartedClass = true;
                        StartCoroutine(CloseTransitionAndStartClass(9));
                    }
                }
            }

            // jam pulang
            if (PlayerPrefsController.instance.GetHour() == 12)
            {
                isHasClass = false;
                if (isDialogPlayedEndClass == false)
                {
                    isDialogPlayedEndClass = true;
                    StartCoroutine(OpenDialogPanel("Sensei", "Oke anak-anak kita akhiri pertemuan ini. じゃ、またあした。", true, true));
                }
            }
        }

        // selasa dan kamis, jam 13 - jam 16
        if (currentDay == "Selasa" || currentDay == "Kamis")
        {
            // set npc
            if (PlayerPrefsController.instance.GetHour() >= 12)
            {
                //blm bikin npcGameObject.SetActive(true);
            }

            // mulai kelas
            if (PlayerPrefsController.instance.GetHour() == 13 && PlayerPrefsController.instance.GetMinute() == 0)
            {
                // Hadir
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Hadir");
            }
            else if (PlayerPrefsController.instance.GetHour() >= 13 && PlayerPrefsController.instance.GetMinute() > 5)
            {
                // Terlambat
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Terlambat");
            }
            else if (PlayerPrefsController.instance.GetHour() >= 16)
            {
                // bolos
                PlayerPrefsController.instance.SetAttendance(currentDay, formattedDate, "Bolos");
            }

            // silahkan duduk untuk memulai kelas jam 13 - jam 16
            if (PlayerPrefsController.instance.GetHour() >= 13 && PlayerPrefsController.instance.GetHour() <= 16)
            {
                // dialog panel
                if (isDialogPlayed == false)
                {
                    isDialogPlayed = true;
                    StartCoroutine(OpenDialogPanel("Sensei", PlayerPrefsController.instance.GetCharacterName() + ", silahkan duduk untuk memulai kelas.", false, false));
                }

                // if duduk
                if (playerController.canMove == false)
                {
                    if (isStartedClass == false)
                    {
                        isStartedClass = true;
                        StartCoroutine(CloseTransitionAndStartClass(13));
                    }
                }
            }

            // jam pulang
            if (PlayerPrefsController.instance.GetHour() == 16)
            {
                isHasClass = false;

                if (isDialogPlayedEndClass == false)
                {
                    isDialogPlayedEndClass = true;
                    StartCoroutine(OpenDialogPanel("Sensei", "Oke anak-anak kita akhiri pertemuan ini. じゃ、またあした。", true, true));
                }
            }
        }
    }

    IEnumerator OpenDialogPanel(string nameText, string sentenceText, bool autoClose, bool goHome)
    {
        dialogPanel.gameObject.SetActive(true);
        dialogNameText.text = "";
        dialogSentenceText.text = "";
        dialogPanel.SetTrigger("Show");
        yield return new WaitForSeconds(1f);
        dialogNameText.text = nameText;
        dialogSentenceText.text = "";

        int wordIndex = 0;
        isFinishedTyping = false;

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

        if (autoClose)
        {
            yield return new WaitForSeconds(2f);

            if (isFinishedTyping)
            {
                dialogPanel.SetTrigger("Hide");
            }
        }

        if (goHome)
        {
            yield return new WaitForSeconds(2f);

            transitionPanel.SetTrigger("Close");
            yield return new WaitForSeconds(3f);

            PlayerPrefsController.instance.DeleteKey("PositionRotationCharacter");
            PlayerPrefsController.instance.SetPositionRotationCharacter(new Vector3(-4.5f, 0.5f, 5.5f), new Quaternion(0f, 180f, 0f, 1f));

            PlayerPrefsController.instance.SetNextScene("Home");
        }
    }

    IEnumerator CloseTransitionAndStartClass(int startHourClass)
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
        
        string ucapan = "こんにちは みなさん！";
        
        if (startHourClass == 9)
        {
            ucapan = "おはようございます。";
        }

        StartCoroutine(OpenDialogPanel("Sensei", ucapan + " hari ini kita akan mempelajari materi " + PlayerPrefsController.instance.listMateri[PlayerPrefsController.instance.GetCurrentMateri()], true, false));

        yield return new WaitForSeconds(1f);
        // board
        boardText.text = "Kalian bisa melanjutkan materi " + PlayerPrefsController.instance.listMateri[PlayerPrefsController.instance.GetCurrentMateri()] + " melalui website JepangCita. \n (Untuk mengakses materi, buka browser dan kunjungi situs JepangCita. Setelah berhasil login, buka menu Materi.)";
        // update materi
        PlayerPrefsController.instance.SetCurrentMateri(PlayerPrefsController.instance.GetCurrentMateri() + 1);
    }

}
