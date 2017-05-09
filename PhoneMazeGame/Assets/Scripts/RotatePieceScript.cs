using UnityEngine;
using System.Collections;

public class RotatePieceScript : MonoBehaviour {

	private float m_rotationAngle = -45f;
	public float m_Speed = 160f;
	private Vector3 m_RotationDirection = Vector3.forward;

	private Quaternion targetRotation;

	// Use this for initialization
	void Start ()
	{
		targetRotation = transform.rotation;
		//targetRotation *= Quaternion.Euler(0, 0, 45);
		targetRotation *= Quaternion.AngleAxis(m_rotationAngle, m_RotationDirection);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Rotate(Vector3.forward, 1);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_Speed * Time.deltaTime);

		if (Mathf.Approximately(transform.rotation.z, targetRotation.z))
			Destroy(this);

		//if (transform.rotation.Equals(targetRotation))
		//	Destroy(this);
	}
}
