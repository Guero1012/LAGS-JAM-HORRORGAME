using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    List<GameObject> spawnPoints = new List<GameObject>(); 
    

    Transform currentSpawnPoint;

    bool canSpawn;

    private void Awake()
    {
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoints"));
    }

    public virtual void EnemySpawn()
    {
        List<GameObject> _spawnPoints = new List<GameObject>();

        //Put this in the childs
        //bool canSpawn2 = CheckIfPlayerIsInVision();
        while (canSpawn)
        {
            int x = Random.Range(0, _spawnPoints.Count); 
            transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
            if (spawnPoints.Count == 0)
            {
                Debug.Log("Theres no room to spawn");
                break;
            }
            else
            {
                _spawnPoints.Remove(_spawnPoints[x]);
            }
            
        }
        //transform SpawnAvailable;
        if (canSpawn)
        {


            //gameObject.transform.position = 
        }

    }

    public virtual void EnemyMove()
    {

    }

    public virtual void EnemyAttack()
    {

    }
    public virtual void EnemyLeave()
    {

    }

    public bool CheckIfPlayerIsInVision(Vector3 RaycastPoint, GameObject player)
    {
        RaycastHit hit;

        if (Physics.Raycast(RaycastPoint, player.transform.position,out hit))
        {
            if(hit.collider.name == "Player")
            {
                return true;

            }
            

        }
        

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FarSpehere" && other.gameObject.tag != "NearSpehere")
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

        
    }

}
