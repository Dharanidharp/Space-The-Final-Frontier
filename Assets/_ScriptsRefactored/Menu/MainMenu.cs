using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation mainMenuAnimator;
    [SerializeField] private AnimationClip fadingOutAnimationClip;
    [SerializeField] private AnimationClip fadingInAnimationClip;

    public Events.EventFadeComplete onMainMenuFadeComplete;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

        //UIManager.Instance.SetMainMenuParticleSystemsActive(true);
    }

    public void OnFadeOutComplete()
    {
        onMainMenuFadeComplete.Invoke(true);
        //UIManager.Instance.SetDummyCamActive(false);
        UIManager.Instance.SetMainMenuActive(false);
        //UIManager.Instance.SetMainMenuParticleSystemsActive(false);
    }

    public void OnFadeInComplete()
    {
        onMainMenuFadeComplete.Invoke(false);
        //UIManager.Instance.SetDummyCamActive(true);
        UIManager.Instance.SetMainMenuActive(true);
        //UIManager.Instance.SetMainMenuParticleSystemsActive(true);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCamActive(false);
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadingOutAnimationClip;
        mainMenuAnimator.Play();
    }

    public void FadeIn()
    {
        UIManager.Instance.SetDummyCamActive(true);
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadingInAnimationClip;
        mainMenuAnimator.Play();
    }
}
