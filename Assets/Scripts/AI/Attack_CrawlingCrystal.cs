using UnityEngine;
using System.Collections;

namespace AI
{
    /// <summary>

    /// </summary>
    public class Attack_CrawlingCrystal : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private MasterAI _masterAI;

        private Vector3 _initialPosition;

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

        private bool _isCharging;

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
        }

        void Update()
        {
            if (_isCharging)
            {
                if (Physics.Raycast(_agent.position, _agent.forward, _navMeshAgent.radius * 1.2f, 1 << LayerMask.NameToLayer("Wall")) ||
                    Physics.Raycast(_agent.position, (_agent.forward * 2 + _agent.right).normalized, _navMeshAgent.radius * 1.2f, 1 << LayerMask.NameToLayer("Wall")) ||
                    Physics.Raycast(_agent.position, (_agent.forward * 2 - _agent.right).normalized, _navMeshAgent.radius * 1.2f, 1 << LayerMask.NameToLayer("Wall")))
                {
                    _masterAI.Die();
                }
            }
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
                StartCoroutine(CO_SuicideAttack());
                _masterAI.Anim.SetBool("PrepareToCharge", true);
            }
        }

        private void _OnLoseAggro(Transform target)
        {
            
        }

        private IEnumerator CO_SuicideAttack()
        {
            while (Mathf.Abs(Quaternion.Angle(Quaternion.LookRotation(Target.position - _agent.position), _agent.rotation)) > 3f)
            {
                _agent.rotation = Quaternion.Slerp(_agent.rotation, Quaternion.LookRotation(Target.position - _agent.position), 0.055f);
                yield return new WaitForEndOfFrame();
            }

            _masterAI.Anim.SetBool("ReadyToCharge", true);

        }

        private void CB_Charge(Transform hit)
        {
            Debug.Log("prout");
        }

        public void CanCharge()
        {
            _isCharging = true;
            _navMeshAgent.velocity = _agent.forward * 25;
        }
    }
}