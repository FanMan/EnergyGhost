using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    protected float CurrentHealth;
    private float MaxHealth;
    public bool isDead = true;

	void Start () {
        CurrentHealth = 100;
        MaxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BringAlive()
    {
        isDead = false;
    }

    public bool CheckIfEnemyIsDead()
    {
        return isDead;
    }

    public void ResetHealth()
    {
        CurrentHealth = 0;
    }

    public void DamageTaken(float Damage)
    {
        if (CurrentHealth - Damage < 0)
        {
            Death();
        }
        else
        {
            CurrentHealth -= Damage;
        }
    }

    public void Death()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        this.transform.position = new Vector3(0, 10, 0);
        this.GetComponent<EnemyBehaviour>().ResetIgnoreChance();
        CurrentHealth = MaxHealth;

        // generate the proper sprite
        for(int i = 0; i < 4; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
