using UnityEngine;
using System.Collections;

public class WarpGrenade : MonoBehaviour {

	public Vector3 _beginPos;
	public Vector3 Decalage;
	public Vector3 _startVelocity;
	public float _timestep;
	public float _maxTime;
	public float cooldown = 5;
	private float CDtimer;
	public bool ballOUT;

	public Material Yep;
	public Material Nope;
	private bool available;
	//private Renderer GreRender;
	private Renderer VisRender;
	
	private Controller _CT;
	private CapsuleCollider _collider;
	private Rigidbody rbody; 
	private Vector3 storedVel;


	public float throwForce = 10;
	public float upThrow = 5;

	private LineRenderer _LR;
	public GameObject Grenade;
	public GameObject Visual;

	public bool TPing;
	public float SpeedTP = 50;
	private Vector3 startPos;
	private Vector3 targetPos;
	private float _t;
	private float _m;

	public bool _trueForTime = false;
	public float TPtime;


	// Use this for initialization
	void Start () 
	{
		_LR = GetComponent<LineRenderer>();
		_CT = GetComponent<Controller>();
		_collider = GetComponent<CapsuleCollider>();
		rbody = GetComponent<Rigidbody>();
		_LR.enabled = false;
		//GreRender = Grenade.GetComponent<Renderer>();
		VisRender = Visual.GetComponent<Renderer>();
		Grenade.SetActive(false);
		CDtimer = 0;
		ballOUT = false;
		TPing = false;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(CDtimer > 0)
		{
			CDtimer -= Time.deltaTime;
		}

		if(Input.GetMouseButton(1))
		{
			_beginPos = transform.position + Camera.main.transform.forward * Decalage.z + Camera.main.transform.right * Decalage.x+Camera.main.transform.up* Decalage.y;
			_startVelocity = Camera.main.transform.forward * throwForce + Vector3.up * upThrow;
			PlotTrajectory (_beginPos,_startVelocity, _timestep, _maxTime);
			_LR.enabled = true;
		}
		if(Input.GetMouseButtonUp(1) && CDtimer <= 0)
		{
			_LR.enabled = false;
			Grenade.SetActive(true);
			CDtimer = cooldown;
			Grenade.transform.position = _beginPos;
			Grenade.transform.rotation = Quaternion.identity;
			Grenade.GetComponent<Rigidbody>().useGravity = true;
			Grenade.GetComponent<Rigidbody>().velocity = _startVelocity;
			ballOUT = true;
		}


		if(ballOUT)
		{
			available = !Physics.Raycast(Grenade.transform.position, Vector3.up, 2);
			if(available)
			{
				//GreRender.material = Yep;
				VisRender.material = Yep;
			}
			else
			{
				//GreRender.material = Nope;
				VisRender.material = Nope;
			}

			if(Input.GetKeyDown(KeyCode.E))
			{
				if(available)
				{
					TPing = true;
					Grenade.GetComponent<Rigidbody>().useGravity = false;
					Grenade.GetComponent<Rigidbody>().velocity = Vector3.zero;
					startPos = transform.position;
					targetPos = Grenade.transform.position + Vector3.up;
					_m = (transform.position - targetPos).magnitude;
					_t = 0;
					_CT.enabled = false;
					_collider.enabled = false;
					rbody.useGravity = false;
					storedVel = rbody.velocity;
					rbody.velocity = Vector3.zero;
				}
			}
		}

		if(TPing)
		{
			if(_t < 1)
			{
				if(_trueForTime)
				{
					if(TPtime == 0)
					{
						_t = 1;
					}
					else
					{
						_t += Time.deltaTime / TPtime;
					}
				}
				else
				{
					_t += Time.deltaTime * SpeedTP / _m;
					
					if(_t > 1)
					{
						_t = 1;
					}

				}
				transform.position = Vector3.Lerp(startPos, targetPos, _t);
			}
			else
			{
				TPing = false;
				Grenade.SetActive(false);
				ballOUT = false;
				_CT.enabled = true;
				_collider.enabled = true;
				rbody.useGravity = true;
				rbody.velocity = storedVel;
			}

		}

	}

	public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) 
	{
		return start + startVelocity*time + Physics.gravity*time*time*0.5f;
	}

	public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) 
	{
		_LR.SetVertexCount(1);
		Vector3 prev = start;
		_LR.SetPosition(0, start);

		for (int i=1;;i++) 
		{
			float t = timestep*i;
			if (t > maxTime) 
			{
				break;
			}
			Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);
			if (Physics.Linecast (prev,pos,  ~((1<<8) | (1<<2) | (1<<13)))) 
			{
				break;
			}
			_LR.SetVertexCount(i);
			_LR.SetPosition(i-1, pos);
			Debug.DrawLine (prev,pos,Color.red);
			prev = pos;
		}
	}

}
