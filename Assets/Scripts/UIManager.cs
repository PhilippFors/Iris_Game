using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool isPaused = false;
    private float fixedDeltaTime;
    public float invoke = 4.0f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioListener fpsAudioListener;
    [SerializeField] private GameObject bedController;
    [SerializeField] private TMPro.TextMeshProUGUI task;
    [SerializeField] private TMPro.TextMeshProUGUI subtitles;
    [SerializeField] private GameObject subObj;
    public Animation interactCircle;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !isPaused)
        {
            ActivateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) & isPaused)
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioListener.pause = true;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    public void SetSubtitle(string text)
    {
        CancelInvoke("DisableSubtitles");
        subtitles.text = text;
        subObj.SetActive(true);
        Invoke("DisableSubtitles", invoke);
    }

    public void DisableSubtitles()
    {
        subObj.SetActive(false);
    }

    public void SetTask(string text)
    {
        task.text = text;
    }
}

