using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private Camera dummyCam;

    [SerializeField] private Button startButton;

    [SerializeField] private ParticleSystem ps1;

    [SerializeField] private ParticleSystem ps2;

    [SerializeField] private ParticleSystem ps3;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        mainMenu.onMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        startButton.onClick.AddListener(HandleStartButtonClick);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }
    }

    void HandleStartButtonClick()
    {
        GameManager.Instance.StartGame();
    }

    public void SetDummyCamActive(bool active)
    {
        dummyCam.gameObject.SetActive(active);
    }

    public void SetMainMenuActive(bool active)
    {
        mainMenu.gameObject.SetActive(active);
    }

    public void SetMainMenuParticleSystemsActive(bool active)
    {
        if (!active)
        {
            ps1.Stop();
            ps2.Stop();
            ps3.Stop();
        }
        else
        {
            ps1.Play();
            ps2.Play();
            ps3.Play();
        }
    }
}

