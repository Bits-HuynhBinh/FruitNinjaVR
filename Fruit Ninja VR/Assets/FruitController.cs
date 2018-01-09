using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject haftFruitLeft;
    public GameObject haftFruitRight;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        // if Tag = floor/plane, then play particle system on the floor to show that the fruit is destroy
        if (other.gameObject.CompareTag("FLOOR"))
        {
            // minus 1 point
            GameManager.Instance.decreaseLife();

            // play particle system on the floor to show that the fruit is destroy
            ParticleManager.Instance.PlayParticleOn(transform.position, ParticleManager.Instance.parFruitHitFloor);

            // hide the fruit
            gameObject.SetActive(false);
        }

        // if Tag = sword, then replace the full fruit with haft fruit, play particle system for haft fruit
        if (other.gameObject.CompareTag("SWORD"))
        {
            // play particle system
            ParticleManager.Instance.PlayParticleOn(transform.position, ParticleManager.Instance.parSwordHitFruit);

            // replace the full fruit with haft fruit
            ReplaceFullFruitWithHaftFruit(transform.position);

            // play sound
            SoundManager.Instance.PlaySoundOneShotOnObject(GetComponent<AudioSource>(), SoundManager.Instance.clipSwordHitFruitSound);

            // increase point
            GameManager.Instance.increasePoint();
        }

        // if fruit collide with player, it is an error, destroy the fruit, dont count it
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


    private void ReplaceFullFruitWithHaftFruit(Vector3 fullFruitPos)
    {
        // add offset to the 2 haft fruit
        Vector3 offset = new Vector3(0, 0, 0);

        // hide full fruit
        gameObject.SetActive(false);

        // spawn haftFruitLeft + offset
        Instantiate(haftFruitLeft, fullFruitPos, Quaternion.identity);

        // spawn haftFruitRight - offset
        Instantiate(haftFruitRight, fullFruitPos, Quaternion.identity);


        // destroy game object
        Destroy(gameObject, 1f);
    }
}
