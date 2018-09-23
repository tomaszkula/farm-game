using UnityEngine;

public class MainProduct : Product {

    private void Start()
    {
        timeOfPlanting = System.DateTime.Now;
        timeToRise = timeOfPlanting.AddDays(blueprint.daysToGrowUp).AddHours(blueprint.hoursToGrowUp).AddMinutes(blueprint.minutesToGrowUp);

        progressGrow = 0;
    }

    private void Update()
    {
        double minutesPassed = (System.DateTime.Now - timeOfPlanting).TotalMinutes;
        double minutesTotal = (timeToRise - timeOfPlanting).TotalMinutes;

        progressGrow = (minutesPassed / minutesTotal) * 100;
        progressGrow = System.Math.Round(progressGrow);

        if (gameObject.CompareTag("Plant"))
            PlantProgress();
        else if (gameObject.CompareTag("Tree") || gameObject.CompareTag("Animal"))
            OtherProgress();
    }

    private void PlantProgress()
    {
        if (progressGrow >= 50)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = blueprint.mediumPrefabPosY;
            GameObject newPlant = Instantiate(blueprint.mediumPrefab, targetPosition, transform.rotation);
            newPlant.GetComponent<MediumProduct>().SetMainSettings(progressGrow, timeOfPlanting, timeToRise);
            newPlant.GetComponent<MediumProduct>().SetCollectableBlueprint(blueprint);
            newPlant.transform.SetParent(transform.parent);
            GetComponentInParent<PlowedField>().SetPlant(newPlant);

            Destroy(gameObject);
        }
    }

    private void OtherProgress()
    {
        if (progressGrow >= 100)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = blueprint.mediumPrefabPosY;
            GameObject newOther = Instantiate(blueprint.mediumPrefab, targetPosition, transform.rotation);
            newOther.GetComponent<FinalProduct>().SetCollectableBlueprint(blueprint);
            newOther.transform.SetParent(transform.parent);
            GetComponentInParent<GridSystem>().PutOnGrid((int)targetPosition.x, (int)targetPosition.z, blueprint.width, blueprint.height, newOther);

            Destroy(gameObject);
        }
    }
}
