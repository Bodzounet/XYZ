using UnityEngine;
using System.Collections.Generic;
using System;

public class SpawnTrigger : ATrigger
{
    public SpawnTriggerData[] enemyToSpawn;

    public override void Activate()
    {
        foreach (var v in enemyToSpawn)
        {
            GameObject go = Instantiate(v.enemy, v.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(GameObject.FindGameObjectWithTag("Avatar").transform);
        }
    }

    [System.Serializable]
    public struct SpawnTriggerData
    {
        public GameObject enemy;
        public Vector3 position;
    }
}
