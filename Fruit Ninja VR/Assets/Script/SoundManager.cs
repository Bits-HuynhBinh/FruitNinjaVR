using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip clipBackgroundMusic;
    public AudioClip clipWinSound;
    public AudioClip clipLooseSound;
    public AudioClip clipSwordHitFruitSound;
    public AudioClip clipSwordHitNothingSound;
    public AudioClip clipSpawnerSound;
    public AudioClip clipBoomBangSound;
    public AudioClip clipFruitFallDownTheFloorSound;
    public AudioClip clipBoomFallDownTheFloorSound;
    public AudioClip clipHalfFruitFallDownTheFloorSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundOneShotOnObject(AudioSource audioSource, AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    public void PlaySoundOneShotOnSoundManager(AudioClip clip)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        GetComponent<AudioSource>().PlayOneShot(clip);
    }



}
