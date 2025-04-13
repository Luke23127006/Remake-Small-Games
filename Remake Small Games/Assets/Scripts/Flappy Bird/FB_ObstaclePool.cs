using UnityEngine;

public class FB_ObstaclePool : MonoBehaviour
{
    public GameObject prefab; // Prefab of the obstacle to be pooled
    private GameObject[] obstaclePrefabs; // Array of obstacle prefabs to be used in the pool
    public int poolSize = 10; // Number of obstacles to create in the pool

    private float maxHeight = 2.5f;
    private float minHeight = -1.5f;
    public float spawnTimerMax = 1.5f;
    private float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstaclePrefabs = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            obstaclePrefabs[i] = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            obstaclePrefabs[i].SetActive(false);
        }

        spawnTimer = spawnTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (FB_GameManager.Instance.isGameOver || !FB_GameManager.Instance.isGameStarted) return;
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = spawnTimerMax;
            int tmp = Random.Range((int)(minHeight * 1000), (int)(maxHeight * 1000));
            float height = 1f * tmp / 1000f;
            GameObject obstacle = GetObstacleFromPool();
            if (obstacle != null)
            {
                obstacle.transform.position = new Vector3(transform.position.x, height, 0);
                obstacle.SetActive(true);
            }
        }
    }

    private GameObject GetObstacleFromPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!obstaclePrefabs[i].activeInHierarchy)
            {
                obstaclePrefabs[i].GetComponent<BoxCollider2D>().enabled = true;
                return obstaclePrefabs[i];
            }
        }
        return null;
    }
}
