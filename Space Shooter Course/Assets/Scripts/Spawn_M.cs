using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_M : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;
    
    private bool _alive = true;

    public void SpawningNest() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_alive == true)
        {
            Vector3 spawn = new Vector3(Random.Range(-9.6f, 9.6f), 8, 0);
            GameObject newEnemy =  Instantiate(_enemyPrefab, spawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform ;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_alive == true)
        {
            Vector3 spawn = new Vector3(Random.Range(-9.6f, 9.6f), 8, 0);
            int _randomPowerUps = Random.Range(0, 3);
            Instantiate(powerUps[_randomPowerUps], spawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
        
    }

    public void OnPlayerDeath() {
        _alive = false;
    }


}
