using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGenerator : MonoBehaviour
{
    public GameObject GeneratorObject;

    private GameObject Player;

    private bool Charging = false;
    private float RateOfCharge = 0.1f;
    private float CurrentTime = 0;
    private float dt = 0;

    void OnTriggerEnter2D(Collider2D PlayerObject)
    {
        if(PlayerObject.CompareTag("Player"))
        {
            Charging = true;
            Player = PlayerObject.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D PlayerObject)
    {
        if(PlayerObject.CompareTag("Player"))
        {
            Charging = false;
        }
    }

    void Update()
    {
        dt = Time.deltaTime;

        if(Charging)
        {
            CurrentTime += dt;

            if(CurrentTime >= RateOfCharge)
            {
                GeneratorObject.GetComponent<Generator>().FillGenerator(Player.GetComponent<ElectroGun>().GetWattage());
                CurrentTime = 0;
            }
        }
    }
}
