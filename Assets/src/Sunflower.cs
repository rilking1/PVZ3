using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public GameObject sunPrefab;
    public float spawnInterval = 5f;
    public int cost = 50;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnSun), spawnInterval, spawnInterval);
    }

    void SpawnSun()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, -0.5f, -1f);
        Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
    }
}