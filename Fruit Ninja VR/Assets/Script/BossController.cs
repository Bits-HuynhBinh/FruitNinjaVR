using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public MyStateMachine stateMachine;
    public float stateTimeInterval = 3f;
    private float stateTimeCountdown;

    public float lifeTime = 60f;
    public int bossHealth = 100;

    public List<GameObject> path;
    private int currentPathIndex = -1;

    public float speed = 5f;

    public GameObject targetPlayer;
    public GameObject fruitPrefab;
    public GameObject boomPrefab;
    public GameObject barel;


    private enum ShootIntensity { SOFT, NORMAL, HARD };
    private ShootIntensity shootIntensity;


    void Awake()
    {
        stateMachine = new MyStateMachine(this.gameObject);
        stateMachine.currentState = StateMachine.State.ANIM;

        shootIntensity = ShootIntensity.SOFT;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        stateTimeCountdown -= Time.deltaTime;

        if (stateTimeCountdown <= 0)
        {
            // move to next state
            stateMachine.AI();
            stateTimeCountdown = stateTimeInterval;
        }

        if (lifeTime <= (lifeTime / 2))
        {
            // shoot harder
            shootIntensity = ShootIntensity.NORMAL;
        }
        if (lifeTime <= (lifeTime / 10))
        {
            // shoot harder
            shootIntensity = ShootIntensity.HARD;
        }

        if (lifeTime <= 0)
        {
            //boss die or player win
            BossDisapear();

            // player win, go to next round
            GameManager.Instance.SetNextLevel(GameManager.Instance.GetNextLevel());
        }

    }

    public void SetPath(List<GameObject> path)
    {
        this.path = path;
    }

    public void BossDisapear()
    {
        // play DEAD anim
        GetComponent<Animator>().SetBool("Dead", true);

        // Destroy object
        Destroy(gameObject, 2f);
    }


    public void MakeColor()
    {
        Debug.Log("MakeColor");
        GetComponent<Animator>().SetTrigger("MakeColor");
    }

    public void Move()
    {
        int random = Random.Range(0, path.Count);

        if (random == currentPathIndex)
        {
            random = currentPathIndex + 1;
        }
        currentPathIndex = random;

        GameObject moveToPath = path[currentPathIndex];

        this.transform.LookAt(moveToPath.transform);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveToPath.transform.position, step);
    }

    public void Idle()
    {
        Debug.Log("Idle");
        GetComponent<Animator>().SetBool("Idle", true);
    }

    public void Shoot()
    {
        if (shootIntensity == ShootIntensity.SOFT)
        {
            Shoot(1, 0);
        }
        else if (shootIntensity == ShootIntensity.NORMAL)
        {
            Shoot(2, 0.3f);
        }
        else if (shootIntensity == ShootIntensity.HARD)
        {
            Shoot(3, 0.3f);
        }

        Debug.Log("Shoot");
    }

    private void Shoot(int howManyFruit, float delay)
    {
        StartCoroutine(ShootDelay(howManyFruit, delay));
    }

    private IEnumerator ShootDelay(int howManyFruit, float delay)
    {
        for (int i = 0; i < howManyFruit; i++)
        {
            GameObject obj;

            int random = Random.Range(0, 4);
            if(random == 0) //shoot boom
            {
                obj = Instantiate(boomPrefab, barel.transform.position, Quaternion.identity);
            }
            else // shoot fruit
            {
                obj = Instantiate(fruitPrefab, barel.transform.position, Quaternion.identity);
            }

            this.transform.LookAt(targetPlayer.transform);

            Vector3 force = targetPlayer.transform.position - this.transform.position;
            force.Normalize();
            force.y += 0.2f;

            obj.GetComponent<Rigidbody>().AddForce(force * speed);

            yield return new WaitForSeconds(delay);
        }
    }
}
