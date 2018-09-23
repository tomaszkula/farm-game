using UnityEngine;

public class GridSystem : MonoBehaviour {

    private BuildManager buildManager;

    private GameObject[,] grid;
    private float gridSize = 1.0f;

    private void Start()
    {
        buildManager = BuildManager.instance;

        int size = PlayerStats.FarmSize;
        grid = new GameObject[size, size];
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (buildManager.GetBuildMode() == BuildManager.Mode.PUT_ON_GRID_MODE)
                PutOnGridMode(SnapToGrid(hit.point));
            //else if (buildManager.GetBuildMode() == "MoveMode")
            //MoveMode(GetNearestPointOnGrid(hit.point));
        }
    }

    private void PutOnGridMode(Vector3 targetPosition)
    {
        Blueprint blueprint = buildManager.GetBlueprint();
        int x = (int)targetPosition.x;
        int y = (int)targetPosition.z;
        int width = blueprint.width;
        int height = blueprint.height;

        if (!CanPutOnGrid(x, y, width, height))
            return;

        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;

        float newX = x + (width - 1) / 2.0f;
        float newY = blueprint.mainPrefabPosY;
        float newZ = y - (height - 1) / 2.0f;
        GameObject _prefab = Instantiate(blueprint.mainPrefab, new Vector3(newX, newY, newZ), blueprint.mainPrefab.transform.rotation);
        _prefab.transform.SetParent(transform);

        PutOnGrid(x, y, width, height, _prefab);
    }

    public bool CanPutOnGrid(int posX, int posY, int width, int height)
    {
        int size = PlayerStats.FarmSize;

        if (posX < 0 || posX > size - 1 || posY < 0 || posY > size - 1
            || posX + width > size || posY - height < -1)
            return false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[posX + x, posY - y] != null) return false;
            }
        }
        return true;
    }

    public void PutOnGrid(int posX, int posY, int width, int height, GameObject prefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[posX + x, posY - y] = prefab;
            }
        }
    }

    public Vector3 SnapToGrid(Vector3 position)
    {
        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int yCount = Mathf.RoundToInt(position.y / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);

        Vector3 result = new Vector3(
            (float)xCount * gridSize,
            (float)yCount * gridSize,
            (float)zCount * gridSize);

        return result;
    }

    private void OnDrawGizmos()
    {
        for (float x = 0; x <= 40; x += gridSize)
        {
            for (float z = 0; z <= 40; z += gridSize)
            {
                var point = SnapToGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
