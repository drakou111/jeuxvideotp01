using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerManager : MonoBehaviour
{
  [SerializeField] private CollectibleManager collectibleManager;

  [SerializeField] private Spawner spawner;
  [SerializeField] private GameObject alien;
  [SerializeField] private GameObject goal;
  [SerializeField] private static int spawnerNumber = 8;
  [SerializeField] private static int alienNumber = 20;
  [SerializeField] private static int maxTotalAlienNumber = 500;
  private int alienCount = 0;

  private Spawner[] spawners = new Spawner[spawnerNumber];
  private List<GameObject> aliens = new List<GameObject>();

  [SerializeField] private Vector3 center;
  [SerializeField] private float radius;

  [SerializeField] private float initialTime = 2;
  [SerializeField] private float timer = 0;

  [SerializeField] private float missileExplosionWidth = 100f;


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
      GameObject currentAlien = Instantiate(alien);
      aliens.Add(currentAlien);
      initAlien(currentAlien);
    }
  }

  private void initAlien(GameObject iAlien)
  {
    iAlien.gameObject.SetActive(false);
    iAlien.gameObject.GetComponent<AlienMovement>().goal = goal;
    iAlien.gameObject.GetComponent<Alien>().collectibleManager = collectibleManager;
    iAlien.gameObject.GetComponent<Alien>().spawnManager = this;
  }

  private void setAlien(GameObject iAlien)
  {
    Spawner randomSpawner = getRandomSpawner();
    if (randomSpawner)
    {
      iAlien.gameObject.SetActive(true);
      iAlien.transform.position = randomSpawner.transform.position;
      return;
    }
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;
    if (timer >= initialTime)
    {
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

  private void spawnAlien()
  {
    foreach (GameObject iAlien in aliens)
    {
      if (!iAlien.gameObject.activeSelf)
      {
        setAlien(iAlien);
        return;
      }
    }
    GameObject newAlien = Instantiate(alien);
    aliens.Add(newAlien);
    initAlien(newAlien);
    setAlien(newAlien);
  }

  private Spawner getRandomSpawner()
  {
    int numberValidSpawners = getEnabledSpawnersCount();
    Spawner[] validSpawners = new Spawner[numberValidSpawners];
    if (validSpawners.Length <= 0)
    {
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

  private int getEnabledSpawnersCount()
  {
    int count = 0;
    for (int i = 0; i < spawnerNumber; i++)
    {
      if (spawners[i].gameObject.activeSelf)
      {
        count++;
      }
    }
    return count;
  }

  public void checkExplosion(Vector3 position)
  {
    foreach (GameObject iAlien in aliens)
    {
      if (iAlien.gameObject.activeSelf)
      {
        float x = (iAlien.transform.position.x - position.x) * (iAlien.transform.position.x - position.x);
        float z = (iAlien.transform.position.z - position.z) * (iAlien.transform.position.z - position.z);
        float distanceFromSource = Mathf.Sqrt(x + z);
        if (distanceFromSource <= missileExplosionWidth && distanceFromSource >= 1f)
        {
          iAlien.GetComponent<Alien>().deactivate();
        }
      }
    }
  }
}
