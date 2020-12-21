using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackBoard
{
    [SerializeField]
    private List<GameObject> EnemyModels = new List<GameObject>();
    private List<Vector3> SpawnLocations = new List<Vector3>();
    public BlackBoard(List<ScriptableEnemies> a)
    {
        //convert scriptable objects to gameobjects with spawn coordinants
        for (int i = 0; i < a.Count; i++)
        {
            for (int X = 0; X < a[i].numberOfPrefabsToCreate; X++)
            {
                EnemyModels.Add(a[i].Models);
                SpawnLocations.Add(a[i].spawnPoints[X]);
            }

        }
    }

    public List<GameObject> GetEnemyModels()
    {
        return EnemyModels;
    }
    public Vector3 GetSpawnLocation(int index)
    {
        return SpawnLocations[index];
    }
}
    //example code valentijn

    //[SerializeField] private List<FloatValue> floatVariables = new List<FloatValue>();
    ////[SerializeField] private List<FloatValue> boolVariables = new List<FloatValue>();

    //public Dictionary<VariableType, FloatValue> VariableDictionary { get; private set; } = new Dictionary<VariableType, FloatValue>();

    //public void OnInitialize()
    //{
    //    VariableDictionary = new Dictionary<VariableType, FloatValue>();
    //    foreach(var v in floatVariables)
    //    {
    //        VariableDictionary.Add(v.Type, v);
    //    }
    //}

    //public FloatValue GetFloatVariableValue(string name)
    //{
    //    var res = floatVariables.Find(x => x.name == name);
    //    return res;
    //}
    //public FloatValue GetFloatVariableValue(VariableType type)
    //{
    //    if (VariableDictionary.ContainsKey(type))
    //    {
    //        return VariableDictionary[type];
    //    }
    //    return null;
    //}
    //public bool GetBoolVariableValue(string name, out bool result)
    //{
    //    var res = floatVariables.Find(x => x.name == name);
    //    if (res != null)
    //    {
    //        result = res.Value;
    //        return true;
    //    }
    //    else
    //    {
    //        result = 0;
    //        return false;
    //    }
    //}

