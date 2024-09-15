using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
        {
            sound.soundSource = gameObject.AddComponent<AudioSource>();
            sound.soundSource.volume = sound.volume;
            sound.soundSource.pitch = sound.pitch;
            sound.soundSource.clip = sound.clip;
            sound.soundSource.loop = sound.loop;
            sound.soundSource.playOnAwake = sound.playOnAwake;
        }
    }

    public void PlayAudio(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
                sound.soundSource.Play();
        }
    }
}
