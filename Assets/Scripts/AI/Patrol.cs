using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IA
{
    public class Patrol : MonoBehaviour
    {
        public float maxDistanceFromSpawningPoint;

        [SerializeField]
        private MovementData _navMeshDataWhenPatrolling;
        private MovementData _navMeshDataDefault;

        NavMeshAgent _navMeshAgent;
        List<Vector3> _patrolPoints = new List<Vector3>();

        private bool _patrollingState = false;
        public bool PatrollingState
        {
            get { return _patrollingState; }
            set
            {
                if (value)
                {
                    StartCoroutine(Co_Patrol());
                    _UpdateNavMeshAgentMovementData(_navMeshDataWhenPatrolling);
                }
                else
                {
                    StopAllCoroutines();
                    _UpdateNavMeshAgentMovementData(_navMeshDataDefault);
                }
                _patrollingState = value;
            }
        }

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            _navMeshDataDefault = new MovementData(_navMeshAgent.speed, _navMeshAgent.angularSpeed, _navMeshAgent.acceleration, _navMeshAgent.stoppingDistance, _navMeshAgent.autoBraking);

            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                RaycastHit hit;
                Vector3 direction = new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1));

                Physics.Raycast(transform.position, direction, out hit, maxDistanceFromSpawningPoint, 1 << LayerMask.NameToLayer("Wall"));


                float maxDistance = hit.collider == null ? maxDistanceFromSpawningPoint : Vector3.Distance(transform.position, hit.point);

                Debug.DrawLine(transform.position, transform.position + direction * maxDistance, Color.red, 20);

                _patrolPoints.Add(transform.position + direction * maxDistance * Random.Range(0.2f, 0.8f));
            }
            _patrolPoints.Add(transform.position);

            PatrollingState = true;
        }

        private IEnumerator Co_Patrol()
        {
            _GoToNextPoint();
            while (true)
            {
                if (_navMeshAgent.remainingDistance < 0.5f)
                {
                    _GoToNextPoint();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        int idx = 0;
        private void _GoToNextPoint()
        {
            _navMeshAgent.SetDestination(_patrolPoints[idx]);
            idx = (idx + 1) % _patrolPoints.Count;
        }

        private void _UpdateNavMeshAgentMovementData(MovementData md)
        {
            _navMeshAgent.speed = md.speed;
            _navMeshAgent.angularSpeed = md.angularSpeed;
            _navMeshAgent.acceleration = md.acceleration;
            _navMeshAgent.stoppingDistance = md.stoppingDistance;
            _navMeshAgent.autoBraking = md.autoBraking;
        }

        [System.Serializable]
        private struct MovementData
        {
            public float speed;
            public float angularSpeed;
            public float acceleration;
            public float stoppingDistance;
            public bool autoBraking;

            public MovementData(float speed_, float angularSpeed_, float acceleration_, float stoppingDistance_, bool autoBraking_)
            {
                speed = speed_;
                angularSpeed = angularSpeed_;
                acceleration = acceleration_;
                stoppingDistance = stoppingDistance_;
                autoBraking = autoBraking_;
            }
        }
    }
}