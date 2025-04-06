using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    public float speed;
    [Header("Pon aqui la pantalla de la UI")]
    public GameObject blackScreen;
    public string sceneName;

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
       blackScreen.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        // Cambiar cámara
        SceneManager.LoadScene(sceneName);
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

    private void Update()
    {
        if (manager.currentState == Manager.enemyStates.Attack) // && ShouldStartAttacking()) AQUI DEBE IR UNA VARIABLE POR SI TIENE LOS OJOS CERRADPOS
        {
            manager.ChangeState(Manager.enemyStates.Flee);
        }
    }

}
