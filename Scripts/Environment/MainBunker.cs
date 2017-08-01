using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBunker : MonoBehaviour
{
    public Text BunkerHealth;
    public Text WinText;
    public Text DeadText;

    public GameObject GeneratorOne;
    public GameObject GeneratorTwo;
    public GameObject GeneratorThree;
    public GameObject Shield;
    public GameObject Director;

    private int count;
    private float health;
    private bool RestartGame = false;
    private float CurrentTime = 0;

    void Start () {
        count = 0;
        health = 100;
        BunkerHealth.text = "Bunker Health: " + health;
	}
	
	// Update is called once per frame
	void Update () {
        CheckPowerGrid();
        BunkerHealth.text = "Bunker Health: " + health;

        if(RestartGame)
        {
            CurrentTime += Time.deltaTime;

            if(CurrentTime >= 7)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    
    public void ReduceHealth(float Damage)
    {
        if(health - Damage <= 0)
        {
            DeadText.gameObject.SetActive(true);
            Director.GetComponent<EnemyDirector>().DisableAllGhosts();
            RestartGame = true;
        }
        else
            health -= Damage;
    }

    void CheckPowerGrid()
    {
        if(GeneratorOne.GetComponent<Generator>().CheckGeneratorWattage() == 100f)
        {
            count++;
            Debug.Log("Generator One online");
        }
        if (GeneratorTwo.GetComponent<Generator>().CheckGeneratorWattage() == 100f)
        {
            count++;
            Debug.Log("Generator Two online");
        }
        if (GeneratorThree.GetComponent<Generator>().CheckGeneratorWattage() == 100f)
        {
            count++;
            Debug.Log("Generator Three online");
        }
    }

    void ActivateShield()
    {
        Shield.SetActive(true);
        Director.GetComponent<EnemyDirector>().DisableAllGhosts();
        WinText.gameObject.SetActive(true);
        RestartGame = true;
    }
}
