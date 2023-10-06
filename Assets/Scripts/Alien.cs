using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    [SerializeField] private const float crushVelocityThreshold = -1;
    [SerializeField] int initialHealth = 2;
    [SerializeField] int health = 2;
    [SerializeField] int damage = 1;

    [SerializeField] UnityEvent<Vector3> trySpawnCollectibleEvent;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            this.health = 0;
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.y >= crushVelocityThreshold)
            {
                collision.gameObject.GetComponent<Player>().hit(damage);
            }
            deactivateIfDead();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damage = collision.gameObject.GetComponent<Bullet>().getDamage();
            hit(damage);
            //TODO: disable bullet
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    void deactivateIfDead() {
        if (this.health <= 0 && gameObject.activeSelf)
        {
            this.health = this.initialHealth;
            gameObject.SetActive(false);
            trySpawnCollectibleEvent.Invoke(transform.position);
        }
    }

    void hit(int health)
    {
        this.health -= health;
        deactivateIfDead();
    }
}