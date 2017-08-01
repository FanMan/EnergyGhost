using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroGun : MonoBehaviour
{
    private RaycastHit2D hit;

    private float CurrentWattageCapacity;
    protected float MaxWattageCapacity;
    private float WattageAmountToGive;

    private float dt;
    private float RateOfDamage;
    private float CurrentTime;
    
	void Start () {
        dt = 0;

        CurrentWattageCapacity = 0;
        MaxWattageCapacity = 100;
        WattageAmountToGive = 1;

        RateOfDamage = 0.23f;
        CurrentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        dt = Time.deltaTime;

	    if(Input.GetMouseButton(0))
        {
            CurrentTime += dt;
            
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

            if(CurrentTime >= RateOfDamage)
            {
                // this if statement is to prevent errors from appearing in console
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<EnemyHealth>().DamageTaken(30);
                        hit.collider.gameObject.GetComponent<EnemyBehaviour>().StopGhostMovement(true);
                        CurrentWattageCapacity += 2;
                    }
                }
                CurrentTime = 0;
            }
        }
        else
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

            if(hit.collider != null)
            {
                if(hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<EnemyBehaviour>().StopGhostMovement(false);
                }
            }
        }
	}

    void FixedUpdate()
    {
        ActivateGun();
    }

    public float GetWattage()
    {
        if (CurrentWattageCapacity == 0)
            return 0;
        else
        {
            CurrentWattageCapacity -= WattageAmountToGive;
            return WattageAmountToGive;
        }
    }

    // for powerups
    public void ExpandWattageCapacity(float WattageAmount)
    {
        MaxWattageCapacity = WattageAmount;
    }

    // not needed
    public void IncreaseCurrentWattage(float AmountGained)
    {
        CurrentWattageCapacity += AmountGained;
    }

    void ActivateGun()
    {
        hit = Physics2D.Raycast(this.transform.position, this.transform.right, 3, 1 << 8);

        //if(hit.collider.tag == "Enemy")
        //    Debug.Log("Collision with " + hit.collider.tag);
        Debug.DrawRay(this.transform.position, this.transform.right * 3, Color.white);
    }
}
