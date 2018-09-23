using UnityEngine;

public class MainBoardManager : MonoBehaviour {

    private BuildManager buildManager;
    private FollowMouse followMouse;

    public CollectableBlueprint plant1;

    private void Start()
    {
        buildManager = BuildManager.instance;
        followMouse = FollowMouse.instance;
    }

    public void NormalMode()
    {
        Debug.Log("normal mode");
        buildManager.SetBuildMode(BuildManager.Mode.NORMAL_MODE);
    }

    public void PlowMode()
    {
        Debug.Log("plow mode");
        buildManager.SetBuildMode(BuildManager.Mode.PUT_ON_GRID_MODE);

        buildManager.SetBlueprint(buildManager.plowedFieldBlueprint);
        followMouse.SetCursor(buildManager.plowedFieldBlueprint.mainPrefab);
    }

    public void DigMode()
    {
        Debug.Log("dig mode");
        buildManager.SetBuildMode(BuildManager.Mode.DIG_MODE);
    }

    public void Shop()
    {
        Debug.Log("plant mode");
        buildManager.SetBuildMode(BuildManager.Mode.PLANT_MODE);

        buildManager.SetBlueprint(plant1);
    }
}
