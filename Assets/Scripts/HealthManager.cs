using UnityEngine;
using System.Collections;

public abstract class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int _maxLife;
    public int MaxLife
    {
        get { return _maxLife; }
    }

    protected int _life;
    public virtual int Life
    {
        get { return _life; }
        set
        {
            _life = Mathf.Clamp(value, 0, MaxLife);
            if (_life == 0)
            {
                _Die();
            }
        }
    }

    protected virtual void Start()
    {
        Life = MaxLife;
    }

    public abstract void TakeDamages(int damages);
    protected abstract void _Die();
}
