using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] public GameManager manager;
  [SerializeField] private SoundManager soundManager;

  [SerializeField] private PlayerShooting playerShooting;

  private AudioSource source;

  [SerializeField] float graceCooldown;
  private float graceTimer;
  private bool grace = false;

  private void Start()
  {
    source = GetComponent<AudioSource>();
    playerShooting.setSource(source);
  }
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
      source.PlayOneShot(soundManager.marineHurtClip);
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
