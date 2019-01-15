using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[SerializeField] private bool musicEnabled = true;

	[SerializeField] private bool fxEnabled = true;

    [Range(0, 1)]
	[SerializeField] private float musicVolume = 1.0f;

    [Range(0, 1)]
	[SerializeField] private float fxVolume = 1.0f;

	[SerializeField] private AudioClip mainMenuBGSound;

	[SerializeField] private AudioClip mainAmbientSound;

	[SerializeField] private AudioSource musicSource;

	[SerializeField] private AudioClip musicClip;

    void Start()
    {
        PlayBackgroundMusic(musicClip);
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (!musicEnabled || !musicClip || !musicSource)
        {
            return;
        }
			
        musicSource.Stop();

        musicSource.clip = musicClip;

        musicSource.volume = musicVolume;

        musicSource.loop = true;

        musicSource.Play();
    }
}
