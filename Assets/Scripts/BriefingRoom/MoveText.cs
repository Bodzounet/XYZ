using UnityEngine;
using System.Collections;

public class MoveText : MonoBehaviour
{
    public float speed = 1f;

	void Update ()
    {
        transform.localPosition += Vector3.up * speed * Time.deltaTime;
	}
}
