using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private TMP_Text heartText;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private TMP_Text multiShotText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text victoryText;

    [SerializeField] private Player player;

    [SerializeField] public int health = 20;
    [SerializeField] public int missiles = 0;
    [SerializeField] public float multiShotCooldown = 0;


    void Start()
    {
        updateHud();
    }

    public void hit(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            gameOver();
        }
        updateHud();
    }

    public void win()
    {
        soundManager.victorySound();
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
        victoryText.gameObject.SetActive(true);
    }

    public void gameOver()
    {
        soundManager.playMarineDeathSound(player.gameObject.transform.position);
        this.health = 0;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;

        gameOverText.gameObject.SetActive(true);
    }

    public void heal(int health)
    {
        this.health += health;
        updateHud();
    }

    public void addMissiles(int amount)
    {
        this.missiles += amount;
        if (this.missiles <= 0) this.missiles = 0;
        updateHud();
    }

    public void addMultiShot(float amount)
    {
        this.multiShotCooldown += amount;
        if (this.multiShotCooldown <= 0) this.multiShotCooldown = 0;
        updateHud();
    }

    void updateHud()
    {
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

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public bool isJoystickConnected()
    {
        string[] joysticks = Input.GetJoystickNames();
        for (int i = 0; i < joysticks.Length; i++) {
            if (joysticks[i] != "") {
                return true;
            }
        }
        return false;
    }
}
