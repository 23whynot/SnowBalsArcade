using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory _factory;
    [SerializeField] private int _maxEnemies;
    
    private int _spawnTimer = 2;
    private int _enemyNowActivated;
    
    public void EnemyNowDeactivated()
    {
        _enemyNowActivated -= 1;
    }
    
    private void Start()
    {
        StartCoroutine(SpawnProcess());
    }
    
    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTimer);
            
            if (_maxEnemies > _enemyNowActivated)
            {
                _enemyNowActivated++;
                _factory.SpawnRandomEnemy();
            }
        }
    }
}
