using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;
    [SerializeField] private Slider volumeSlider;

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
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volumeSlider.value;
        }
    }

    public void PlaySoundChannel(string filePath, int channel)
    {
        if (channel >= audioSources.Length) return;
        if (audioSources[channel].isPlaying) return;

        AudioClip clip = Resources.Load(filePath) as AudioClip;

        audioSources[channel].PlayOneShot(clip);
    }
}
