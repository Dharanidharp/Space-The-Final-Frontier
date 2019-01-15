using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Instance variables for the pause menu buttons
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button quit;


    private void Start()
    {
        // Event Listeners
        resume.onClick.AddListener(HandleResumeClick);
        //restart.onClick.AddListener(HandleRestartClick);
        quit.onClick.AddListener(HandleQuitClick);
    }

    // Resume button event
    void HandleResumeClick()
    {
        GameManager.Instance.TogglePause();
    }

    // Restart button Event
    void HandleRestartClick()
    {
        //GameManager.Instance.RestartGame();
    }

    // Quit button event
    void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }
}
