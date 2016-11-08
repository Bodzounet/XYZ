using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// child 1 is the img;
/// child 2 is the frame;
/// </summary>
public class CharacterBehaviour : MonoBehaviour
{
    private Animator _anim;
    private Image _img;
    private Image _frame;

    private ScenarioManager.e_Characters _id = ScenarioManager.e_Characters.None;

    public ScenarioManager.e_Characters Id
    {
        get { return _id; }
    }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _img = transform.GetChild(1).GetComponent<Image>();
        _frame = transform.GetChild(2).GetComponent<Image>();
    }

    public void SetCharacter(DialogPanel.CharacterData data)
    {
        if (data.id == Id)
            return;

        _id = data.id;
        _img.sprite = data.illustration;
        _frame.color = data.associatedColor;
        _anim.SetBool("In", true);
        _anim.SetBool("Out", false);
        _anim.SetTrigger("Switch");
    }

    public void ClearCharacter()
    {
        _id = ScenarioManager.e_Characters.None;
        _frame.color = Color.black;
        _anim.SetBool("In", false);
        _anim.SetBool("Out", true);
    }

    public void ResetCharacter()
    {
        _id = ScenarioManager.e_Characters.None;
        _frame.color = Color.black;
        _anim.SetBool("In", false);
        _anim.SetBool("Out", false);
    }
}
