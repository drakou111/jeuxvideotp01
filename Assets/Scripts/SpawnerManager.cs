using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] Alien alien;
    private static int spawnerNumber = 8;
    private static int alienNumber = 20;

    private Spawner[] spawners = new Spawner[spawnerNumber];
    private Alien[] aliens = new Alien[alienNumber];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnerNumber; i++)
        {
            spawners[i] = Instantiate(spawner);
            spawners[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < alienNumber; i++)
        {
            aliens[i] = Instantiate(alien);
            aliens[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnSpawner()
    {
        for (int i = 0; i < spawnerNumber; i++)
        {
            if (!spawners[i].gameObject.activeSelf)
            {
                spawners[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    private void spawnAlien() {
        for (int i = 0; i < alienNumber; i++)
        {
            if (!aliens[i].gameObject.activeSelf)
            {
                aliens[i].gameObject.SetActive(true);
                Spawner randomSpawner = getRandomSpawner();

                aliens[i].transform.position = randomSpawner.transform.position;
                return;
            }
        }
    }

    private Spawner getRandomSpawner()
    {
        int numberValidSpawners = getEnabledSpawnersCount();
        Spawner[] validSpawners = new Spawner[numberValidSpawners];
        if (validSpawners.Length <= 0) {
            return null;
        }
        int offset = 0;
        for (int i = 0; i < spawnerNumber; i++)
        {
            if (spawners[i].gameObject.activeSelf)
            {
                validSpawners[offset] = spawners[i];
                offset++;
            }
        }


        int random = Random.Range(0, numberValidSpawners);
        return validSpawners[random];
    }

    private int getEnabledSpawnersCount() {
        int count = 0;
        for (int i = 0; i < spawnerNumber; i++) {
            if (spawners[i].gameObject.activeSelf) {
                count++;
            }
        }
        return count;
    }


}
