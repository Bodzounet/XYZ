using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
    public enum e_Characters
    {
        Lambda1,
        Lambda2,

        None
    }

    public List<ScenarioChunk> scenario;

    public DialogPanel dialogPanel;

    private AudioSource _as;
    private bool _paused;
    public bool Paused
    {
        get { return _paused; }
        set
        {
            if (value)
                _as.Pause();
            else
                _as.UnPause();
            _paused = value;
        }
    }

    private int _idx = 0;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
    }

    void Start()
    {
        dialogPanel.ResetDialogPanel();
        _UseChunk(scenario[_idx++]);
    }

    void LateUpdate()
    {
        if (!_paused && !_as.isPlaying && _idx < scenario.Count)
        {
            _UseChunk(scenario[_idx++]);
        }
    }

    private void _UseChunk(ScenarioChunk chunk)
    {
        foreach (var v in chunk.leavingMembers)
        {
            dialogPanel.RemoveCharacter(v);
        }
        foreach (var v in chunk.newMembers)
        {
            dialogPanel.AddCharacter(v);
        }

        dialogPanel.SetSpeaker(chunk.speaker);
        dialogPanel.WriteMsg(chunk.msg, chunk.speaker);
        _as.PlayOneShot(chunk.audioMsg);
    }

    public void ResetScenario()
    {
        _as.Stop();
        _idx = 0;
        dialogPanel.ResetDialogPanel();
        _paused = true;
        Invoke("_WaitAfterReset", 2);
    }

    private void _WaitAfterReset()
    {
        Paused = false;
    }

    [System.Serializable]
    public struct ScenarioChunk
    {
        public e_Characters speaker;
        public AudioClip audioMsg;
        public string msg;

        public e_Characters[] newMembers;
        public e_Characters[] leavingMembers;
    }
}
