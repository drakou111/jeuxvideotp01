using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
  [SerializeField] private CollectibleManager collectibleManager;
  [SerializeField] private GameManager gameManager;
  [SerializeField] private SoundManager soundManager;

  [SerializeField] private Spawner spawner;
  [SerializeField] private Alien alien;
  [SerializeField] private GameObject goal;
  [SerializeField] private static int spawnerNumber = 8;
  [SerializeField] private static int alienNumber = 20;
  [SerializeField] private static int maxTotalAlienNumber = 500;

  private Spawner[] spawners = new Spawner[spawnerNumber];
  private Alien[] aliens = new Alien[alienNumber];

  [SerializeField] private Vector3 center;
  [SerializeField] private float radius;

  [SerializeField] private float initialTime = 2;
  [SerializeField] private float timer = 0;

  [SerializeField] private float missileExplosionWidth = 200f;

  private bool hasWon = false;


  // Start is called before the first frame update
  void Start()
  {
    float angleOffset = ((Mathf.PI * 2) / spawnerNumber) / 2;
    for (int i = 0; i < spawnerNumber; i++)
    {
      spawners[i] = Instantiate(spawner);
      float angle = (Mathf.PI * 2) / spawnerNumber + angleOffset;
      spawners[i].transform.position = new Vector3(center.x + (radius * Mathf.Cos(angle * i)), center.y, center.z + (radius * Mathf.Sin(angle * i)));
      spawners[i].spawnerManager = this;
      spawners[i].gameObject.SetActive(true);
    }

    for (int i = 0; i < alienNumber; i++)
    {
      aliens[i] = initAlien();
    }
  }

  private Alien initAlien()
  {
    Alien currentAlien = Instantiate(alien);
    currentAlien.gameObject.SetActive(false);
    currentAlien.gameObject.GetComponent<AlienMovement>().goal = goal;
    currentAlien.setCollectibleManager(collectibleManager);
    currentAlien.setSpawnManager(this);
    currentAlien.setSoundManager(soundManager);
    return currentAlien;
  }
  private void setAlien(Alien currentAlien)
  {
    Spawner randomSpawner = getRandomSpawner();
    if (randomSpawner)
    {
      maxTotalAlienNumber--;
      currentAlien.gameObject.SetActive(true);
      currentAlien.transform.position = randomSpawner.transform.position;
      return;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (!hasWon)
    {
      timer += Time.deltaTime;
      if (timer >= initialTime)
      {
        timer = 0;
        spawnAlien();
      }

      if (checkIfWin())
      {
        gameManager.win();
        hasWon = true;
      }
    }
  }

  private void spawnAlien()
  {
    if (maxTotalAlienNumber > 0)
    {
      foreach (Alien currentAlien in aliens)
      {
        if (!currentAlien.gameObject.activeSelf)
        {
          setAlien(currentAlien);
          return;
        }
      }
    }
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

  private bool checkIfWin()
  {
    int spawnerCount = getEnabledSpawnersCount();
    int alienCount = getEnabledAlienCount();

    if (spawnerCount == 0 && alienCount == 0)
    {
      return true;
    }
    return false;
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

  private int getEnabledAlienCount()
  {
    int count = 0;
    for (int i = 0; i < alienNumber; i++)
    {
      if (aliens[i].gameObject.activeSelf)
      {
        count++;
      }
    }
    return count;
  }

  public void checkExplosion(Vector3 position, int explosionDamage)
  {
    foreach (Alien iAlien in aliens)
    {
      if (iAlien.gameObject.activeSelf)
      {
        float x = (iAlien.transform.position.x - position.x) * (iAlien.transform.position.x - position.x);
        float z = (iAlien.transform.position.z - position.z) * (iAlien.transform.position.z - position.z);
        float distanceFromSource = Mathf.Sqrt(x + z);
        if (distanceFromSource <= missileExplosionWidth && distanceFromSource >= 1f)
        {
          iAlien.hit(explosionDamage);
        }
      }
    }
  }
}
