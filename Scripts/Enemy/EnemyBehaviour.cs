using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject Player;
    public bool IgnorePlayer;

    protected bool StopMovement;
    private float dt;
    private float MovingSpeed;
    private float PlayerRange;
    private float IgnorePlayerChance;
    protected float Chance;
	
	void Start () {
        dt = 0;
        IgnorePlayer = false;
        StopMovement = false;

        MovingSpeed = 0.76f; // 1.23
        //ObjectiveRange = 15f;
        PlayerRange = 5f;

        IgnorePlayerChance = 0.4f;
        Chance = 0;
	}
	
	void Update () {
        dt = Time.deltaTime;

        if (StopMovement == false)
        {
            if (IgnorePlayer)
            {
                this.transform.GetChild(2).gameObject.SetActive(true);
                this.transform.Translate(new Vector3(0, -MovingSpeed, 0) * dt);
            }
            else
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                AlertRange();
            }
        }
	}

    public void StopGhostMovement(bool stop)
    {
        StopMovement = stop;
    }

    public void SetSpawnLocation(Vector3 SpawnLocation)
    {
        this.transform.position = SpawnLocation;
    }

    public void GenerateIgnorePlayerChance()
    {
        Chance = Random.Range(0,1);

        if(Chance <= IgnorePlayerChance)
        {
            IgnorePlayer = true;
        }
    }

    public void ResetIgnoreChance()
    {
        IgnorePlayer = false;
    }

    void AlertRange()
    {
        if(Vector3.Distance(this.transform.position, Player.transform.position) < PlayerRange)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, MovingSpeed * dt);
        }
        else
        {
            this.transform.Translate(new Vector3(0, -MovingSpeed, 0) * dt);
        }
    }

    void OnTriggerEnter2D(Collider2D BunkerObject)
    {
        if(BunkerObject.CompareTag("Bunker"))
        {
            this.GetComponent<EnemyHealth>().Death();
            BunkerObject.GetComponent<MainBunker>().ReduceHealth(10);
        }
    }
}
