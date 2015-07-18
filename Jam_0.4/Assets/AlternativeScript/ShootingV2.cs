using UnityEngine;
using System.Collections;

public class ShootingV2 : MonoBehaviour {

	public GameObject _prefabBullet;
	public int _preloaded;
	public GameObject[] bullets;
	public float cadence = 0.2f;
	private float sTimer;
	public Transform cannonHead;
	public Transform PAPA;
	public float bulletSpeed = 30;

	// Use this for initialization
	void Start () 
	{
		bullets = new GameObject[_preloaded];

		for(int i = 0; i < _preloaded; i++)
		{
			bullets[i] = Instantiate(_prefabBullet, Vector3.down * 500, Quaternion.identity) as GameObject;
			bullets[i].SetActive(false);
			bullets[i].transform.parent = PAPA;
		}
		sTimer = 0;
	}
	
	// Update is called once per frame
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
		for(int i = 0; i < bullets.Length; i++)
		{
			if(bullets[i].activeSelf == false)
			{
				bullets[i].SetActive(true);
				bullets[i].transform.position = cannonHead.position;
				bullets[i].transform.rotation = cannonHead.rotation;
				bullets[i].GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * bulletSpeed;
				break;
			}
			   
		}
	}





}
