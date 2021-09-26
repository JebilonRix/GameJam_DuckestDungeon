using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject[] ui;

    private void Start()
    {
        Button_ChangeUiTo(0);
    }
    public void Button_LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Button_ChangeUiTo(int index)
    {
        for (int i = 0; i < ui.Length; i++)
        {
            if (i == index)
            {
                ui[i].SetActive(true);
            }
            else
            {
                ui[i].SetActive(false);
            }
        }
    }
    public void Button_Quit()
    {
        Application.Quit();
    }
}