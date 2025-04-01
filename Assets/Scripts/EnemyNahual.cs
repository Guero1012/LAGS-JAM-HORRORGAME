using System.Collections.Generic;
using UnityEngine;

public class EnemyNahual : EnemyBase
{
    public override void EnemySpawn()
    {
        List<GameObject> _spawnPoints = new List<GameObject>();

        //Put this in the childs
        //bool canSpawn2 = CheckIfPlayerIsInVision();
        while (!canSpawn)
        {
            Debug.Log("A");
            if (spawnPoints.Count == 0)
            {
                Debug.Log("Theres no room to spawn");
                break;
            }
            int x = Random.Range(0, _spawnPoints.Count);
            transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
            
            
                _spawnPoints.Remove(_spawnPoints[x]);
            
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
