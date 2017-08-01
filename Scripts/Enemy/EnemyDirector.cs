using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDirector : MonoBehaviour {

    public GameObject RegularEnemy;
    public List<GameObject> Spawns;

    private List<GameObject> DeadEnemies = new List<GameObject>();

    private float dt;
    
    private int SpawnIndex;
    private int AliveIndex;
    private int DeadIndex;
    private int EnemySize = 10;

    private float SpawnTime;
    private float CurrentTime;
    private float MiniBossSpawnChance;
    private float CurrentChance;

    void Awake()
    {
        InstantiateEnemies();
    }

    void Start () {
        dt = 0;

        SpawnIndex = 0;
        AliveIndex = 0;
        DeadIndex = 0;

        SpawnTime = 1.5f;
        CurrentTime = 0;
        MiniBossSpawnChance = 17.5f;
	}
	
	// Update is called once per frame
	void Update () {
        dt = Time.deltaTime;

        CurrentTime += dt;
        if(CurrentTime >= SpawnTime)
        {
            CurrentTime = Random.Range(1, 100);
            SpawnLocation();
            CurrentTime = 0;
        }
	}

    void InstantiateEnemies()
    {
        for(int i = 0; i < EnemySize; i++)
        {
            DeadEnemies.Add(Instantiate(RegularEnemy));
        }
        Debug.Log(DeadEnemies.Count);
    }

    void SpawnLocation()
    {
        for(int i = 0; i < EnemySize; i++)
        {
            // is the enemy is dead, revive them
            if(DeadEnemies[i].GetComponent<EnemyHealth>().CheckIfEnemyIsDead())
            {
                SpawnIndex = Random.Range(0, 5);
                DeadEnemies[i].GetComponent<EnemyBehaviour>().SetSpawnLocation(Spawns[SpawnIndex].transform.position);
                DeadEnemies[i].GetComponent<EnemyHealth>().BringAlive();
                DeadEnemies[i].GetComponent<EnemyBehaviour>().GenerateIgnorePlayerChance();
                DeadEnemies[i].SetActive(true);
                DeadEnemies[i].GetComponent<EnemyBehaviour>().StopGhostMovement(false);

                break;
            }
        }
    }

    public void DisableAllGhosts()
    {
        for(int i = 0; i < DeadEnemies.Count; i++)
        {
            DeadEnemies[i].SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
