using UnityEngine;
using System.Collections;
using System;

public class AvatarHealthManager : HealthManager
{
    public delegate void StatChanged(int before, int current);
    public event StatChanged OnLifeChanged;
    public event StatChanged OnArmorChanged;

    private Coroutine _regen = null;

    public override int Life
    {
        get { return base.Life; }
        set
        {
            int before = Life;
            base.Life = value;
        
            if (OnLifeChanged != null)
                OnLifeChanged(before, Life);
        }
    }

    [SerializeField]
    private int _maxArmor;
    public int MaxArmor
    {
        get { return _maxArmor; }
    }

    protected int _armor; // damages are done to the armor first. Armor reduces every damage taken by 1.
    public int Armor
    {
        get { return _armor; }
        set
        {
            int before = _armor;

            _armor = Mathf.Clamp(value, 0, MaxArmor);

            if (OnArmorChanged != null)
                OnArmorChanged(before, Armor);
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

    protected override void Start()
    {
        base.Start();
        Armor = MaxArmor;
    }

    public override void TakeDamages(int damages)
    {
        if (Armor > 0) // armor reduces damages taken by 1
            damages--;

        int ArmorAbsorption = Mathf.Min(damages, Armor);
        Armor -= ArmorAbsorption;
        Life -= (damages - ArmorAbsorption);

        _Regen();
    }

    protected override void _Die()
    {

    }

    private void _Regen()
    {
        if (_regen != null)
        {
            StopCoroutine(_regen);
        }
        _regen = StartCoroutine(Co_RegenArmor());
    }

    private IEnumerator Co_RegenArmor()
    {
        int regen = ArmorRegeneration;
        float delay = ArmorRegenerationRate;

        while (Armor < MaxArmor)
        {
            yield return new WaitForSeconds(delay);
            Armor += regen;

            regen++;
            regen = Mathf.Clamp(regen, 0, 5);

            delay -= ArmorRegenerationRate * 0.1f;
            delay = Mathf.Clamp(delay, delay * 0.5f, delay * 1.5f);
        }
    }
}
