using UnityEngine;
using System.Collections;

public class RotationCorps : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

	}
}
