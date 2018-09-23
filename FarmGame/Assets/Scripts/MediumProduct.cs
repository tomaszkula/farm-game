using UnityEngine;

public class MediumProduct : Product {

    private void Update()
    {
        double minutesPassed = (System.DateTime.Now - timeOfPlanting).TotalMinutes;
        double minutesTotal = (timeToRise - timeOfPlanting).TotalMinutes;

        progressGrow = (minutesPassed / minutesTotal) * 100;
        progressGrow = System.Math.Round(progressGrow);

        if (progressGrow >= 100)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = blueprint.finalPrefabPosY;
            GameObject newPlant = Instantiate(blueprint.finalPrefab, targetPosition, transform.rotation);
            newPlant.GetComponent<FinalProduct>().SetCollectableBlueprint(blueprint);
            newPlant.transform.SetParent(transform.parent);
            GetComponentInParent<PlowedField>().SetPlant(newPlant);

            Destroy(gameObject);
        }
    }
}
