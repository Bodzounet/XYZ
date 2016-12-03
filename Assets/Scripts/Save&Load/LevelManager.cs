using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    LoadingScreen _loadingScreen;

    void Start()
    {
        _loadingScreen = GameObject.FindObjectOfType<LoadingScreen>();

        SceneManager.activeSceneChanged += (Scene before, Scene After) =>
        {
            _loadingScreen = GameObject.FindObjectOfType<LoadingScreen>();
        };
    }

    public void LoadLevel(string lvlName)
    {
        _loadingScreen.IsEnabled = true;
        _loadingScreen.PrintMessage("LoadingLevel...");
        SceneManager.LoadSceneAsync(lvlName);
        _loadingScreen.IsEnabled = false;
    }

    public void LoadLevelFromSavedFile(int slot)
    {
        Save_Load.Instance.Load(slot);
    }
}
