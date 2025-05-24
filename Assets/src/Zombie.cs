using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 2f;
    public int health = 100;
    private Vector2Int gridSize;

    public void Initialize(Vector2Int gridSize)
    {
        this.gridSize = gridSize;
    }

    private void Update()
    {

        transform.position += Vector3.left * speed * Time.deltaTime;

 
        if (transform.position.x < -0.5f)
        {
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Зомбі отримав {damage} шкоди. Залишилось HP: {health}");

        if (health <= 0)
        {
            LevelManager.Instance.OnZombieDied();
            Destroy(gameObject);
            Debug.Log("Зомбі знищений!");
        }

    }

    private void Start()
    {
        gameObject.tag = "Zombie";
    }
}
