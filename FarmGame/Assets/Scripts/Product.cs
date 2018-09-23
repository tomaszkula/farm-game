using UnityEngine;

public class Product : MonoBehaviour {

    protected CollectableBlueprint blueprint;
    protected double progressGrow;
    protected System.DateTime timeOfPlanting;
    protected System.DateTime timeToRise;

    public void SetMainSettings(double progressGrow, System.DateTime timeOfPlanting, System.DateTime timeToRise)
    {
        this.progressGrow = progressGrow;
        this.timeOfPlanting = timeOfPlanting;
        this.timeToRise = timeToRise;
    }

    public CollectableBlueprint GetCollectableBlueprint()
    {
        return blueprint;
    }

    public void SetCollectableBlueprint(CollectableBlueprint blueprint)
    {
        this.blueprint = blueprint;
    }
}
