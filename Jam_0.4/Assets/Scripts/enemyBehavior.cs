using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour 
{
	public Transform[] target;
	public float speed;

	Quaternion lookingAt;

	int index;

	void Start()
	{
		index = 0;
		transform.LookAt(target[index].position);
	}

	void Update() 
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target[index].position, step);
		if (Vector3.Distance(target[index].position, transform.position)<1)
		{
			index = (index + 1)%target.Length;
		}

		lookingAt = Quaternion.LookRotation(target[index].position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookingAt, Time.deltaTime);
	}
}
