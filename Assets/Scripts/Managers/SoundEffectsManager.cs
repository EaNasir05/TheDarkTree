using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
        AudioSource newSource = Instantiate(soundEffectObject, Vector3.zero, Quaternion.identity);
        newSource.clip = clip;
        newSource.volume = volume;
        newSource.Play();
        StartCoroutine(DestroyAfterPlay(newSource));
    }

    private IEnumerator DestroyAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source.gameObject);
    }

    private IEnumerator DestroyDialogueAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source.gameObject);
        currentDialogue = null;
    }

    public void PlayDialogue(AudioClip clip, float volume)
    {
        if (currentDialogue != null)
        {
            currentDialogue.Stop();
        }
        if (clip != null)
        {
            AudioSource newSource = Instantiate(soundEffectObject, Vector3.zero, Quaternion.identity);
            currentDialogue = newSource;
            newSource.clip = clip;
            newSource.volume = volume;
            newSource.Play();
            StartCoroutine(DestroyDialogueAfterPlay(newSource));
        }
        else
        {
            currentDialogue = null;
        }
    }

    public void PlaySFXClipOnFeed(AudioClip clip, float volume)
    {
        if (currentDialogue == null)
        {
            AudioSource newSource = Instantiate(soundEffectObject, Vector3.zero, Quaternion.identity);
            currentDialogue = newSource;
            newSource.clip = clip;
            newSource.volume = volume;
            newSource.Play();
            StartCoroutine(DestroyDialogueAfterPlay(newSource));
        }
    }
}
