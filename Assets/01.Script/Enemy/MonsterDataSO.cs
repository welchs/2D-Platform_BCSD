using UnityEngine;


[CreateAssetMenu(fileName = "MonsterDataS0", menuName = "Scriptable Objects/MonsterDataSO")]
public class MonsterDataSO : ScriptableObject
{
    public string name;
    public float hp;
    public float moveSpeed;
    public float damage;
    public float attackTime;
}
