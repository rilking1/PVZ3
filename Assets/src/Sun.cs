using UnityEngine;

public class Sun : MonoBehaviour
{
    public int sunValue = 25;

    private void OnMouseDown()
    {
        SunManager.Instance.AddSun(sunValue);
        Destroy(gameObject);
    }
}
