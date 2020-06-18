using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainAudio : MonoBehaviour
{
    // public static int masterVolume;
    // public SliderInt masterSlider;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioClip startRound;
    [SerializeField] private AudioClip endRound;

    // private AudioMixerGroup musicChannel;
    // private AudioMixerGroup effectsChannel;

    // Update is called once per frame
    void Update()
    {

    }
}
