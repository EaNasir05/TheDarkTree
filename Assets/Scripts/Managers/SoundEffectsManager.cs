using UnityEngine;
using System.Collections.Generic;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;
    [SerializeField] private AudioSource soundEffectObject;
    private List<AudioSource> audios;
    private AudioSource currentDialogue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audios = new List<AudioSource>();
    }

    /*public void Resume()
    {
        foreach (AudioSource audioSource in audios)
        {
            audioSource.UnPause();
        }
    }

    public void Pause()
    {
        foreach(AudioSource audioSource in audios)
        {
            audioSource.Pause();
        }
    }*/

    public void PlaySFXClip(AudioClip clip, Transform spawn, float volume)
    {
        float time = 0;
        bool ended = false;
        AudioSource audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        //audios.Add(audioSource);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        while (!ended) // && !GameManager.pause
        {
            time += Time.deltaTime;
            if (time >= clip.length)
            {
                ended = true;
            }
        }
        //audios.Remove(audioSource);
        Destroy(audioSource);
    }

    public void PlayDialogue(AudioClip clip, Transform spawn, float volume)
    {
        if (currentDialogue != null)
        {
            //audios.Remove(currentDialogue);
            currentDialogue.Stop();
        }
        float time = 0;
        bool ended = false;
        AudioSource audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        //audios.Add(audioSource);
        audioSource.clip = clip;
        audioSource.volume = volume;
        currentDialogue = audioSource;
        audioSource.Play();
        while (!ended)
        {
            time += Time.deltaTime;
            if (time >= clip.length)
            {
                ended = true;
            }
        }
        //audios.Remove(audioSource);
        Destroy(audioSource);
        currentDialogue = null;
    }
}
