using UnityEngine;
using UnityEngine.EventSystems;

public class PlowedField : MonoBehaviour {

    private BuildManager buildManager;
    private TaskManager taskManager;

    private GameObject plant;
    private bool inQueue;

    private void Start()
    {
        buildManager = BuildManager.instance;
        taskManager = TaskManager.instance;

        inQueue = false;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetBuildMode() == BuildManager.Mode.NORMAL_MODE)
            NormalMode();
        else if (buildManager.GetBuildMode() == BuildManager.Mode.DIG_MODE)
            DigMode();
        else if (buildManager.GetBuildMode() == BuildManager.Mode.PLANT_MODE)
            PlantMode();
    }

    private void NormalMode()
    {

    }

    private void DigMode()
    {
        if (plant != null || inQueue)
            return;

        Destroy(gameObject);
    }

    private void PlantMode()
    {
        if (plant != null || inQueue)
            return;

        Blueprint blueprint = buildManager.GetBlueprint();
        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;

        SetInQueue(true);

        PlantTask _plant = new PlantTask(gameObject, (CollectableBlueprint)blueprint);
        taskManager.AddTask(_plant);
    }

    public void SetInQueue(bool inQueue)
    {
        this.inQueue = inQueue;
    }

    public void SetPlant(GameObject plant)
    {
        this.plant = plant;
    }
}
