using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Character", menuName = "Swipe Objects/Character")]
public class EnemyCharacter : ScriptableObject
{
    public string SkinName;
    public float Speed;
    public int ScoreCost;
}