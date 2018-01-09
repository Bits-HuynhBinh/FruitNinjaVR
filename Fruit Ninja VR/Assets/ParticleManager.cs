using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem parSwordHitFruit;
    public ParticleSystem parFruitHitFloor;
    public ParticleSystem parBoomHitFloor;
    public ParticleSystem parSwordHitBoom;

    public static ParticleManager Instance;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }


    public void PlayParticleOn(Vector3 position, ParticleSystem particle)
    {
        ParticleSystem particleSystem = Instantiate(particle, position, Quaternion.identity);
        particleSystem.Play();
    }

    
}
