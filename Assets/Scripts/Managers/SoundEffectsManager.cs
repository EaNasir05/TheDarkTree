using UnityEngine;
using System.Collections.Generic;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;
    [SerializeField] private AudioSource soundEffectObject;
    private AudioSource currentDialogue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip clip, float volume)
    {
        float time = 0;
        bool ended = false;
        AudioSource audioSource = Instantiate(soundEffectObject, new Vector2(0, 0), Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        while (!ended)
        {
            time += Time.deltaTime;
            if (time >= clip.length)
            {
                ended = true;
            }
        }
        Destroy(audioSource);
    }

    public void PlayDialogue(AudioClip clip, float volume)
    {
        if (currentDialogue != null)
        {
            currentDialogue.Stop();
        }
        float time = 0;
        bool ended = false;
        AudioSource audioSource = Instantiate(soundEffectObject, new Vector2(0, 0), Quaternion.identity);
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
        Destroy(audioSource);
        currentDialogue = null;
    }
}
