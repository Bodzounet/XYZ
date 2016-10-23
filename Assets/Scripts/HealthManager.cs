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

    [SerializeField]
    private int _maxArmor;
    public int MaxArmor
    {
        get { return _maxArmor; }
    }

    protected int _armor; // damages are done to the armor first. Armor reduces every damage taken by 1.
    public virtual int Armor
    {
        get { return _armor; }
        set
        {
            // armor reduces damage taken by one.
            if (_armor > 0 && _armor > value)
            {
                value++;
            }

            _armor = Mathf.Clamp(value, 0, MaxArmor);

            if (value < 0)
            {
                Life += value;
            }
        }
    }

    [SerializeField]
    private float _armorRegenerationRate = 5; // delay before armor start regenerating after taking damage
    public float ArmorRegenerationRate
    {
        get { return _armorRegenerationRate; }
        set
        {
            _armorRegenerationRate = value;
        }
    }

    [SerializeField]
    private int _armorRegeneration = 1; // base regenration for the first tick, when armor is regenerating
    public int ArmorRegeneration
    {
        get { return _armorRegeneration; }
        set
        {
            _armorRegeneration = value;
        }
    }

    void Start()
    {
        Life = MaxLife;
        Armor = MaxArmor;
    }

    protected abstract void _Die();
}
