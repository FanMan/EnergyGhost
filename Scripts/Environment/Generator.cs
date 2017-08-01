using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Generator : MonoBehaviour {

    public int Number;
    public Text Wattage;
    protected float CurrentWattage;
    private float MaxWattage;

	void Start () {
        CurrentWattage = 0f;
        MaxWattage = 100f;
        Wattage.text = "Generator " + Number + ": " + CurrentWattage + " watts";
	}
	
	// Update is called once per frame
	void Update () {
        Wattage.text = "Generator One: " + CurrentWattage + " watts";
    }

    public void FillGenerator(float VacuumAmount)
    {
        if(VacuumAmount + CurrentWattage > 100)
        {
            CurrentWattage = 100;
        }
        else if (CurrentWattage < MaxWattage)
        {
            CurrentWattage += VacuumAmount;
        }
    }

    public float CheckGeneratorWattage()
    {
        return CurrentWattage;
    }

    public void ResetWattage()
    {
        CurrentWattage = 0;
    }
}
