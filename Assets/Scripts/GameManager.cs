using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public SoundManager soundManager;

    [SerializeField] private TMP_Text heartText;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private TMP_Text multiShotText;
    [SerializeField] private TMP_Text gameOverText;

    [SerializeField] private Player player;
    private AudioSource playerAudioSource;

    [SerializeField] public int health = 20;
    [SerializeField] public int missiles = 0;
    [SerializeField] public float multiShotCooldown = 0;


    void Start()
    {
        playerAudioSource = player.GetComponent<AudioSource>();
        updateHud();
    }

    public void hit(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            //TODO: End game
            this.health = 0;
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerShooting>().enabled = false;

            gameOverText.gameObject.SetActive(true);



            //playerAudioSource.PlayOneShot(soundManager.marineDeath);
        }
        else
        {
            //playerAudioSource.PlayOneShot(soundManager.marineHurt);
        }
        updateHud();
    }

    public void win() {
        //This function is called in SpawnerManager whenever there is no spawners and no aliens left...
        //TODO: Win
        Debug.Log("WINNER WINNER CHICKEN DINNER.");
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
    }
}
