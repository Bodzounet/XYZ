using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextArea : MonoBehaviour
{
    private List<Text> _textBlocks = new List<Text>();
    private List<Vector3> _pos = new List<Vector3>();

    private Animator _anim;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _textBlocks.Add(transform.GetChild(i).GetComponent<Text>());
            _pos.Add(_textBlocks[i].transform.localPosition);
        }

        _anim = GetComponent<Animator>();
    }

    public void AddText_Anim()
    {
        for (int i = 0; i < _textBlocks.Count - 1; i++)
        {
            _textBlocks[i].text = _textBlocks[i + 1].text;
            _textBlocks[i].color = _textBlocks[i + 1].color;
        }
        _textBlocks.Last().text = _nextmsg;
        _textBlocks.Last().color = _nextColor;
    }

    private string _nextmsg;
    private Color _nextColor;

    public void AddText(string msg, Color color)
    {
        _nextmsg = msg;
        _nextColor = color;
        _anim.Play("MoveText");
    }

    public void ResetText()
    {
        _anim.Play("Idle");
        foreach (var v in _textBlocks)
        {
            v.text = "";
        }
    }
}
