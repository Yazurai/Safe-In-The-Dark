using UnityEngine;
using System.Collections;

public class UIRotation : MonoBehaviour 
{
	public float xRotation;
	public float yRotation;
	public float zRotation;

	void Update () 
	{
		gameObject.transform.Rotate (new Vector3 (xRotation * Time.deltaTime, yRotation* Time.deltaTime, zRotation* Time.deltaTime));
	}
}
