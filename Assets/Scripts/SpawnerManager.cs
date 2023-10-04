using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private Alien alien;
    [SerializeField] private GameObject goal;
    private static int spawnerNumber = 8;
    private static int alienNumber = 20;

    private Spawner[] spawners = new Spawner[spawnerNumber];
    private Alien[] aliens = new Alien[alienNumber];

    [SerializeField] private Vector3 center;
    [SerializeField] private float radius;

    [SerializeField] private float initialTime = 2;
    [SerializeField] private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        float angleOffset = ((Mathf.PI * 2) / spawnerNumber) / 2;
        for (int i = 0; i < spawnerNumber; i++)
        {
            spawners[i] = Instantiate(spawner);
            float angle = (Mathf.PI * 2) / spawnerNumber + angleOffset;
            spawners[i].transform.position = new Vector3(center.x + (radius * Mathf.Cos(angle * i)), center.y, center.z + (radius * Mathf.Sin(angle * i)));
            spawners[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < alienNumber; i++)
        {
            aliens[i] = Instantiate(alien);
            aliens[i].gameObject.SetActive(false);
            aliens[i].gameObject.GetComponent<AlienMovement>().goal = goal;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= initialTime) {
            timer = 0;
            spawnAlien();
        }
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
                Spawner randomSpawner = getRandomSpawner();
                if (randomSpawner)
                {
                    aliens[i].gameObject.SetActive(true);
                    aliens[i].transform.position = randomSpawner.transform.position;
                }
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
