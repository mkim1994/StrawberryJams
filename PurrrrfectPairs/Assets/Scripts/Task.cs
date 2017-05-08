using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task {

	public enum TaskStatus : byte {
		Detached, // Task has not been attached to a TaskManager
		Pending, // Task has not been initialized
		Working, // Task has been initialized
		Success, // Task completed successfully
		Fail, // Task completed unsuccessfully
		Aborted // Task was aborted
	}

	// The only member variable that a base task has is its status
	public TaskStatus Status { get; private set; }

	//convenience status checking

	public bool IsDetached { get { return Status == TaskStatus.Detached; } }
	public bool IsAttached { get { return Status != TaskStatus.Detached; } }
	public bool IsPending { get { return Status == TaskStatus.Pending; } }
	public bool IsWorking { get { return Status == TaskStatus.Working; } }
	public bool IsSuccessful { get { return Status == TaskStatus.Success; } }
	public bool IsFailed { get { return Status == TaskStatus.Fail; } }
	public bool IsAborted { get { return Status == TaskStatus.Aborted; } }
	public bool IsFinished { get { return (Status == TaskStatus.Fail ||
		Status == TaskStatus.Success || Status == TaskStatus.Aborted); } }

	// convenience method for external classes to abort the task
	public void Abort(){
		SetStatus (TaskStatus.Aborted);
	}

	/* A method for changing the status of the task
	 * It's marked internal so that the manager can access it
	 * assuming tasks and their manager are in the same assembly
	 *  */
	internal void SetStatus(TaskStatus newStatus){
		if (Status == newStatus)
			return;

		Status = newStatus;

		switch (newStatus) {
		case TaskStatus.Working:
			/* initialize the task when the Task first starts
			 * it's important to separate the initialization from the constructor, 
			 * since the tasks may not start running till aftter they've been constructed
			 * */
			Init ();
			break;

			/* Success/Aborted/Failed are the completed states of a task.
		 * Subclasses are notified when entering one of these states
		 * and are given the opportunity to do any clean up
		 * */
		case TaskStatus.Success:
			OnSuccess ();
			CleanUp ();
			break;

		case TaskStatus.Aborted:
			OnAbort ();
			CleanUp ();
			break;

		case TaskStatus.Fail:
			OnFail ();
			CleanUp ();
			break;

			/* these are "internal states that are mostly relevant for the task manager */
		case TaskStatus.Detached:
			break;
		case TaskStatus.Pending:
			break;
		default:
			throw new ArgumentOutOfRangeException(newStatus.ToString(), newStatus, null);


		}

	}

	protected virtual void OnAbort(){

	}

	protected virtual void OnSuccess(){

	}

	protected virtual void OnFail(){

	}

	/* LIFECYCLE
	 * Init: This is when the task starts and gets ready to do its work. It's important to distinguish this
	 * 	from the task's instructor. Tasks might start long after they're instanced, so we need to provide
	 * 	a way for them to start their work in a context that isn't stale (i.e. you don't want tasks to be
	 *  doing work based on assumptions that were made when it was instanced but may now no longer be valid
	 * 
	 * Update: This represents one complete "iteration" of the work the task needs to do. For example, if
	 * 	you have a task where a character walks to a location, its Update would tak a step and check to see
	 *  if the character arrived.
	 * 
	 * CleanUp: When a task completes it might need to free up resources or let somebody know.
	 * */

	//Override to handle initialization of the task; called when the task enters the Working state
	protected virtual void Init (){

	}

	// called whenever the TaskManager updates. your tasks' work generally goes here
	internal virtual void Update(){

	}

	// This is called when the tasks complete (i.e. is aborted, fails, or succeeds.
	// called after the status change handlers are called
	protected virtual void CleanUp(){

	}


	//Assign a task to be run if this task runs successfully
	public Task NextTask { get; private set; }

	// sets a task to be automatically attached when this one completes successfully
	// NOTE: if a task is aborted or fails, its next task will not be queued
	// NOTE: DO NOT assign attached tasks with this method
	public Task Then(Task task){
		Debug.Assert (!task.IsAttached);
		NextTask = task;
		return task;
	}
}
