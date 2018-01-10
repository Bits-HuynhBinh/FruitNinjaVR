using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem parSwordHitFruit;
    public ParticleSystem parFruitHitFloor;
    public ParticleSystem parBoomHitFloor;
    public ParticleSystem parSwordHitBoom;
    public ParticleSystem parSpawningFruit;
    public ParticleSystem parSpawningBoom;
    public ParticleSystem parHalfFruitHitFloor;


    public static ParticleManager Instance;

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
    }


    public void PlayParticleOn(Vector3 position, ParticleSystem particle)
    {
        ParticleSystem particleSystem = Instantiate(particle, position, Quaternion.identity);
        particleSystem.Play();
    }

    public void PlayParticleOn(Vector3 position, ParticleSystem particle, Quaternion rotation)
    {
        ParticleSystem particleSystem = Instantiate(particle, position, rotation);
        particleSystem.Play();
    }




}
