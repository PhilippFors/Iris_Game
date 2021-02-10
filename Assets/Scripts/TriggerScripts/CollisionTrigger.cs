using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public bool col;
    
    //Needs to self reference
    public GameObject obj;
    private AudioSource audioSource;
    private ResonanceAudioSource resonanceAudioSource;
    public AudioClip clip;

    public delegate void PlayAudio(GameObject o, AudioClip clip);
    public static event PlayAudio playAudio;

    public GameObject player;
    void Start()
    {
        audioSource = obj.gameObject.GetComponent<AudioSource>();
        resonanceAudioSource = obj.gameObject.GetComponent<ResonanceAudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player.name)
        {
            playAudio(obj, clip);
        }
        
    }
}



