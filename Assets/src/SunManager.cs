using UnityEngine;
using TMPro;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance;
    public TextMeshProUGUI sunText;
    private int sunCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        UpdateSunUI();
    }

    public void AddSun(int amount)
    {
        sunCount += amount;
        UpdateSunUI();
    }

    private void UpdateSunUI()
    {
        sunText.text = sunCount.ToString();
    }
    public int GetSunAmount()
    {
        return sunCount;
    }

    public void SpendSun(int amount)
    {
        sunCount -= amount;
        UpdateSunUI();
    }

}
