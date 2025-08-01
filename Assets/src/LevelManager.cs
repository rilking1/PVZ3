using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZombieWave
{
    public int zombieCount;
    public float delayBetweenZombies;
}

[System.Serializable]
public class Level
{
    public List<ZombieWave> waves;
}


public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public PlaneGrid planeGrid;
    public float delayBetweenWaves = 3f;

    private int currentLevelIndex = 0;
    private int currentWaveIndex = 0;
    private int aliveZombies = 0;
    private bool isGameOver = false;
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnZombieSpawned()
    {
        aliveZombies++;
    }

    public void OnZombieDied()
    {
        aliveZombies--;
    }

    public void StartLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        StopAllCoroutines();
        StartCoroutine(RunLevel(currentLevelIndex));
    }

    private void Start()
    {
        //StartCoroutine(RunLevel(currentLevelIndex));
    }
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        StopAllCoroutines();

        UIManager ui = FindObjectOfType<UIManager>();
        ui.ShowLoseScreen();

        Debug.Log("�������!");
    }

    private IEnumerator RunLevel(int levelIndex)
    {
        if (levelIndex >= levels.Count)
        {
            Debug.Log("�� ���� ��������!");
            UIManager ui = FindObjectOfType<UIManager>();
            ui.ShowWinScreen();
            yield break;
        }

        Level level = levels[levelIndex];

        for (int i = 0; i < level.waves.Count; i++)
        {
            currentWaveIndex = i;
            ZombieWave wave = level.waves[i];

            Debug.Log($"����� {i + 1} � {level.waves.Count}");
            for (int j = 0; j < wave.zombieCount; j++)
            {
                planeGrid.SpawnZombie();
                yield return new WaitForSeconds(wave.delayBetweenZombies);
            }

            yield return new WaitUntil(() => aliveZombies == 0);
            yield return new WaitForSeconds(delayBetweenWaves);
        }

        Debug.Log($"г���� {levelIndex + 1} ���������!");

        currentLevelIndex++;

        // ����������, �� ����� ����� ���� � �������� ��������
        if (currentLevelIndex >= levels.Count)
        {
            Debug.Log("�� ���� ��������! ��������!");
            UIManager ui = FindObjectOfType<UIManager>();
            ui.ShowWinScreen();
            yield break;
        }

        // ������ ��������� ��������� �����
        StartCoroutine(RunLevel(currentLevelIndex));

    }

}
