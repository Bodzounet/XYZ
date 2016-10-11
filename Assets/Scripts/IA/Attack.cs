using UnityEngine;
using System.Collections;

namespace IA
{
    public class Attack : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private Vector3 _initialPosition;
        public float maxDistanceBeforeLosingAggro = 50f;
        public float averageAttackDistance = 10f;

        private Transform _target = null;
        public Transform Target
        {
            get { return _target; }
            private set
            {
                _target = value;
                if (_target == null)
                {
                    _MoveTo(_initialPosition);
                }
            }
        }

        private bool _isRoaming = false;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            _initialPosition = transform.position;
        }

        void Update()
        {
            if (Target != null)
            {
                if (Vector3.Distance(transform.position, _initialPosition) > maxDistanceBeforeLosingAggro)
                {
                    Target = null;
                }
                else
                {
                    if (!_IsInLineOfSight(Target))
                    {
                        _MoveTo(Target.position);
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, Target.position) < averageAttackDistance)
                        {
                            _Roam();
                        }
                        else //if (!_isRoaming || Vector3.Distance(transform.position, Target.position) > averageAttackDistance * 1.3f)
                        {
                            _MoveTo(Target.position);
                        }
                    }
                }
            }
        }

        void OnTriggerStay(Collider col)
        {
            if (col.tag != "Avatar") // only track the player
                return;

            if (Physics.Linecast(transform.position, col.transform.position, 1 << LayerMask.NameToLayer("Wall"))) // if the player is in another room, ignore it.
                return;

            if (Target == null)
            {
                Target = col.gameObject.transform;
            }
        }

        private bool _IsInLineOfSight(Transform target)
        {
            return !Physics.Linecast(transform.position, target.transform.position, 1 << LayerMask.NameToLayer("Wall"));
        }

        private void _MoveTo(Vector3 to)
        {
            _isRoaming = false;

            _navMeshAgent.updateRotation = true;
            _navMeshAgent.SetDestination(to);
        }

        Vector3 roamPos;
        private void _Roam()
        {
            if (!_isRoaming)
            {
                roamPos = transform.position + new Vector3(Random.Range(-5, 5f), 0, Random.Range(-5, 5f));
                _navMeshAgent.SetDestination(roamPos);
                _navMeshAgent.updateRotation = false;

                _isRoaming = true;
            }
            else if (!_navMeshAgent.pathPending && Vector3.Distance(roamPos, transform.position) < 0.2f)//(_navMeshAgent.remainingDistance < 0.2f))
            {
                _isRoaming = false;
            }
            transform.LookAt(Target);
        }
    }
}