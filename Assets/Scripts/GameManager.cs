using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text heartText;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private TMP_Text multiShotText;

    [SerializeField] int health = 20;
    [SerializeField] int missiles = 0;
    [SerializeField] float multiShotCooldown = 0;


    void Start()
    {
        updateHud();
    }

    public void hit(int health)
    {
        this.health = Mathf.Max(0, this.health - health);
        if (this.health == 0)
        {
            //TODO: End game
        }
        updateHud();
    }

    public void heal(int health) {
        this.health += health;
        updateHud();
    }

    public void addMissile(int amount) { 
        this.missiles += amount;
        updateHud();
    }

    public void addMultiShot(int amount) { 
        this.multiShotCooldown += amount;
        updateHud();
    }

    void updateHud() { 
        this.heartText.text = health.ToString();
        this.missileText.text = missiles.ToString();
        this.multiShotText.text = Mathf.Ceil(multiShotCooldown).ToString();
    }

    void Update()
    { 
        if (multiShotCooldown > 0)
        {
            multiShotCooldown = Mathf.Max(0, multiShotCooldown - Time.deltaTime);
            updateHud();
        }
    }
}
