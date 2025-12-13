using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake(){
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume(){
        volume += 0.1f;
        if (volume > 1.0f){
            volume = 0.0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume(){
        return volume;
    }
}
