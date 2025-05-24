using UnityEngine;
using UnityEngine.UIElements;

public class PlaneGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(100, 100);
    public Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;


    public Vector2Int spawnAreaStart = new Vector2Int(7, 7);
    public Vector2Int spawnAreaSize = new Vector2Int(3, 3);

    public GameObject zombiePrefab;
    public LevelManager levelManager;


    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        int buildingCost = 0;

        if (buildingPrefab.TryGetComponent(out Sunflower sunflower))
        {
            buildingCost = sunflower.cost;
        }
        else if (buildingPrefab.TryGetComponent(out Peashooter peashooter))
        {
            buildingCost = peashooter.cost;
        }

        if (SunManager.Instance.GetSunAmount() < buildingCost)
        {
            Debug.Log("Не вистачає сонця!");
            return;
        }

        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;
            Debug.Log("mousePosition" + mousePosition);
            Debug.Log("worldPosition" + worldPosition);

            Vector2Int gridPosition = new Vector2Int(
                Mathf.RoundToInt(worldPosition.x),
                Mathf.RoundToInt(worldPosition.y)
            );
            if (gridPosition.x >= 0 && gridPosition.x < GridSize.x &&
                gridPosition.y >= 0 && gridPosition.y < GridSize.y)
            {
                flyingBuilding.transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);

                if (CanPlaceBuilding(gridPosition))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceBuilding(gridPosition);
                    }
                }
            }
        }
    }
    bool CanPlaceBuilding(Vector2Int gridPosition)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (grid[gridPosition.x + x, gridPosition.y + y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }


    private void OnDrawGizmos()
    {
        if (grid == null) return;

        Gizmos.color = Color.gray;

        for (int x = 0; x <= GridSize.x; x++)
        {
            Vector3 start = new Vector3(x - 0.5f, -0.5f, 0);
            Vector3 end = new Vector3(x - 0.5f, GridSize.y - 0.5f, 0);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= GridSize.y; y++)
        {
            Vector3 start = new Vector3(-0.5f, y - 0.5f, 0);
            Vector3 end = new Vector3(GridSize.x - 0.5f, y - 0.5f, 0);
            Gizmos.DrawLine(start, end);
        }
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

        Vector3 bottomLeft = new Vector3(spawnAreaStart.x - 0.5f, spawnAreaStart.y - 0.5f, 0);
        Vector3 topRight = new Vector3(spawnAreaStart.x + spawnAreaSize.x - 0.5f, spawnAreaStart.y + spawnAreaSize.y - 0.5f, 0);

        Gizmos.DrawCube(
            (bottomLeft + topRight) / 2,
            new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0)
        );
    }

    public void SpawnZombie()
    {
        if (zombiePrefab == null) return;

        int x = Random.Range(spawnAreaStart.x, spawnAreaStart.x + spawnAreaSize.x);
        int y = Random.Range(spawnAreaStart.y, spawnAreaStart.y + spawnAreaSize.y);

        if (grid[x, y] == null)
        {
            Vector3 spawnPosition = new Vector3(x, y, 0);
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombieScript != null)
            {
                zombieScript.Initialize(GridSize);
                levelManager.OnZombieSpawned();
            }
        }


    }

    private void Start()
    {
        //InvokeRepeating(nameof(SpawnZombie), 1f, 2f);
    }



    void PlaceBuilding(Vector2Int gridPosition)
    {
        int buildingCost = 0;

        if (flyingBuilding.TryGetComponent(out Sunflower sunflower))
        {
            buildingCost = sunflower.cost;
        }
        else if (flyingBuilding.TryGetComponent(out Peashooter peashooter))
        {
            buildingCost = peashooter.cost;
        }

        SunManager.Instance.SpendSun(buildingCost);

        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[gridPosition.x + x, gridPosition.y + y] = flyingBuilding;
            }
        }

        flyingBuilding = null;
    }

}