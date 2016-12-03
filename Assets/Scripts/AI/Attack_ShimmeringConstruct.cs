using UnityEngine;
using System.Collections;

namespace AI
{
    /// <summary>
    /// State machine
    /// 3 states
    ///     A : move close to target
    ///     B : walk around the target
    ///     C : is close enough of the target
    ///     
    /// A -> C -> B
    /// B -> A
    /// </summary>
    public class Attack_ShimmeringConstruct : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private MasterAI _masterAI;

        private Vector3 _initialPosition;
        private bool _isRoaming;
        private bool _canRoam;
        public bool CanRoam
        {
            get { return _canRoam; }
            set
            {
                _canRoam = value;
                _SetWalkingMode();
            }
        }

        private Transform _target;
        public Transform Target
        {
            get { return _target; }
            private set
            {
                _target = value;
            }
        }

        private Transform _agent;

        void Awake()
        {
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            _agent = _navMeshAgent.transform;

            _masterAI = GetComponent<MasterAI>();

            GetComponentInChildren<TakeAggro>().OnTakeAggro += _OnTakeAggro;
            GetComponentInChildren<LoseAggro>().OnLoseAggro += _OnLoseAggro;
        }

        void Start()
        {
            _initialPosition = transform.position;
            _navMeshAgent.updateRotation = false;
        }

        void Update()
        {
            if (Target != null)
            {
                if (!_IsInLineOfSight(Target)) // state A
                {
                    _navMeshAgent.SetDestination(Target.position - (Target.position - _agent.position).normalized * 3.5f);
                    CanRoam = false;
                }

                if (CanRoam) // state B
                {
                    _SetRoamingMode();

                    _agent.rotation = Quaternion.Slerp(_agent.rotation, Quaternion.LookRotation(Target.position - _agent.position), 0.075f);
                    if (!_isRoaming)
                    {
                        Vector3 newPos;
                        do
                        {
                            newPos = _agent.position + new Vector3(Random.Range(-5, 5f), 0, Random.Range(-5, 5f));
                            while (Physics.Linecast(newPos, Target.position, 1 << LayerMask.NameToLayer("Wall")))
                                newPos = _agent.position + new Vector3(Random.Range(-5, 5f), 0, Random.Range(-5, 5f));
                        } while (!_navMeshAgent.SetDestination(newPos));
                        _isRoaming = true;
                    }

                    if (_navMeshAgent.pathPending)
                        return;

                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                        _isRoaming = false;
                }
                else // state C
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                        CanRoam = true;
                }
            }
        }

        private void _SetRoamingMode()
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.stoppingDistance = 0.2f;

            _masterAI.Anim.SetBool("Attack", true);
        }

        private void _SetWalkingMode()
        {
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.stoppingDistance = 1f;
            _isRoaming = false;

            _masterAI.Anim.SetBool("Attack", false);
        }

        private bool _IsInLineOfSight(Transform target)
        {
            return !Physics.Linecast(_agent.position, target.position, 1 << LayerMask.NameToLayer("Wall"));
        }

        private void _OnTakeAggro(Transform target)
        {
            if (Target == null)
            {
                Target = target;
            }
        }

        private void _OnLoseAggro(Transform target)
        {
            StartCoroutine(CO_WaitForLastRoaming());
        }

        private IEnumerator CO_WaitForLastRoaming()
        {
            while (_isRoaming)
            {
                yield return new WaitForEndOfFrame();
            }
            Target = null;
            CanRoam = false;
            _navMeshAgent.SetDestination(_initialPosition);
        }
    }
}