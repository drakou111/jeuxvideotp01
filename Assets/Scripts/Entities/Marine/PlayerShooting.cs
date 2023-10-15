using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.Input;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject missile;
    [SerializeField] GameObject gun;

    [SerializeField] GameManager gameManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] float shootCooldown;

    float shootTimer = 0;
    [SerializeField] private static int bulletCount = 200;
    [SerializeField] private static int maxMissiles = 15;
    [SerializeField] private float multiShotAngleOffset = 5f;

    private List<GameObject> bullets = new List<GameObject>();
    private List<GameObject> missiles = new List<GameObject>();

    private GameManager manager;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        this.manager = GetComponent<Player>().manager;
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject currentBullet = Instantiate(bullet);
            bullets.Add(currentBullet);
            currentBullet.SetActive(false);
        }

        for (int i = 0; i < maxMissiles; i++)
        {
            GameObject currentMissile = Instantiate(missile);
            missiles.Add(currentMissile);
            currentMissile.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        bool joystickConnected = this.manager.isJoystickConnected();
        if ((!joystickConnected && Input.GetButton("Fire1")) || (joystickConnected && Input.GetAxis("Fire1J") > .5))
        {
            if (shootTimer >= shootCooldown)
            {
                shootTimer = 0;
                Shoot();
            }
        }
        else if ((!joystickConnected && Input.GetButton("Fire2")) || (joystickConnected && Input.GetAxis("Fire2J") > .5))
        {
            if (shootTimer >= shootCooldown && gameManager.missiles != 0)
            {
                shootTimer = 0;
                gameManager.addMissiles(-1);
                ShootMissile();
            }
        }
    }

    void Shoot()
    {
        ShootBullet(0);
        if (gameManager.multiShotCooldown > 0)
        {
            source.PlayOneShot(soundManager.tripleShootClip);
            ShootBullet(-multiShotAngleOffset);
            ShootBullet(multiShotAngleOffset);
        }
        else
        {
            source.PlayOneShot(soundManager.shootClip);
        }
    }

    void ShootBullet(float angleOffset)
    {
        foreach (GameObject iBullet in bullets)
        {
            if (!iBullet.activeSelf)
            {
                FireBullet(iBullet, angleOffset);
                return;
            }
        }
    }
    void FireBullet(GameObject bullet, float angleOffset)
    {
        bullet.SetActive(true);
        bullet.transform.position = gun.transform.position;
        bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, angleOffset, 0); ;
    }

    void ShootMissile()
    {
        foreach (GameObject iMissile in missiles)
        {
            if (!iMissile.activeSelf)
            {
                source.PlayOneShot(soundManager.missileShotClip);
                FireMissile(iMissile);
                return;
            }
        }
    }
    void FireMissile(GameObject missile)
    {
        missile.SetActive(true);
        missile.transform.position = gun.transform.position;
        missile.transform.rotation = transform.rotation;
    }

    public void setSource(AudioSource src)
    {
        source = src;
    }

}
