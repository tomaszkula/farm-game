using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    public static TaskManager instance;

    private Queue<Task> tasks = new Queue<Task>();
    private Task currentTask = null;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 TaskManager");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        if (currentTask == null)
        {
            if (tasks.Count > 0)
            {
                currentTask = tasks.Dequeue();
            }
        }
        else
        {
            if (currentTask.Moved) currentTask.DoTask();
            else currentTask.Move();

            if (currentTask.Finished) currentTask = null;
        }
    }

    public void AddTask(Task task)
    {
        tasks.Enqueue(task);
        Debug.Log("dodano do kolejki");
    }
}
