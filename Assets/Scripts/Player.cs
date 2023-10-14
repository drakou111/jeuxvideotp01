using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerShooting playerShooting;

    [SerializeField] float graceCooldown;
    private float graceTimer;
    private bool grace = false;

    [SerializeField] int healthPickupHeal = 1;

    void Update()
    {
        if (grace)
        {
            graceTimer += Time.deltaTime;
            if (graceTimer >= graceCooldown)
            {
                grace = false;
            }
        }
    }

    public void hit(int damage)
    {
        if (!grace)
        {
            manager.hit(damage);
            graceTimer = 0;
            grace = true;
        }
    }

    public void heal(int health)
    {
        manager.heal(health);
    }

    public void addMissiles(int amount)
    {
        manager.addMissiles(amount);
    }

    public void addMultiShot(float amount)
    {
        manager.addMultiShot(amount);
    }


}
