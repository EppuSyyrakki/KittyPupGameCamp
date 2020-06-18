using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] AudioClip[] contact;
    [SerializeField] AudioClip[] death;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    
}
