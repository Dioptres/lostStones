using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private Rigidbody rbody;
	public float speed = 10;
	public float sprintSpeed;
	public float accel = 10;
	public float jumpForce = 5;

	[HideInInspector]
	public WarpGrenade _WarpG;

	public bool grounded;
	private bool sprint;

	private float _xMove;
	private float _zMove;
	public float _brakeVelocity = 5;
	public float _brakeRatio = 4;
	public float _airSpeed;




	// Use this for initialization
	void Start () 
	{
		rbody = GetComponent<Rigidbody>();
		_WarpG = GetComponent<WarpGrenade>();
		sprint = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		//Camera.main.transform.position = transform.position + transform.up * 0.75f;

	}

	void FixedUpdate()
	{
		grounded = Physics.Raycast(transform.position - transform.up*0.90f, Vector3.down, 0.3f);
		
		
		if(grounded)
		{
			sprint = false;
			
			if(Input.GetKey(KeyCode.LeftShift))
			{
				sprint = true;
			}
			
			Vector3 dirXZ = (Camera.main.transform.right * Input.GetAxisRaw("Horizontal") + Camera.main.transform.forward * Input.GetAxisRaw("Vertical")).normalized;
			
			if(sprint)
			{
				rbody.velocity = Vector3.Lerp(rbody.velocity, dirXZ * sprintSpeed, Time.deltaTime * accel );
				if(Input.GetKey(KeyCode.Space))
				{
					rbody.velocity = new Vector3(rbody.velocity.x, jumpForce * speed/sprintSpeed, rbody.velocity.z);
				}
				
			}
			else
			{
				rbody.velocity = Vector3.Lerp(rbody.velocity, dirXZ * speed, Time.deltaTime * accel );
				if(Input.GetKey(KeyCode.Space))
				{
					rbody.velocity = new Vector3(rbody.velocity.x, jumpForce, rbody.velocity.z);
				}
			}
			
		}
		else
		{
			AirMove();
			rbody.AddRelativeForce(new Vector3(_xMove*_airSpeed * Time.deltaTime, 0, _zMove*_airSpeed * Time.deltaTime), ForceMode.Acceleration);
		}
	}

	private void AirMove()
	{
		_xMove = 0.0f;
		_zMove = 0.0f;
		Vector3 locVel;
		locVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		
		
		if(Input.GetKey(KeyCode.Z))
		{
			if(locVel.z > speed)
				_zMove = 0;
			else
				_zMove += 1;
			if(locVel.z < -_brakeVelocity)
				_zMove = _zMove*_brakeRatio;
		}
		if(Input.GetKey(KeyCode.S))
		{
			if(locVel.z <-speed)
				_zMove =  0;
			else
				_zMove -= 1;
			if(locVel.z >_brakeVelocity)
				_zMove = _zMove*_brakeRatio;
		}
		if(Input.GetKey(KeyCode.Q))
		{
			if(locVel.x < -speed)
				_xMove =  0;
			else
				_xMove -= 1;
			if(locVel.x > _brakeVelocity)
				_xMove = _xMove*_brakeRatio;
		}
		if(Input.GetKey(KeyCode.D))
		{
			if(locVel.x > speed)
				_xMove =  0;
			else
				_xMove += 1;
			if(locVel.x < -_brakeVelocity)
				_xMove = _xMove*_brakeRatio;
		}
		
		if(_xMove >= 0.5f && _zMove >= 0.5f)
		{
			_xMove = _xMove * 0.7f;
			_zMove = _zMove * 0.7f;
		}
	}


}
