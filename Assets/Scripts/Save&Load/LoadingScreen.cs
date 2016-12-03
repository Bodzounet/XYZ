using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    CanvasGroup _cg;
    Text _progression;

    Animator _animator;

    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
        _progression = GetComponentInChildren<Text>();

        _animator = GetComponent<Animator>();
    }

    public void PrintMessage(string msg)
    {
        _progression.text = msg;
    }

    public bool IsEnabled
    {
        set
        {
            if (value)
            {
                _progression.text = "";
                _animator.SetBool("On", true);
            }
            else
            {
                _animator.SetBool("On", false);
            }
        }
    }
}
