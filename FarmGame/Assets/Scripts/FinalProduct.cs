using UnityEngine;

public class FinalProduct : Product {

    private BuildManager buildManager;
    private TaskManager taskManager;

    private bool inQueue;

    private void Start()
    {
        buildManager = BuildManager.instance;
        taskManager = TaskManager.instance;
    }

    private void OnMouseDown()
    {
        if (buildManager.GetBuildMode() == BuildManager.Mode.NORMAL_MODE)
            NormalMode();
    }

    private void NormalMode()
    {
        if (inQueue)
            return;

        SetInQueue(true);

        CollectTask _collect = new CollectTask(gameObject, blueprint);
        taskManager.AddTask(_collect);
    }

    public void SetInQueue(bool inQueue)
    {
        this.inQueue = inQueue;
    }

    
}
