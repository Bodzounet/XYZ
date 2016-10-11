using UnityEngine;
using System.Collections;

public class GoToTarget : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform target;

    public Transform[] patrolPoints;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _GoToNextPoint();
    }

    void Update()
    {
        //_navMeshAgent.SetDestination(target.position);
        if (_navMeshAgent.remainingDistance < 0.5f)
        {
            Debug.Log("cc");
            _GoToNextPoint();
        }
    }

    int idx = 0;
    private void _GoToNextPoint()
    {
        _navMeshAgent.SetDestination(patrolPoints[idx++ % patrolPoints.Length].position);

        Debug.Log(_navMeshAgent.remainingDistance);

        while (_navMeshAgent.remainingDistance > 20f)
        {
            _navMeshAgent.SetDestination(patrolPoints[idx++ % patrolPoints.Length].position);
        }
        //while (_navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
        //{
        //    Debug.Log("caca");
        //    idx++;
        //}
    }
}
