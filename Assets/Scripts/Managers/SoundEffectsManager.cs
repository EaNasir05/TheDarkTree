using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;
    [SerializeField] private AudioSource soundEffectObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip clip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

}
