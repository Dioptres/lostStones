using UnityEngine;
using System.Collections;

public class GroundMark : MonoBehaviour {

	public Transform _Outter;
	public Transform _Inner;
	public Transform _Center;

	public float rotSpeed = 90;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		_Outter.Rotate(transform.forward * rotSpeed * Time.deltaTime);
		_Inner.Rotate(transform.forward * -rotSpeed * Time.deltaTime);
	}
}
