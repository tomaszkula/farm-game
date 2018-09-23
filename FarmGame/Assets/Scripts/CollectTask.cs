using UnityEngine;

public class CollectTask : Task {

    public CollectTask(GameObject go, CollectableBlueprint plantBlueprint) : base(go, plantBlueprint)
    {
    }

    public override void DoTask()
    {
        CollectableBlueprint plantBlueprint = (CollectableBlueprint) blueprint;
        PlayerStats.Money += plantBlueprint.prize;

        if (go.CompareTag("Plant"))
            CollectPlant();
        else if (go.CompareTag("Tree"))
            CollectTree();

        go.GetComponent<FinalProduct>().SetInQueue(false);
        Finished = true;
    }

    private void CollectPlant()
    {
        Blueprint blueprint = BuildManager.instance.unplowedFieldBlueprint;
        int width = blueprint.width;
        int height = blueprint.height;
        int x = (int)(go.transform.position.x - (width - 1) / 2.0f);
        int z = (int)(go.transform.position.z + (height - 1) / 2.0f);

        GameObject _prefab = GameObject.Instantiate(blueprint.mainPrefab, go.transform.parent.position, blueprint.mainPrefab.transform.rotation);
        _prefab.transform.SetParent(go.transform.parent.parent);
        go.transform.parent.parent.GetComponent<GridSystem>().PutOnGrid(x, z, width, height, _prefab);

        GameObject.Destroy(go.transform.parent.gameObject);
        //GameObject.Destroy(go);
    }

    private void CollectTree()
    {
        Vector3 targetPosition = go.transform.position;
        targetPosition.y = blueprint.mainPrefabPosY;
        GameObject newPlant = GameObject.Instantiate(blueprint.mainPrefab, targetPosition, go.transform.rotation);
        newPlant.GetComponent<MainProduct>().SetCollectableBlueprint((CollectableBlueprint)blueprint);
        newPlant.transform.SetParent(go.transform.parent);
        newPlant.GetComponentInParent<GridSystem>().PutOnGrid((int)targetPosition.x, (int)targetPosition.z, blueprint.width, blueprint.height, newPlant);

        GameObject.Destroy(go);
    }
}
