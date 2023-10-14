  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public SpawnerManager spawnerManager;
    [SerializeField] private int health = 20;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            hit(collider.gameObject.GetComponent<Bullet>().getDamage());
            collider.gameObject.SetActive(false);
        } 
        else if (collider.gameObject.CompareTag("Missile"))
        {
            collider.gameObject.SetActive(false);
            hit(collider.gameObject.GetComponent<Bullet>().getDamage());
            spawnerManager.checkExplosion(transform.position, collider.gameObject.GetComponent<Bullet>().getDamage());
        }
    }

    private void hit(int damage) {
        this.health -= damage;
        if (this.health <= 0) {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
