using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] AudioClip[] contact;
    [SerializeField] AudioClip[] death;

    [HideInInspector] public enum AudioStyle { Contact, Death }; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    public void PlayRandom(AudioStyle type)
    {
        if (type == AudioStyle.Contact)
        {
            int index = Random.Range(0, contact.Length);
            audioSource.Stop();
            audioSource.PlayOneShot(contact[index]);
        } 
        else if (type == AudioStyle.Death)
        {
            int index = Random.Range(0, death.Length);
            audioSource.Stop();
            audioSource.PlayOneShot(death[index]);
        }       
    }

}
