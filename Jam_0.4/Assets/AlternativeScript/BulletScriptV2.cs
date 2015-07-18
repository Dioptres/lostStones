using UnityEngine;
using System.Collections;

public class BulletScriptV2 : MonoBehaviour {

	public float damages = 5;
	public float lifetime = 5;
	private float life;
	private Rigidbody rbody;
	private TrailRenderer _TR;

	void OnEnable()
	{
		life = lifetime;
		_TR.time = 0.5f;
	}

	void Awake () 
	{
		rbody = GetComponent<Rigidbody>();
		_TR = GetComponent<TrailRenderer>();
	}
	
	void Update () 
	{
		life -= Time.deltaTime;
		if(life <= 0)
		{
			rbody.velocity = Vector3.zero;
			gameObject.SetActive(false);
			_TR.time = 0;
		}
	
	}

	void OnCollisionEnter(Collision collision)
	{
		rbody.velocity = Vector3.zero;
		gameObject.SetActive(false);
		_TR.time = 0;
	}
}
