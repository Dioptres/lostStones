using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public GameObject target;
	public Vector3 start;
	public float speed;
	public int damages = 5;
	private float m;
	private float t;
	private TrailRenderer _TR;
	public bool apply;
	private Vector3 FXpos;

	void Start () 
	{
		m = (transform.position - target.transform.position).magnitude;
		_TR = GetComponent<TrailRenderer>();
		t = 0;
		apply = false;
	}
	
	void Update () 
	{
		if(t < 1)
		{
			t += Time.deltaTime * speed / m;
			if(target != null)
			{
				transform.position = Vector3.Lerp(start, target.transform.position, t);
			}
		}
		else // Moment du contact
		{
			if(!apply)
			{
				apply = true;
				FXpos = start + (target.transform.position - start)*0.99f;
				Instantiate(FXmanager.FXimpact, FXpos, Quaternion.identity);
				if(target.transform.parent.tag == "Enemy")
				{
					target.transform.parent.GetComponent<EnemyHealth>().ReceiveDamage(damages);
				}
			}
			Destroy(target);
			Destroy(gameObject, _TR.time);
		}
	
	}
}
