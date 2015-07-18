using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform _player;

	void LateUpdate () 
	{
		transform.position = _player.position + _player.up * 0.75f;
	}
}
