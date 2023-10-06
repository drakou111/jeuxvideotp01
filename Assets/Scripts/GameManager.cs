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
    [SerializeField] int missileCooldown = 0;
    [SerializeField] int multiShotCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateHud();
    }

    public void hit(int health)
    {
        this.health -= health;
        if (this.health < 0)
        {
            this.health = 0;
            //TODO
        }
        updateHud();
    }

    void updateHud() { 
        this.heartText.text = health.ToString();
        this.missileText.text = missileCooldown.ToString();
        this.multiShotText.text = multiShotCooldown.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
