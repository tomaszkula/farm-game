using UnityEngine;
using UnityEngine.EventSystems;

public class UnplowedField : MonoBehaviour {

    private BuildManager buildManager;
    private TaskManager taskManager;

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
    }

    private void NormalMode()
    {
        //if (PlayerStats.Money < blueprint.cost)
        //return;

        if (inQueue)
            return;

        SetInQueue(true);

        PlowTask _plow = new PlowTask(gameObject, buildManager.plowedFieldBlueprint);
        taskManager.AddTask(_plow);
    }

    private void DigMode()
    {
        if (inQueue)
            return;

        Destroy(gameObject);
    }

    public void SetInQueue(bool inQueue)
    {
        this.inQueue = inQueue;
    }
}
