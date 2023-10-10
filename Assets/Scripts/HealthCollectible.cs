using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healthAmount = 3;
    private AudioSource source;
    public SoundManager soundManager;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();   
    }

    void Update()
    {
           
    }

    private void OnTriggerEnter(Collider collidier)
    {
        if (collidier.gameObject.CompareTag("Player")) 
        {
            collidier.gameObject.GetComponent<Player>().heal(healthAmount);
            source.PlayOneShot(soundManager.powerupPickup);
            gameObject.SetActive(false);
        }
    }
}
