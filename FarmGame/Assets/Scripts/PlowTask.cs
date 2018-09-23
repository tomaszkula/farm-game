using UnityEngine;

public class PlowTask : Task {

    public PlowTask(GameObject go, Blueprint blueprint) : base(go, blueprint)
    {
    }

    public override void DoTask()
    {
        int width = blueprint.width;
        int height = blueprint.height;
        int x = (int)(go.transform.position.x - (width - 1) / 2.0f);
        int z = (int)(go.transform.position.z + (height - 1) / 2.0f);

        GameObject _prefab = GameObject.Instantiate(blueprint.mainPrefab, go.transform.position, blueprint.mainPrefab.transform.rotation);
        _prefab.transform.SetParent(go.transform.parent);
        go.transform.parent.GetComponent<GridSystem>().PutOnGrid(x, z, width, height, _prefab);

        GameObject.Destroy(go);
        go.GetComponent<UnplowedField>().SetInQueue(false);
        Finished = true;
    }
}
