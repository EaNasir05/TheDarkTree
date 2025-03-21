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
        float time = 0;
        bool ended = false;
        AudioSource audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        while (!ended)
        {
            time += Time.deltaTime;
            if (time >= clip.length)
            {
                Destroy(audioSource);
                ended = true;
            }
        }
    }
}
