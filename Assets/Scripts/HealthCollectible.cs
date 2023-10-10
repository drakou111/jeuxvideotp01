using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healthAmount = 3;
    void Start()
    {
        
    }

    void Update()
    {
           
    }

    private void OnTriggerEnter(Collider collidier)
    {
        if (collidier.gameObject.CompareTag("Player")) {
            collidier.gameObject.GetComponent<Player>().heal(healthAmount);
            gameObject.SetActive(false);
        }
    }
}
