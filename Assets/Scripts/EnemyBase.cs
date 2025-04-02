using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> spawnPoints = new List<GameObject>(); 
    

    protected Transform currentSpawnPoint;
    [SerializeField]
    protected bool canSpawnNear,canSpawnFar;

    protected GameObject player;

    private void Awake()
    {
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoints"));
        player = GameObject.Find("Player");
    }

    public virtual void EnemySpawn()
    {
        List<GameObject> _spawnPoints = new List<GameObject>();

        //Put this in the childs
        //bool canSpawn2 = CheckIfPlayerIsInVision();
       

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

    public bool CheckIfPlayerIsInVision(Vector3 RaycastPoint)
    {
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Default", "Obstacles");
        Debug.DrawRay(RaycastPoint, player.transform.position * 100, Color.red);

        if(Physics.Linecast(transform.position,player.transform.position, out hit,layerMask))
        {
            Debug.Log("Obstáculo detectado: " + hit.collider.gameObject.name);
            Debug.Log("AAAAA");
            return false;
            
        }

        
        

        return true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "NearSphere")
        {
            Debug.Log("No bas");
            canSpawnNear = true;
            
        }
        else if(other.gameObject.name == "FarSphere")
        {
            Debug.Log("Basado");
            canSpawnFar = true;
        }
        



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "NearSphere")
        {
            Debug.Log("No bas");
            canSpawnNear = false;

        }
        else if (other.gameObject.name == "FarSphere")
        {
            Debug.Log("Basado");
            canSpawnFar = false;
        }
    }

}
