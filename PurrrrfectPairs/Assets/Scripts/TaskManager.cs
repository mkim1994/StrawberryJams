using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager { // maybe from extend from MonoBehaviour

	private readonly List<Task> _tasks = new List<Task>();


	// Use this for initialization
	void Start () {

	}

	// manage lifecycle of the tasks and removing them when completed
	public void Update () {
		//iterate through all the tasks
		for (int i = _tasks.Count - 1; i >= 0; --i) {
			Task task = _tasks [i];
			//initialize any tasks that have just been added
			if (task.IsPending) {
				task.SetStatus (Task.TaskStatus.Working);
			}
			//a task could finish during initialization
			// (e.g. the task has to abort b/c the conditions for executing no longer exist)
			// so you need to check before the update
			if (task.IsFinished) {
				HandleCompletion (task, i);
			} else {
				//update the task and clear it if it's done
				task.Update();
				if (task.IsFinished) {
					HandleCompletion (task, i);
				}
			}
		}
	}

	//tasks can only be added and you have to abort them
	//to remove them before they complete on their own
	public void AddTask(Task task){
		Debug.Assert (task != null);
		//NOTE: only add tasks that aren't already attached.
		//Don't want multiple task managers updating the same task
		Debug.Assert(!task.IsAttached);
		_tasks.Add (task);
		task.SetStatus (Task.TaskStatus.Pending);
	}

	public void HandleCompletion(Task task, int taskIndex){
		//if the finished task has a "next" task, queue it up - but only if the original task was successful
		if (task.NextTask != null && task.IsSuccessful) {
			AddTask (task.NextTask);
		}

		//clear the task from the manager and let it know its no longer being managed
		_tasks.RemoveAt(taskIndex);
		task.SetStatus (Task.TaskStatus.Detached);
	}
}
