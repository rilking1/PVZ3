using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sunPrefab;
    public float spawnInterval = 7f;
    public Vector2 spawnRangeX = new Vector2(1f, 9f);
    public float spawnHeight = 6f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSun), spawnInterval, spawnInterval);
    }

    void SpawnSun()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, -1f);

        GameObject sun = Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
        sun.AddComponent<SunFall>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Vector3 center = new Vector3((spawnRangeX.x + spawnRangeX.y) / 2, spawnHeight, -1f);
        Vector3 size = new Vector3(spawnRangeX.y - spawnRangeX.x, 0.5f, 0.1f);
        Gizmos.DrawCube(center, size);
    }
}
