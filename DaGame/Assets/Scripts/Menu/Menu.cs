using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject[] ui;
    public void Button_Play(string sceneName)
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