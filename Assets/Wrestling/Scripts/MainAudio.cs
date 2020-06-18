using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainAudio : MonoBehaviour
{
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioClip startRound;
    [SerializeField] private AudioClip endRound;
    [SerializeField] private static AudioMixer mixer;
    private bool _roundEnded = false;
    private int _currentSceneIndex;


    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_currentSceneIndex != 0) effectSource.PlayOneShot(startRound);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreControl._isOneFall && !_roundEnded)
        {
            _roundEnded = true;
            Invoke("PlayEndRound", 1f);
        }
    }

    public static void SetEffectVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, -80f, 0f);
        mixer.SetFloat("WrestlingEffectsVolume", clampedVolume);
    }

    public static void SetMusicVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, -80f, 0f);
        mixer.SetFloat("WrestlingMusicVolume", clampedVolume);
    }

    private void PlayEndRound()
    {
        _roundEnded = true;
        effectSource.PlayOneShot(endRound);
    }
}
