using UnityEngine;
using System.Collections;

public class CanvasOn : MonoBehaviour {

	public GameObject _canvas;

	void Start () 
	{
		_canvas.SetActive(true);
	}

}
