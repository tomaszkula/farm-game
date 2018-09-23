using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public static FollowMouse instance;

    private BuildManager buildManager;
    private GridSystem grid;

    private GameObject cursor;
    private int typeHightlight = 0;
    public Material cursorMaterial;

    public Color yellowCollor = new Color(0.9f, 1f, 0.3f, 1);
    public Color blueColor = new Color(0f, 0.2f, 1, 1);
    public Color redCollor = new Color(0.8f, 0.2f, 0.2f, 1);

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 FollowMouse");
            return;
        }

        instance = this;
    }

    private void Start ()
    {
        buildManager = BuildManager.instance;
        grid = FindObjectOfType<GridSystem>();
    }

    private void Update ()
    {
        if (buildManager.GetBuildMode() != BuildManager.Mode.PUT_ON_GRID_MODE)
        {
            if (cursor != null)
                SetCursor(null);

            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = grid.SnapToGrid(hit.point);

            int width = buildManager.GetBlueprint().width;
            int height = buildManager.GetBlueprint().height;
            float x = point.x + (width - 1) / 2.0f;
            float z = point.z - (height - 1) / 2.0f;
            Vector3 finalPoint = new Vector3(x-6.1f, 0f+5, z-6.1f);
            cursor.transform.position = finalPoint;

            if (grid.CanPutOnGrid((int)point.x, (int)point.z, width, height))
            {
                if (typeHightlight == 1) return;

                cursorMaterial.SetColor("_OutlineColor", blueColor);
                typeHightlight = 1;
            }
            else
            {
                if (hit.collider.CompareTag("Background"))
                {
                    if (typeHightlight == 0) return;

                    cursorMaterial.SetColor("_OutlineColor", yellowCollor);
                    typeHightlight = 0;
                }
                else
                {
                    if (typeHightlight == 2) return;

                    cursorMaterial.SetColor("_OutlineColor", redCollor);
                    typeHightlight = 2;
                }
            }
        }
    }

    public void SetCursor(GameObject cursor)
    {
        if(cursor == null)
        {
            Destroy(this.cursor);
            return;
        }

        cursorMaterial.SetColor("_Color", cursor.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.GetColor("_Color"));
        cursorMaterial.SetFloat("_OutlineWidth", 1.2f);
        cursorMaterial.SetColor("_OutlineColor", yellowCollor);
        typeHightlight = 0;

        this.cursor = Instantiate(cursor);
        foreach (var comp in this.cursor.GetComponents<Component>())
        {
            if (!(comp is Transform)) Destroy(comp);
        }
        this.cursor.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = cursorMaterial;
    }
}
