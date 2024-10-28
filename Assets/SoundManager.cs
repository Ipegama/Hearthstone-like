using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] List<AudioClip> allAudioClips;

    [SerializeField] private AudioSource soundObject;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        musicSource.clip = allAudioClips[0];
        musicSource.Play();
    }

    public void PlaySoundClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(soundObject);

        audioSource.clip = audioClip;
        audioSource.Play();

        float clipLengt = audioSource.clip.length;

        Destroy(audioSource, clipLengt);
        StartCoroutine(WaitAndRestoreVolume(clipLengt));
    }

    private IEnumerator WaitAndRestoreVolume(float clipLength)
    {
        float normalMusicVolume = musicSource.volume;
        musicSource.volume *= 0.2f;
        yield return new WaitForSeconds(clipLength);
        musicSource.volume = normalMusicVolume;
    }
}
