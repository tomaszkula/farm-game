using UnityEngine;

public class PlantTask : Task {

    public PlantTask(GameObject go, CollectableBlueprint plantBlueprint) : base(go, plantBlueprint)
    {
    }

    public override void DoTask()
    {
        CollectableBlueprint plantBlueprint = (CollectableBlueprint)blueprint;

        Vector3 targetPosition = go.transform.position;
        targetPosition.y = plantBlueprint.mainPrefabPosY;
        GameObject _plant = GameObject.Instantiate(plantBlueprint.mainPrefab, targetPosition, plantBlueprint.mainPrefab.transform.rotation);
        _plant.GetComponent<MainProduct>().SetCollectableBlueprint(plantBlueprint);
        _plant.transform.SetParent(go.transform);
        go.GetComponent<PlowedField>().SetPlant(_plant);

        go.GetComponent<PlowedField>().SetInQueue(false);
        Finished = true;
    }
}
