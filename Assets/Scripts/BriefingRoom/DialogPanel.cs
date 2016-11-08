using UnityEngine;
using System.Collections;
using System.Linq;

public class DialogPanel : MonoBehaviour
{
    public CharacterData[] characterData;

    private CharacterBehaviour _speaker;
    private CharacterBehaviour[] _members;
    private TextArea _textArea;

    void Awake()
    {
        _speaker = transform.GetChild(1).GetComponentInChildren<CharacterBehaviour>();
        _members = transform.GetChild(0).GetComponentsInChildren<CharacterBehaviour>();
        _textArea = GetComponentInChildren<TextArea>();
    }
    
    public void AddCharacter(ScenarioManager.e_Characters character)
    {
#if UNITY_EDITOR
        if (_members.Any(x => x.Id == character))
        {
            Debug.Log(character.ToString() + " : member already connected.");
            return;
        }

        if (!_members.Any(x => x.Id == ScenarioManager.e_Characters.None))
        {
            Debug.Log(character.ToString() + " not enough place in the conv to add this member");
            return;
        }
#endif
        CharacterBehaviour emptyMember = _members.First(x => x.Id == ScenarioManager.e_Characters.None);
        CharacterData sd = characterData.Single(x => x.id == character);
        emptyMember.SetCharacter(sd);
    }

    public void RemoveCharacter(ScenarioManager.e_Characters character)
    {
#if UNITY_EDITOR
        if (!_members.Any(x => x.Id == character))
        {
            Debug.Log(character.ToString() + " : member not connected.");
            return;
        }
#endif
        _members.Single(x => x.Id == character).ClearCharacter();
    }

    public void SetSpeaker(ScenarioManager.e_Characters character)
    {
        _speaker.SetCharacter(characterData.Single(x => x.id == character));
    }

    public void RemoveSpeaker()
    {
        _speaker.ClearCharacter();
    }

    public void WriteMsg(string msg, ScenarioManager.e_Characters character)
    {
        _textArea.AddText(msg, characterData.Single(x => x.id == character).associatedColor);
    }

    public void ResetDialogPanel()
    {
        foreach (var v in _members)
        {
            v.ResetCharacter();
        }
        _speaker.ResetCharacter();
        _textArea.ResetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            AddCharacter(ScenarioManager.e_Characters.Lambda1);
        if (Input.GetKeyDown(KeyCode.Z))
            RemoveCharacter(ScenarioManager.e_Characters.Lambda1);

        if (Input.GetKeyDown(KeyCode.E))
            SetSpeaker(ScenarioManager.e_Characters.Lambda1);
        if (Input.GetKeyDown(KeyCode.R))
            RemoveSpeaker();


    }

    [System.Serializable]
    public struct CharacterData
    {
        public ScenarioManager.e_Characters id;
        public Sprite illustration;
        public Color associatedColor;
    }
}
