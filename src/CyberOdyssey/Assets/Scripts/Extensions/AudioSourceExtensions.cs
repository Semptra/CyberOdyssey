using UnityEngine;

public static class AudioSourceExtensions
{
    public static void Play(this AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
