using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(ExitGame);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
