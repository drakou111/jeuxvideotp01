using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] float speed = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hit(int health)
    {
        manager.hit(health);
    }

    public void heal(int health)
    {
        manager.heal(health);
    }

    public void addMissile(int amount) {
        manager.addMissile(amount);
    }

    public void addMultiShot(int amount) {
        manager.addMultiShot(amount);
    }
}
