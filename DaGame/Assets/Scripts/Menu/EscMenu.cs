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
        if (Input.GetKeyDown(KeyCode.Escape) && !isActivated)
        {
            escCanvas.SetActive(true);
            StartCoroutine(ButtonDelay());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isActivated)
        {
            escCanvas.SetActive(false);
            StartCoroutine(ButtonDelay());
        }

        if (isActivated)
        {
            snapshotMenu.TransitionTo(0.2f);
        }
        else
        {
            snapshotNormal.TransitionTo(0.2f);
        }
    }
    IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(0.1f);
        isActivated = !isActivated;
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