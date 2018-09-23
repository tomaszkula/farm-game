using UnityEngine;

public abstract class Task {

    protected GameObject go;
    protected Blueprint blueprint;

    private GameObject player;
    private Vector3 nearestCorner = Vector3.down;

    private float posY = 1.7f;
    private float maxDistanceToRotate = 0.5f;
    private float speed = 5f;

    public bool Moved { get; set; }
    public bool Finished { get; set; }

    public virtual void DoTask() { }

    public Task(GameObject go, Blueprint blueprint)
    {
        this.go = go;
        this.blueprint = blueprint;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private Vector3 FindTargetPosition()
    {
        int width = blueprint.width;
        int height = blueprint.height;
        Vector3 posGo = go.transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3[] corners = new Vector3[4]; 
        corners[0] = new Vector3(posGo.x - (width - 1) / 2.0f, posGo.y, posGo.z + (height - 1) / 2.0f);
        corners[1] = new Vector3(posGo.x - (width - 1) / 2.0f, posGo.y, posGo.z - (height - 1) / 2.0f);
        corners[2] = new Vector3(posGo.x + (width - 1) / 2.0f, posGo.y, posGo.z + (height - 1) / 2.0f);
        corners[3] = new Vector3(posGo.x + (width - 1) / 2.0f, posGo.y, posGo.z - (height - 1) / 2.0f);

        float minDistance = float.MaxValue;
        Vector3 nearestCorner = Vector3.down;
        for (int i = 0; i < 4; i++)
        {
            if(Vector3.Distance(playerPosition, corners[i]) < minDistance)
            {
                minDistance = Vector3.Distance(playerPosition, corners[i]);
                nearestCorner = corners[i];
            }
        }

        return nearestCorner;
    }

    public void Move()
    {
        if (nearestCorner == Vector3.down)
            nearestCorner = FindTargetPosition();

        Vector3 horizontalTargetPosition = new Vector3(nearestCorner.x, posY, player.transform.position.z);
        Vector3 verticaltargetPosition = new Vector3(player.transform.position.x, posY, nearestCorner.z);

        float distance = Vector3.Distance(player.transform.position, horizontalTargetPosition);

        if (distance > maxDistanceToRotate)
            player.transform.LookAt(horizontalTargetPosition);

        player.transform.position = Vector3.MoveTowards(player.transform.position, horizontalTargetPosition, speed * Time.deltaTime);

        if (player.transform.position == horizontalTargetPosition)
        {
            distance = Vector3.Distance(player.transform.position, verticaltargetPosition);

            if (distance > maxDistanceToRotate)
                player.transform.LookAt(verticaltargetPosition);

            player.transform.position = Vector3.MoveTowards(player.transform.position, verticaltargetPosition, speed * Time.deltaTime);
        }

        if (player.transform.position == verticaltargetPosition)
        {
            Moved = true;
        }
    }
}
