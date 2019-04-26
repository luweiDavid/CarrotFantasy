using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{

    AudioSource[] audioSrcArray;

    private AudioSource mBgAudioSrc;
    private AudioSource mAudioEffectSrc;

    public void Init() {
        audioSrcArray = GameObject.Find("GameManager").GetComponents<AudioSource>();

        mBgAudioSrc = audioSrcArray[0];
        mAudioEffectSrc = audioSrcArray[1];
    }

    public void PlayBgAudio(AudioClip _clip) {
        if (!mBgAudioSrc.isPlaying || mBgAudioSrc.clip != _clip) {
            mBgAudioSrc.clip = _clip;
            mBgAudioSrc.Play();
            mBgAudioSrc.loop = true;
        }
    }

    public void SetBgVolume(float volume) {
        if (mBgAudioSrc.isPlaying) { 
            mBgAudioSrc.volume = volume;
        }
    }

    public void PlayAudioEffect(AudioClip _clip) {
        mAudioEffectSrc.PlayOneShot(_clip);
    }

    public void SetAudioEffectVolume(float volume) {
        mAudioEffectSrc.volume = volume;
    }

}
