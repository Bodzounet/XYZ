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
            if (before > Life)
                _Regen();

            if (OnLifeChanged != null)
                OnLifeChanged(before, Life);
        }
    }

    public override int Armor
    {
        get { return base.Armor; }
        set
        {
            int before = Armor;
            base.Armor = value;
            if (before > Armor)
                _Regen();

            if (OnArmorChanged != null)
                OnArmorChanged(before, Armor);
        }
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
