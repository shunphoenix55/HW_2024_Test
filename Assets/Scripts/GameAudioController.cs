using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This starts the audio for the main menu
public class GameAudioController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip startAudio;
    public AudioClip loopAudio;


    IEnumerator Start()
    {
        audioSource.clip = startAudio;
        audioSource.Play();
        yield return new WaitForSeconds(startAudio.length);
        audioSource.clip = loopAudio;
        audioSource.loop = true;
        audioSource.Play();
    }



}
