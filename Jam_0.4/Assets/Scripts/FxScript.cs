using UnityEngine;
using System.Collections;

public class FxScript : MonoBehaviour {

	public float duration = 3;
	private float timer;

	void OnEnable () 
	{
		timer = 0;
		for(int i = 0; i < transform.childCount ; i++)
		{
			transform.GetChild(i).GetComponent<ParticleSystem>().Play();
		}
	}
	
	void Update () 
	{
		timer += Time.deltaTime;
		if(timer >= duration)
		{
			Destroy(gameObject);
		}
	}
}
