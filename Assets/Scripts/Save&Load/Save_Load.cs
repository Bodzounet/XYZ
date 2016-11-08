using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Save_Load : Singleton<Save_Load>
{
    public GameObject PickUpHolder;
    public GameObject EnemyHolder;
    public GameObject TriggerHolder;

    void Start()
    {
        PickUpHolder = GameObject.Find("PickUpHolder");
        EnemyHolder = GameObject.Find("EnemyHolder");
        TriggerHolder = GameObject.Find("TriggerHolder");

        SceneManager.activeSceneChanged += (Scene before, Scene After) =>
        {
            PickUpHolder = GameObject.Find("PickUpHolder");
            EnemyHolder = GameObject.Find("EnemyHolder");
            TriggerHolder = GameObject.Find("TriggerHolder");
        };
    }

    #region Save

    public void SaveGame(int slot)
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        //if (!currentSceneName.Contains("Level"))
        //    return;

        StartCoroutine(Co_Save(slot));
    }

    private IEnumerator Co_Save(int slot)
    {
        SaveFile sf = new SaveFile();

        int loop = 0;
        foreach (var triggerData in _SaveTrigger())
        {
            sf.triggerData.Add(triggerData);
            if (++loop > 10)
            {
                loop = 0;
                yield return new WaitForEndOfFrame();
            }
        }

        Exception e = null;
        System.Threading.Thread thread = new System.Threading.Thread(() => _SafeExecute(() => { _WriteData(sf, slot); }, out e));
        thread.Start();
        while (thread.IsAlive)
            yield return new WaitForEndOfFrame();

        if (e != null)
        {
            _HandleSaveException(e);
            yield break;
        }
    }

    private void _SavePickUp(SaveFile savefile)
    {
        for (int i = 0; i < PickUpHolder.transform.childCount; i++)
        {
            Transform child = PickUpHolder.transform.GetChild(i);

            PickUpData pud = new PickUpData();
            pud.position = child.position;
            pud.id = child.GetComponent<APickUp>().id;

            savefile.pickUpData.Add(pud);
        }
    }

    private void _SaveEnemy(SaveFile savefile)
    {
        for (int i = 0; i < EnemyHolder.transform.childCount; i++)
        {
            Transform child = EnemyHolder.transform.GetChild(i);

            EnemyData ed = new EnemyData();

            ed.position = child.position;
            ed.rotation = child.rotation;

            EnemyId ei = child.GetComponent<EnemyId>();
            ed.enemyId = ei.id;
            ed.enemyElite = ei.elite;

            ed.hp = child.GetComponent<HealthManager>().Life;

            savefile.enemyData.Add(ed);
        }
    }

    private IEnumerable<TriggerData> _SaveTrigger()
    {
        for (int i = 0; i < TriggerHolder.transform.childCount; i++)
        {
            var child = TriggerHolder.transform.GetChild(i);

            TriggerData td = new TriggerData();

            TriggerManager tm = child.GetComponent<TriggerManager>();
            td.id = tm.triggerId;
            td.isSwitch = tm.isSwitch;
            td.isActivated = tm.isActivated;

            td.position = child.position;
            td.rotation = child.rotation;

            Collider c = child.GetComponent<Collider>();
            if (c is BoxCollider)
            {
                td.triggerCenter = (c as BoxCollider).center;
                td.triggerSize = (c as BoxCollider).size;
                td.type = TriggerData.e_ColliderType.Box;
            }
            else if (c is SphereCollider)
            {
                td.triggerCenter = (c as SphereCollider).center;
                td.triggerRadius = (c as SphereCollider).radius;
                td.type = TriggerData.e_ColliderType.Sphere;
            }

            yield return td;
        }
    }

    private void _SavePlayer(SaveFile savefile)
    {

    }

    private void _WriteData(SaveFile savefile, int slot)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/XYZ";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        TextWriter writer = new StreamWriter(path + "/" + slot.ToString() + ".save");
        serializer.Serialize(writer, savefile);

        writer.Close();
    }

    #endregion

    #region Load

    public void Load(int slot)
    {
        StartCoroutine(Co_Load(slot));
    }

    private IEnumerator Co_Load(int slot)
    {
        SaveFile sf = null;

        Exception e = null;
        System.Threading.Thread thread = new System.Threading.Thread(() => _SafeExecute(() => { sf = _ReadData(slot); }, out e));
        thread.Start();
        while (thread.IsAlive)
            yield return new WaitForEndOfFrame();

        if (e != null)
        {
            _HandleLoadException(e);
            yield break;
        }

        int loop = 0;
        foreach (var trigger in _LoadTrigger(sf))
        {
            trigger.transform.SetParent(TriggerHolder.transform);
            if (++loop > 10)
            {
                loop = 0;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerable<GameObject> _LoadTrigger(SaveFile sf)
    {
        foreach (var v in sf.triggerData)
        {
            GameObject child = new GameObject();
            //child.transform.SetParent(TriggerHolder.transform);

            TriggerManager tm = child.AddComponent<TriggerManager>();
            tm.triggerId = v.id;
            tm.isSwitch = v.isSwitch;
            tm.isActivated = v.isActivated;

            child.transform.position = v.position;
            child.transform.rotation = v.rotation;

            Debug.Log(v.type);

            if (v.type == TriggerData.e_ColliderType.Box)
            {
                BoxCollider c = child.AddComponent<BoxCollider>();
                c.center = v.triggerCenter;
                c.size = v.triggerSize;
                c.isTrigger = true;
            }
            else
            {
                SphereCollider c = child.AddComponent<SphereCollider>();
                c.center = v.triggerCenter;
                c.radius = v.triggerRadius;
                c.isTrigger = true;
            }
            yield return child;
        }
    }

    private SaveFile _ReadData(int slot)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/XYZ/" + slot.ToString() + ".save";
        TextReader reader = new StreamReader(path);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
        SaveFile sf = serializer.Deserialize(reader) as SaveFile;
        reader.Close();
        return sf;
    }

    #endregion

    #region SerializabledData

    [System.Serializable]
    public class SaveFile
    {
        public List<EnemyData> enemyData = new List<EnemyData>();
        public List<PickUpData> pickUpData = new List<PickUpData>();
        public List<TriggerData> triggerData = new List<TriggerData>();
    }

    [System.Serializable]
    public struct PickUpData
    {
        public Vector3 position;
        public APickUp.e_PickUpId id;
    }

    [System.Serializable]
    public struct EnemyData
    {
        public Vector3 position;
        public Quaternion rotation;
        public EnemyId.e_EnemyID enemyId;
        public EnemyId.e_EnemyElite enemyElite;
        public int hp;
    }

    [System.Serializable]
    public struct TriggerData
    {
        public enum e_ColliderType
        {
            Box = 0,
            Sphere = 1,
        }

        public int id;
        public bool isSwitch;
        public bool isActivated;

        public Vector3 position;
        public Quaternion rotation;

        public e_ColliderType type;
        public Vector3 triggerCenter;

        public Vector3 triggerSize; // for boxCollider;
        public float triggerRadius; // for sphereCollider;
    }

    [System.Serializable]
    private struct PlayerData
    {
        public int blueAmmo;
        public int redAmmo;

    }

    #endregion

    #region Thread Functions

    private void _SafeExecute(Action action, out Exception e)
    {
        e = null;

        try
        {
            action.Invoke();
        }
        catch (Exception ex)
        {
            e = ex;
        }
    }

    private void _HandleLoadException(Exception e)
    {
        if (e is DirectoryNotFoundException || e is FileNotFoundException || e is IOException)
        {
            Debug.Log("Unable to find or to open the save file :-(");
        }
        else if (e is InvalidOperationException)
        {
            Debug.Log("Save file corrupted :/");
        }
        else
        {
            Debug.Log("Something went wrong when loading the save file and it is not a common error :/ feelsbadman");
        }

        StopAllCoroutines();
        Debug.Log("backToMenu");
    }

    private void _HandleSaveException(Exception e)
    {
        if (e is DirectoryNotFoundException || e is FileNotFoundException || e is IOException || e is UnauthorizedAccessException)
        {
            Debug.Log("Unable to create the save file :-(");
        }
        else
        {
            Debug.Log("Something went wrong when saving the progression and it is not a common error :/ feelsbadman");
        }

        StopAllCoroutines();
        Debug.Log("backToGame, progression has not been saved");
    }

    #endregion
}
