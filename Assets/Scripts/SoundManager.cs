using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour
{
    private AudioSource         audioDevice;
    public AudioClip[]          audioClip;

    void Start()
    {
        audioDevice = this.GetComponent<AudioSource>();
    }

    public void Play(string audioName)
    {
        for (int i = 0; i < audioClip.Length; i++)
        {
            if (audioClip[i].name == audioName)
            {
                audioDevice.clip = audioClip[i];
                break;
            }
        }

        audioDevice.Play();
    }

    public void Loop(bool loop)
    {
        audioDevice.loop = loop;
    }

    public void Mute(bool isMute)
    {
        audioDevice.mute = isMute;
    }

    public void Stop()
    {
        audioDevice.Stop();
    }

}
