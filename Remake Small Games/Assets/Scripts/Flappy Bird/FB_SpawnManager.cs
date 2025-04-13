using System.Collections;
using UnityEngine;

public class FB_SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float maxHeight = 2.5f;
    private float minHeight = -1.5f;
    public float spawnTimerMax = 1.5f;
    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            int tmp = Random.Range((int)(minHeight* 1000), (int)(maxHeight * 1000));
            float height = 1f * tmp / 1000f;
            Instantiate(obstaclePrefab, new Vector3(transform.position.x, height, 0), Quaternion.identity);
        }
    }
}