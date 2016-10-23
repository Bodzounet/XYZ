using UnityEngine;
using System.Collections;

public class UI_Bar : MonoBehaviour
{
    private enum e_type
    {
        Life,
        Armor
    }

    public RectTransform frontBar;
    public RectTransform backBar;

    private float _barLenght;

    private Vector3 _min;
    private Vector3 _max;
    private int factor = 0;

    [SerializeField]
    private e_type _managedBar;

    void Start()
    {
        _barLenght = frontBar.rect.width;

        _max = frontBar.localPosition;
        _min = _max - Vector3.right * _barLenght;

        var healthManager = FindObjectOfType<AvatarHealthManager>();
        if (_managedBar == e_type.Armor)
        {
            healthManager.OnArmorChanged += OnBarValueChanged;
            factor = healthManager.MaxArmor;

            frontBar.localPosition = Vector3.Lerp(_min, _max, healthManager.Armor / (float)factor);
            backBar.localPosition = Vector3.Lerp(_min, _max, healthManager.Armor / (float)factor);
        }
        else
        {
            healthManager.OnLifeChanged += OnBarValueChanged;
            factor = healthManager.MaxLife;

            frontBar.localPosition = Vector3.Lerp(_min, _max, healthManager.Life / (float)factor);
            backBar.localPosition = Vector3.Lerp(_min, _max, healthManager.Life / (float)factor);
        }
    }

    public void OnBarValueChanged(int previous, int current)
    {
        float delta = (current - previous) * _barLenght / 100;
        if (delta == 0)
            return;

        StopAllCoroutines();

        if (delta < 0)
        {
            frontBar.localPosition = Vector3.Lerp(_min, _max, current / (float)factor);
            backBar.localPosition = Vector3.Lerp(_min, _max, previous / (float)factor);

            StartCoroutine(Co_MoveBarSlowly(new Co_MoveBarSlowly_Data(backBar, frontBar.localPosition)));
        }
        else
        {
            frontBar.localPosition = Vector3.Lerp(_min, _max, previous / (float)factor);
            backBar.localPosition = Vector3.Lerp(_min, _max, current / (float)factor);

            StartCoroutine(Co_MoveBarSlowly(new Co_MoveBarSlowly_Data(frontBar, backBar.localPosition)));
        }
    }

    private IEnumerator Co_MoveBarSlowly(Co_MoveBarSlowly_Data data)
    {
        while (data.bar.localPosition != data.end)
        {
            data.bar.localPosition = Vector3.Lerp(data.bar.localPosition, data.end, 0.1f);
            yield return new WaitForEndOfFrame();
        }
    }

    private struct Co_MoveBarSlowly_Data
    {
        public RectTransform bar;
        public Vector3 end;

        public Co_MoveBarSlowly_Data(RectTransform bar_, Vector3 end_)
        {
            bar = bar_;
            end = end_;
        }
    }
}
