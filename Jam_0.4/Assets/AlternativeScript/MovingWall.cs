using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {

	private Vector3 _left;
	private Vector3 _right;
	private float _t;
	private bool sens;


	// Use this for initialization
	void Start () 
	{
		_t = 0;
		sens = true;
		_left = transform.position + transform.right * -20;
		_right = transform.position + transform.right * 20;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sens)
		{
			_t += Time.deltaTime;
		}
		else
		{
			_t -= Time.deltaTime;
		}
		if(_t > 1 || _t <= 0)
		{
			sens = !sens;
		}
		transform.position = Vector3.Lerp(_left, _right, _t);

	}
}
