using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Text HealthText;
    public Text DeadText;
    public GameObject Director;
    private float Health;
    private bool RestartGame = false;
    private float CurrentTime = 0;

	void Start () {
        Health = 100;
        HealthText.text = "Player Health: " + Health;
	}
	
    void Update()
    {
        HealthText.text = "Player Health: " + Health;

        if (RestartGame)
        {
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= 7)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

	public float GetHealth()
    {
        return Health;
    }

    void OnTriggerEnter2D(Collider2D EnemyObject)
    {
        Debug.Log("Enemy collision");
        if(EnemyObject.CompareTag("Enemy"))
        {
            Health -= 10;
            EnemyObject.gameObject.GetComponent<EnemyHealth>().Death();

            if (Health == 0)
            {
                DeadText.gameObject.SetActive(true);
                Director.GetComponent<EnemyDirector>().DisableAllGhosts();
                DeadText.gameObject.SetActive(true);
                this.GetComponent<PlayerMovement>().enabled = false;
                RestartGame = true;
            }
        }
    }
}
