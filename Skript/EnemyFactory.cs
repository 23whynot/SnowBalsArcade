using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _leoPrefab;
    [SerializeField] private Transform _spawnPoint, _PointA, _PointB, targetForSnowBall;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private List<EnemyCharacter> characterList;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Score score;

    public void SpawnRandomEnemy()
    {
        Spawn<Enemy>(GetCharacter());
    }

    private void Start()
    {
        _objectPool.RegisterPrefab<Enemy>(_leoPrefab, 5); //TODO
    }

    private EnemyCharacter GetCharacter()
    {
        int randomIndex = Random.Range(0, characterList.Count);
        return characterList[randomIndex];
    }


    private T Spawn<T>(EnemyCharacter character) where T : Enemy
    {
        T enemy = _objectPool.GetObject<T>();
        enemy.Initialize(_PointA, _PointB, _objectPool, targetForSnowBall, _spawnPoint, character, spawner, score);
        enemy.transform.position = _spawnPoint.position;


        return enemy;
    }
}