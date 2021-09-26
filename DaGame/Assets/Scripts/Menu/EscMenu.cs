using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private bool isActivated = false;
    [SerializeField] private GameObject escCanvas;
    [SerializeField] private AudioMixerSnapshot snapshotMenu;
    [SerializeField] private AudioMixerSnapshot snapshotNormal;

    private void Start()
    {
        escCanvas.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActivated)
            {
                ExitMenu();
            }
            else
            {
                EnterMenu();
            }
        }
    }

    private void EnterMenu()
    {
        escCanvas.SetActive(true);
        isActivated = !isActivated;
        snapshotMenu.TransitionTo(0.2f);
    }

    private void ExitMenu()
    {
        escCanvas.SetActive(false);
        isActivated = !isActivated;
        snapshotNormal.TransitionTo(0.2f);
    }

    public void Button_Deactivate()
    {
        isActivated = false;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}