using UnityEngine;

public class SunFall : MonoBehaviour
{
    public float fallSpeed = 1f;
    public float minY = -3f;

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= minY)
        {
            Destroy(gameObject);
        }
    }
}
