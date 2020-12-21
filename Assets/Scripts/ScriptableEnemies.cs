using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyType")]
public class ScriptableEnemies : ScriptableObject
{
        
        public string prefabName;
        public int numberOfPrefabsToCreate;
        public Vector3[] spawnPoints;
        public GameObject Models;
    
}
