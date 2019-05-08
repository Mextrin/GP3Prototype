using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destination : MonoBehaviour
{
    int health = 0;
    int health2 = 0;
    int damage = 1;

    public PlayerController[] players;

    public bool useHealthBar;
    public Slider healthSlider;
    public Text healthText;
    public Text healthGoingUPText;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        health2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(useHealthBar)
        {
            if(healthSlider && healthText && healthGoingUPText)
            {
                healthSlider.gameObject.SetActive(true);
                healthText.gameObject.SetActive(true);
                healthGoingUPText.gameObject.SetActive(false);

                healthSlider.value = health;
                healthText.text = health.ToString();
            }

            if (health <= 0)
            {
                //health = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    if(players[i])
                    {
                        if(players[i].GetComponent<PlayerController>().enabled == true)
                        {
                            players[i].GetComponent<PlayerController>().enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            if (healthSlider && healthText && healthGoingUPText)
            {
                healthSlider.gameObject.SetActive(false);
                healthText.gameObject.SetActive(false);

                healthGoingUPText.gameObject.SetActive(true);
                healthGoingUPText.text = health2.ToString();
            }

            if (health2 >= 100)
            {
                //health2 = 100;
                for (int i = 0; i < players.Length; i++)
                {
                    if(players[i])
                    {
                        players[i].GetComponent<PlayerController>().enabled = false;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            LoseHealth();
        }
    }

    void LoseHealth()
    {
        health -= damage;
        health2 += damage;
    }
}
