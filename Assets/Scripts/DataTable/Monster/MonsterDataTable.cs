using UnityEngine;
using InfiniteValue;

[CreateAssetMenu(menuName = "ScriptableObjects/MonsterDataTable")]
public class MonsterDataTable : ScriptableObject
{
    public string monsterName; // ���� �̸�
    public float monsterHP; // ���� ü��
    public InfVal dropResourceAmount; // ����Ǵ� �ڿ��� �� 
}
