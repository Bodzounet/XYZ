using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float sensitivityY;

    public float maxAngle = 55;

    private float xAngle = 0;
    
	void Start ()
    {
	
	}
	
	void Update ()
    {
	    _Look();
	}

    private void _Look()
    {
        xAngle += Input.GetAxis("Mouse Y") * sensitivityY;
        if (xAngle > 180)
            xAngle -= 360;
        xAngle = Mathf.Clamp(xAngle, -maxAngle, maxAngle);

        transform.localEulerAngles = new Vector3(-xAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
