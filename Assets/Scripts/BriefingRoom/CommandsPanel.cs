using UnityEngine;
using System.Collections;

public class CommandsPanel : MonoBehaviour
{
    //public Sprite 

    public void OpenMenu()
    {
        Debug.Log("menu not created yet");
    }

    public void Pause()
    {
        // no need, done with ScenarioManager;
    }

    public void Replay()
    {

    }

    public void StartMission()
    {
        LevelManager.Instance.LoadLevel("Testing AI");
    }
}
