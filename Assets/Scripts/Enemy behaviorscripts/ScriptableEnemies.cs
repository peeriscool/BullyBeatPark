using UnityEngine;

[CreateAssetMenu(menuName = "EnemyType")]
public class ScriptableEnemies : ScriptableObject
{
    public string prefabName;
    public int amountTomake;
    public Vector3[] spawnPoints;
    public GameObject Prefab;
}
