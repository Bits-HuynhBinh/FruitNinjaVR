using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// play a small particle system when created
// 2 halfs fall down the floor based on gravity
// 2 half separated each other a bit
// play particle system when 2 halfs touch the floor
// destroy when on the floor
public class HalfFruitController : MonoBehaviour
{
    public enum HalfMode { HalfLeft, HalfRight };

    public HalfMode halfMode;

  
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        // play a small particle system when created


        // 2 half separated each other a bit, I think can add a bit force or a joint
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag(Tags.tagFloor))
        {
            // play particle system when half touch the floor
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            ParticleManager.Instance.PlayParticleOn(position, ParticleManager.Instance.parHalfFruitHitFloor, rotation);

            // play sound when hitting the floor, bẹp
            SoundManager.Instance.PlaySoundOneShotOnObject(GetComponent<AudioSource>(), SoundManager.Instance.clipHalfFruitFallDownTheFloorSound);

            // destroy when on the floor
            Destroy(gameObject, 1f);
        }
    }

}
