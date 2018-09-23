using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private Mode buildMode;
    public enum Mode { NORMAL_MODE, PUT_ON_GRID_MODE, DIG_MODE, PLANT_MODE }

    public Blueprint unplowedFieldBlueprint;
    public Blueprint plowedFieldBlueprint;
    private Blueprint blueprint;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 BuildManager");
            return;
        }

        instance = this;
    }

    //BUILD MODE
    private void Start()
    {
        buildMode = Mode.NORMAL_MODE;
    }

    public Mode GetBuildMode()
    {
        return buildMode;
    }

    public void SetBuildMode(Mode buildMode)
    {
        this.buildMode = buildMode;
    }

    //BLUEPRINT
    public Blueprint GetBlueprint()
    {
        return blueprint;
    }

    public void SetBlueprint(Blueprint blueprint)
    {
        this.blueprint = blueprint;
    }
}
