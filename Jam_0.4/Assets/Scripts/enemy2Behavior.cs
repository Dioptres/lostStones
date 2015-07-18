using UnityEngine;
using System.Collections;

public class enemy2Behavior : MonoBehaviour {

	GameObject target;
	public float fieldOfViewRange;

	public float timeBeforeShoot;
	public float timeBetweenShoot;

	private bool isSeeingPlayer;

	float time;
	float time2;

	private bool CanSeePlayer()
	{
		
		RaycastHit hit;
		Vector3 rayDirection = target.transform.position - transform.position;
		
		if((Vector3.Angle(rayDirection, transform.forward)) < fieldOfViewRange)
		{
			Debug.DrawLine (transform.position,target.transform.position,Color.cyan);
			if (Physics.Raycast (transform.position, rayDirection, out hit)) 
			{
				if (hit.transform.tag == "Player") 
				{
					return true;
				}
				else
				{

					return false;
				}
			}
		}
		return false;
	}

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		fieldOfViewRange = fieldOfViewRange * 0.5f;
		time2 = timeBetweenShoot + 1;
	}

	// Update is called once per frame
	void Update () 
	{
		if (CanSeePlayer ()) 
		{
			time += Time.deltaTime;
		}
		else
		{
			time = 0;
		}

		if(time>timeBeforeShoot)
		{
			time2 += Time.deltaTime;
			if(time2>timeBetweenShoot)
			{
				time2 = 0;
				Debug.Log("Shoot");
			}
		}
	}
}
