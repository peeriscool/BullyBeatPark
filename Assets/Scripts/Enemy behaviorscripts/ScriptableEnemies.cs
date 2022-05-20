using UnityEngine;

[CreateAssetMenu(menuName = "EnemyType")]
public class ScriptableEnemies : ScriptableObject
{
    public string prefabName;
    [Range(1, 10)] public int amountTomake;
    public Vector3[] spawnPoints;
    public GameObject Prefab;
}
