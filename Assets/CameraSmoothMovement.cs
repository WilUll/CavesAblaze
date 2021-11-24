using UnityEngine;

public class CameraSmoothMovement : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed;
	public Vector3 offset;

	void FixedUpdate()
	{
		Vector3 targetPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
		
		transform.position = smoothedPosition;
		transform.rotation = Quaternion.identity;
		

		transform.LookAt(target);
	}
}
