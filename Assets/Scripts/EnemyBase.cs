using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> spawnPoints = new List<GameObject>();
    [HideInInspector]
    public Manager manager;
    protected Transform currentSpawnPoint;
    [SerializeField]
    protected bool canSpawnNear,canSpawnFar;

    protected GameObject player;

    protected float speed;

    public Camera mainCamera, deathCamera;

    public bool playerDeath;

    private void Awake()
    {
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoints"));
        player = GameObject.Find("Player");
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    public virtual IEnumerator EnemySpawn()
    {
        //List<GameObject> _spawnPoints = new List<GameObject>();



        return null;
        //Put this in the childs
        //bool canSpawn2 = CheckIfPlayerIsInVision();
       

    }

    public virtual IEnumerator EnemyMove()
    {
        return null;
    }

    public virtual IEnumerator EnemyAttack()
    {
        return null;
    }
    public virtual IEnumerator EnemyLeave()
    {
        return null;
    }
    //Para saber si spawnea o no
    public virtual bool CheckIfPlayerIsInVision(Vector3 RaycastPoint)
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

    public virtual IEnumerator DeathSequence()
    {
        // Fade a negro, PAUL
       

        yield return new WaitForSeconds(1f);

        // Cambiar cámara
        if (mainCamera != null) mainCamera.gameObject.SetActive(false);
        if (deathCamera != null) deathCamera.gameObject.SetActive(true);

        // Aquí podrías llamar una animación también si lo deseas
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

    protected bool IsVisibleToCamera(Camera cam)
    {
        Vector3 viewportPoint = cam.WorldToViewportPoint(transform.position);
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }
    public int RandomNumber()
    {
        return Random.Range(1, 101);
    }

}
