using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void LoadLevel()
    {

    }

    public void LoadSavedGame(int slot)
    {
        Save_Load.Instance.Load(slot);
    }

    private IEnumerator Co_Load()
    {
        yield return SceneManager.LoadSceneAsync("");
    }
}
