using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
