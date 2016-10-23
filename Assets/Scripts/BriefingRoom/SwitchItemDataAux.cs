using UnityEngine;
using System.Collections;

public class SwitchItemDataAux : MonoBehaviour
{
    private Vector3 _destination;

    void Start()
    {
        _destination = transform.localPosition;
    }

    public void GoTo(Vector3 destination)
    {
        StopAllCoroutines();
        _destination = destination;
        transform.localPosition = destination;
    }

    public void MoveTo(Vector3 destination)
    {
        StopAllCoroutines();
        transform.localPosition = _destination;

        _destination = destination;
        StartCoroutine(Co_MoveTo());
    }

    private IEnumerator Co_MoveTo()
    {
        while (transform.localPosition != _destination)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _destination, 0.1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
