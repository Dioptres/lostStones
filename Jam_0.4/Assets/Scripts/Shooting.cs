using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject _prefabBullet;
	public float cadence = 0.2f;
	private float sTimer;
	public Transform cannonHead;
	public Transform PAPA;
	public float bulletSpeed;

	void Start () 
	{

	}
	
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			if(sTimer <= 0)
			{
				sTimer = cadence;
				Shoot();
			}
		}
		if(sTimer > 0)
		{
			sTimer -= Time.deltaTime;
		}
 	}

	void Shoot()
	{
		RaycastHit hit;
		GameObject target = new GameObject();

		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 200))
		{
			target.transform.position = hit.point;
			target.transform.parent = hit.transform;
		}
		else
		{
			target.transform.position = Camera.main.transform.position + Camera.main.transform.forward*200;
			target.transform.parent = PAPA;
		}
		GameObject bullet = Instantiate(_prefabBullet, cannonHead.position, Quaternion.identity) as GameObject;
		bullet.transform.parent = PAPA;
		bullet.GetComponent<BulletScript>().target = target;
		bullet.GetComponent<BulletScript>().start = cannonHead.position;
		bullet.GetComponent<BulletScript>().speed = bulletSpeed;

	}





}
