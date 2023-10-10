using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollectible : MonoBehaviour
{
    [SerializeField] private int missileAmount = 5;
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
            collidier.gameObject.GetComponent<Player>().addMissile(missileAmount);
            source.PlayOneShot(soundManager.powerupPickup);
            gameObject.SetActive(false);
        }
    }
}
