using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    private static int spawnerNumber = 8;

    private Spawner[] spawners = new Spawner[spawnerNumber];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnerNumber; i++) {
            spawners[i] = Instantiate(spawner);
            spawners[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnSpawner() { 
        for (int i = 0; i < spawnerNumber; i++)
        {
            if (!spawners[i].gameObject.activeSelf)
            {
                spawners[i].gameObject.SetActive(true);
                return;
            }
        }
    }


}
