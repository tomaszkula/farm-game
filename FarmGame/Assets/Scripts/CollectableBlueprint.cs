using UnityEngine;

[System.Serializable]
public class CollectableBlueprint : Blueprint {

    public GameObject mediumPrefab;
    public GameObject finalPrefab;
    public float mediumPrefabPosY;
    public float finalPrefabPosY;

    public int prize;

    public int daysToGrowUp;
    public int hoursToGrowUp;
    public int minutesToGrowUp;
}