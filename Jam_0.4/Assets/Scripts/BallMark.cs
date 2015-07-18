using UnityEngine;
using System.Collections;

public class BallMark : MonoBehaviour {

	public Transform Mark;
	private GroundMark _GM;

	void Start () 
	{
		_GM = Mark.GetComponent<GroundMark>();
	}
	
	void Update () 
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 100, ~((1<<8) | (1<<2) | (1<<13))))
		{
			if(!Mark.gameObject.activeSelf)
			{
				Mark.gameObject.SetActive(true);
			}
			Mark.position = hit.point;
			_GM._Center.localScale =  Vector3.up + Vector3.right + Vector3.forward * Vector3.Distance(transform.position, hit.point);
			_GM._Outter.forward = hit.normal;
			_GM._Inner.forward = hit.normal;
		}
		else
		{
			if(Mark.gameObject.activeSelf)
			{
				Mark.gameObject.SetActive(false);
			}
		}
	}

}
