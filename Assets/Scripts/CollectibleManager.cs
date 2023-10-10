using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private HealthCollectible healthC;
    [SerializeField] private MissileCollectible missileC;
    [SerializeField] private MultiShotCollectible multishotC;
    [SerializeField] private static int healthCollectibleNumber = 20;
    [SerializeField] private static int missileCollectibleNumber = 20;
    [SerializeField] private static int multishotCollectibleNumber = 20;

    [SerializeField] private float spawnCollectibleOdds = 0.15f;

    private HealthCollectible[] healthCs = new HealthCollectible[healthCollectibleNumber];
    private MissileCollectible[] missileCs = new MissileCollectible[missileCollectibleNumber];
    private MultiShotCollectible[] multishotCs = new MultiShotCollectible[multishotCollectibleNumber];

    void Start()
    {
        for (int i = 0; i < healthCollectibleNumber; i++)
        {
            healthCs[i] = Instantiate(healthC);
            healthCs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < missileCollectibleNumber; i++)
        {
            missileCs[i] = Instantiate(missileC);
            missileCs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < multishotCollectibleNumber; i++)
        {
            multishotCs[i] = Instantiate(multishotC);
            multishotCs[i].gameObject.SetActive(false);
        }
    }

    void spawnHealthCollectible(Vector3 position) {
        for (int i = 0; i < healthCollectibleNumber; i++)
        {
            if (!healthCs[i].gameObject.activeSelf)
            {
                healthCs[i].transform.position = position;
                healthCs[i].gameObject.SetActive(true);
                return;
            }
        }
    }
    void spawnMissileCollectible(Vector3 position)
    {
        for (int i = 0; i < missileCollectibleNumber; i++)
        {
            if (!missileCs[i].gameObject.activeSelf)
            {
                missileCs[i].transform.position = position;
                missileCs[i].gameObject.SetActive(true);
                return;
            }
        }
    }
    void spawnMultiShotCollectible(Vector3 position)
    {
        for (int i = 0; i < multishotCollectibleNumber; i++)
        {
            if (!multishotCs[i].gameObject.activeSelf)
            {
                multishotCs[i].transform.position = position;
                multishotCs[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    void spawnRandomCollectible(Vector3 position) {
        int random = Random.Range(1, 4);
        if (random == 1) {
            spawnHealthCollectible(position);
        }
        else if (random == 2) {
            spawnMissileCollectible(position);
        } 
        else if (random == 3) {
            spawnMultiShotCollectible(position);
        }
    }

    public void trySpawnCollectible(Vector3 position)
    {

        float random = Random.Range(0f, 1f);
        if (random <= spawnCollectibleOdds) {
            spawnRandomCollectible(position);
        }
    }

    void Update()
    {
        
    }
}
