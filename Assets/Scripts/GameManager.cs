using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class GameManager : MonoBehaviour
{
    [SerializeField] public static string selectableTag = "Selectable";
    [SerializeField] public static string notSelectable = "Not Selectable";

    public int level = 1;

    // level 1
    public bool alarmClockState;
    public bool closetState;
    public bool faucetState;
    public bool faceWashed;
    public bool handsWashed;
    public bool clothesOn;

    // level 2
    public bool coffeeMachineFound;
    public bool coffeeMade;
    public bool gotCup;
    public bool fridgeOpen;
    public bool gotBowl;
    public bool gotCereals;

    // level 3
    public bool phoneRinging;
    public bool computerOn;
    public bool windowClosed;
    public bool workFinished;
    public bool emailSent;
    public bool pickedUpPhone;
    public bool jacketOn;
    public bool mumCalled;

    public Trigger bathroomTrigger;
    public Trigger officeTrigger;
    public Trigger bedroomTrigger;
    public Trigger livingroomTrigger;

    public GameObject fpsController;
    public GameObject monologueobj;
    public AudioSource monologeAudiosource;
    public AudioSource character;
    public GameObject bedobj;
    public Animation introCam;
    public GameObject blur;
    public GameObject circle;

    public Office office;
    public Bedroom bedroom;
    public Bathroom bathroom;
    public Livingroom livingroom;

    public Animator transitionAnim;
    public Interact interact;
    public GameObject[] audioSources = new GameObject[1];

    // narration
    public AudioClip[] levelOne_Narration;
    public AudioClip[] levelTwo_Narration;
    public AudioClip[] levelThree_Narration;

    public VideoClip[] _levelOne_Narration;
    public VideoClip[] _levelTwo_Narration;
    public VideoClip[] _levelThree_Narration;

    public VideoPlayer videoPlayer;
    public GameObject videoPanel;
    ResonanceAudioSource[] a;
    public enum GameState { undefined, gameStart, main, restart, newLevel, end };
    GameState m_gameState = GameState.undefined;

    //Setup for Singleton
    //This means we can access this class form anywhere without using GetComponent<>
    //StateCheck is now a global class
    //How to access the GameManagaer class and call it's variables/methods from anywhere:
    //GameMangaer.Instance.yourVariable or GameManager.Instance.YourMethod()
    private static GameManager _instance;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Interact.progress += CheckObject;
        CollisionTrigger.playAudio += PlayAudio;
        a = FindObjectsOfType<ResonanceAudioSource>();
        for (int i = 0; i < a.Length; i++)
        {
            if (!a[i].gameObject.GetComponent<AudioSource>().isPlaying)
            {
                StopAudio(a[i].gameObject);
            }
        }
        SetState(GameState.gameStart);
    }
    private void Update()
    {
        // if (UIManager.Instance.isPaused)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         Application.Quit();
        //     }
        // }
        // for (int i = 0; i < audio.Length; i++)
        // {
        //     if (!audio[i].gameObject.GetComponent<AudioSource>().isPlaying)
        //     {
        //         StopAudio(audio[i].gameObject);
        //     }
        // }
    }
    public void CheckObject(GameObject o)
    {
        if (bedroomTrigger.triggered)
        {
            bedroom.CheckObject(o);
        }
        if (bathroomTrigger.triggered)
        {
            bathroom.CheckObject(o);
        }
        if (livingroomTrigger.triggered)
        {
            livingroom.CheckObject(o);
        }
        if (officeTrigger.triggered)
        {
            office.CheckObject(o);
        }
    }

    //Method for playing audio from Arrays
    public static void PlayAudio(GameObject o, AudioClip[] audioClip, int audioIndex)
    {

        // o.SetActive(true);
        AudioSource audioSource = o.gameObject.GetComponent<AudioSource>();
        ResonanceAudioSource resonanceAudioSource = o.gameObject.GetComponent<ResonanceAudioSource>();
        if (!audioSource.isPlaying)
        {

            if (resonanceAudioSource != null)
            {

                resonanceAudioSource.enabled = true;
            }

            audioSource.clip = audioClip[audioIndex];
            audioSource.Play();

        }
    }

    //Method for playing single audio files if needed
    public static void PlayAudio(GameObject o, AudioClip audioClip)
    {
        AudioSource audioSource = o.gameObject.GetComponent<AudioSource>();
        ResonanceAudioSource resonanceAudioSource = o.gameObject.GetComponent<ResonanceAudioSource>();
        if (!audioSource.isPlaying)
        {

            if (resonanceAudioSource != null)
            {

                resonanceAudioSource.enabled = true;

            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public static void PlayAudio(GameObject o)
    {
        AudioSource audioSource = o.gameObject.GetComponent<AudioSource>();
        ResonanceAudioSource resonanceAudioSource = o.gameObject.GetComponent<ResonanceAudioSource>();
        if (!audioSource.isPlaying)
        {
            if (resonanceAudioSource != null)
            {

                resonanceAudioSource.enabled = true;
            }

            audioSource.Play();
        }
    }

    //Method for stopping audio
    public static void StopAudio(GameObject o)
    {
        ResonanceAudioSource resonanceAudioSource = o.gameObject.GetComponent<ResonanceAudioSource>();
        AudioSource audioSource = o.gameObject.GetComponent<AudioSource>();

        audioSource.Stop();
        if (resonanceAudioSource != null)
        {

            resonanceAudioSource.enabled = false;
        }
    }

    void StartVideo(VideoClip clip)
    {

        videoPlayer.SetTargetAudioSource(0, monologeAudiosource);
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void SetState(GameState state)
    {
        m_gameState = state;
        switch (m_gameState)
        {
            case GameState.gameStart:
                StopCoroutine("LevelStartCoroutine");
                ResetPosition();

                setBool();
                StartCoroutine("LevelStartCoroutine");
                m_gameState = GameState.main;
                break;
            case GameState.newLevel:
                StopAllCoroutines();
                PlayEndTransition();

                monologueobj.GetComponent<AudioListener>().enabled = true;
                level++;
                setBool();
                StartCoroutine("LevelStartCoroutine");
                m_gameState = GameState.main;
                break;
            case GameState.end:
                SceneManager.LoadScene("Outro", LoadSceneMode.Single);
                break;
        }
    }

    public void ResetPosition()
    {
        fpsController.transform.position = new Vector3(6.70f, -0.55f, -5.55f);
        fpsController.transform.eulerAngles = new Vector3(0, 90f, 0);
        character.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        Input.mousePosition.Set(0.0f, 0.0f, 0.0f);
        bedobj.transform.position = new Vector3(5.24f, -0.53f, -6.70f);
        bedobj.transform.eulerAngles = new Vector3(0, 90f, -56.33f);
    }

    void setBool()
    {
        if (level == 1)
        {
            alarmClockState = true;
            faucetState = false;
            faceWashed = false;
            clothesOn = false;
        }
        else if (level == 2)
        {

            faucetState = true;
            faceWashed = true;
            clothesOn = true;
            alarmClockState = true;
            coffeeMachineFound = false;
            coffeeMade = false;
            gotCup = false;
            fridgeOpen = false;
            gotBowl = false;
            gotCereals = false;
        }
        else if (level == 3)
        {
            faucetState = true;
            faceWashed = true;
            clothesOn = true;
            alarmClockState = true;
            phoneRinging = false;
            computerOn = false;
            windowClosed = false;
            workFinished = false;
            emailSent = false;
            pickedUpPhone = false;
            coffeeMade = false;
            coffeeMachineFound = false;
            gotCup = false;
            jacketOn = false;
            mumCalled = false;
        }
    }

    void PlayEndTransition()
    {
        transitionAnim.Play("End");
        // monologueobj.SetActive(false);
        // monologueobj.GetComponent<AudioListener>().enabled = false;
        // monologueobj.GetComponent<AudioListener>().enabled = false;
        // monologueobj.GetComponent<ResonanceAudioSource>().enabled = false;
        // character.GetComponent<AudioListener>().enabled = false;
    }

    void PlayStartTransition()
    {
        transitionAnim.Play("Start");
    }
    void StartIntroCamera()
    {
        introCam.Play();

    }

    void EnableIntroCam()
    {
        introCam.gameObject.SetActive(true);
        blur.SetActive(false);
        circle.SetActive(false);
    }

    void DisableIntroCam()
    {
        introCam.gameObject.SetActive(false);
        videoPanel.SetActive(false);
        blur.SetActive(true);
        circle.SetActive(true);

    }

    IEnumerator LevelStartCoroutine()
    {
        if (level == 1)
        {

            ToggleCamera(1);

            EnableIntroCam();
            yield return new WaitForSeconds(2f);
            PlayStartTransition();
            StartIntroCamera();
            PlayAudio(monologueobj, levelOne_Narration, 0);
            yield return new WaitForSeconds(32f);

            PlayEndTransition();
            yield return new WaitForSeconds(2.5f);

            videoPanel.SetActive(true);
            PlayStartTransition();

            StartVideo(_levelOne_Narration[0]);


            yield return new WaitForSeconds(22f);
            PlayEndTransition();
            yield return new WaitForSeconds(2f);

            DisableIntroCam();
            ToggleCamera(2);
            PlayStartTransition();
            yield return new WaitForSeconds(2f);
            bedroom.LevelStart();
            // PlayAudio(bedroom.clock);
        }
        else if (level == 2)
        {
            yield return new WaitForSeconds(2.5f);
            ToggleCamera(1);
            videoPanel.SetActive(true);
            EnableIntroCam();
            PlayStartTransition();
            StartVideo(_levelOne_Narration[1]);
            yield return new WaitForSeconds(12f);

            PlayEndTransition();
            yield return new WaitForSeconds(3f);
            PlayStartTransition();

            StartVideo(_levelTwo_Narration[1]);
            yield return new WaitForSeconds(28f);

            PlayEndTransition();
            yield return new WaitForSeconds(2.5f);

            DisableIntroCam();
            ToggleCamera(2);
            PlayStartTransition();
            yield return new WaitForSeconds(2.5f);

            bedroom.LevelStart();
        }
        else if (level == 3)
        {
            yield return new WaitForSeconds(2.5f);
            ToggleCamera(1);
            videoPanel.SetActive(true);
            EnableIntroCam();
            PlayStartTransition();
            StartVideo(_levelTwo_Narration[2]);

            yield return new WaitForSeconds(12f);

            PlayEndTransition();
            yield return new WaitForSeconds(3f);
            PlayStartTransition();

            StartVideo(_levelThree_Narration[0]);
            yield return new WaitForSeconds(16f);

            PlayEndTransition();
            yield return new WaitForSeconds(3f);

            DisableIntroCam();
            ToggleCamera(2);
            PlayStartTransition();
            yield return new WaitForSeconds(2.5f);
            bedroom.LevelStart();
        }
        else if (level == 4)
        {
            yield return new WaitForSeconds(2.5f);
            videoPanel.SetActive(true);
            EnableIntroCam();
            PlayStartTransition();
            StartVideo(_levelThree_Narration[1]);

            yield return new WaitForSeconds(31f);
            PlayEndTransition();
            yield return new WaitForSeconds(2.5f);
            SetState(GameState.end);
        }
    }

    public void ToggleCamera(int camera)
    {
        if (camera == 1)
        {
            ResetPosition();
            fpsController.SetActive(false);
            bedobj.SetActive(false);
            interact.bedCamera.enabled = false;
            interact.fpsCamera.enabled = false;
            // monologueobj.GetComponent<AudioListener>().enabled = true;
        }
        else if (camera == 2)
        {
            bedobj.SetActive(true);
            interact.bedCamera.enabled = true;
        }
        else if (camera == 3)
        {
            interact.fpsCamera.enabled = true;
            interact.bedCamera.enabled = false;
            bedobj.SetActive(false);
            fpsController.SetActive(true);
            // character.GetComponent<AudioListener>().enabled = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}


