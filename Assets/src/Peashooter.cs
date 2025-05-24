using UnityEngine;

public class Peashooter : MonoBehaviour
{
    public GameObject peaBulletPrefab;
    public float shootInterval = 2f;
    public int cost = 100;


    private void Start()
    {
        InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
    }

    void Shoot()
    {

        if (IsZombieInLine())
        {
            Vector3 spawnPosition = transform.position + new Vector3(0.5f, 0, 0);
            Instantiate(peaBulletPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsZombieInLine()
    {

        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (var zombie in zombies)
        {
            if (Mathf.RoundToInt(zombie.transform.position.y) == Mathf.RoundToInt(transform.position.y))
            {
                return true;
            }
        }
        return false;
    }
}
