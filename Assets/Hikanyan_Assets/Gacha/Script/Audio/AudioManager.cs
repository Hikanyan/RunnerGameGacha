using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : AbstractSingleton<AudioManager>
{
    private AudioSource _bgmAudioSource;
    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = clip;

        audioSource.Play();

        Destroy(audioSource, clip.length);
    }

    public void PlayMusic(AudioClip clip)
    {
        StopMusic();

        _bgmAudioSource = gameObject.AddComponent<AudioSource>();

        _bgmAudioSource.clip = clip;

        _bgmAudioSource.loop = true;

        _bgmAudioSource.Play();
    }

    public void StopMusic()
    {
        if (_bgmAudioSource != null && _bgmAudioSource.isPlaying)
        {
            _bgmAudioSource.Stop();
            Destroy(_bgmAudioSource);
        }
    }
}
