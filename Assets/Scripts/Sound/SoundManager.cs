using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    // Singleton behavior so it can play as necessary
    public static SoundManager Instance {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void UpdateVolume(float value)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = value;
        }
    }

    public void PlaySoundChannel1()
    {

    }
}
