using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNahual : EnemyBase
{
   
    public List<GameObject> movePoints = new List<GameObject>();

    
    public GameObject head;
    private BoxCollider headTrigger;
    RaycastHit hit;
    private bool isPlayerInRange;
    NavMeshAgent agent;

    private GameObject lastpoint;

    public float timerToSpawn;

    public override IEnumerator EnemySpawn()
    {

        yield return new WaitForSeconds(2);

        List<GameObject> _spawnPoints = new List<GameObject>(spawnPoints);


        while (_spawnPoints.Count > 0)
        {
            int x = Random.Range(0, _spawnPoints.Count);
            transform.position = _spawnPoints[x].transform.position;

            yield return new WaitForSeconds(0.01f);

            if (canSpawnFar && !canSpawnNear && !CheckIfPlayerIsInVision(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z)))
            {
                Debug.Log("Punto válido encontrado, enemigo spawneado.");
                manager.ChangeState(Manager.enemyStates.Move);
                yield break; // Termina la corrutina al encontrar un lugar adecuado
            }

            Debug.Log("Punto no válido, probando otro...");
            _spawnPoints.RemoveAt(x); // Elimina el punto inválido de la lista

        }

        Debug.Log("No se encontró un punto adecuado, enemigo no spawneado.");
        manager.ChangeState(Manager.enemyStates.GoAway);



    }

    public override IEnumerator EnemyMove()
    {
        /* Nahual movimiento
         *  -Se mueva lentamente 
         *  -se detenga en algun punto del mapa
         *  ---Grite (30)
         *  ---Porcentaje de que te voltee a ver inmediatamente (10)
         *  ---se quede en idle, oliendo o algo similar (60)
         * 
         * 
         */
        agent.speed = 1;


        GameObject nearPoint1 = null;

        foreach (GameObject p in movePoints)
        {
            if(nearPoint1 == null || Vector3.Distance(transform.position, nearPoint1.transform.position) > Vector3.Distance(transform.position, p.transform.position))
            {
                if (lastpoint != p)
                {
                    nearPoint1 = p;
                }
                               
            }

            
        }

        Debug.Log(nearPoint1);

        //Ni modo hacer simple el sistema

        nearPoint1 = movePoints[Random.Range(0, movePoints.Count)];

        while(nearPoint1 != null && agent.remainingDistance < 0.2f)
        {
            //transform.position =  Vector3.MoveTowards(transform.position, nearPoint1.transform.position, speed*Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(transform.position);

            agent.destination = nearPoint1.transform.position;

            yield return null;
        }

        yield return new WaitForSeconds(1);

        int n = RandomNumber();

        if(n < 10) //voltear
        {
            
        }
        else if(n < 30) //gritar
        {

        }
        lastpoint = nearPoint1;

        StartCoroutine(EnemyMove());
        
    }

    public override IEnumerator EnemyAttack()
    {
        agent.enabled = true;
        //Se avalanza contra ti
        // 
        while (true)
        {
            agent.SetDestination(player.transform.position);

            agent.speed = 3;

            // Detecta si el jugador ha sido atrapado
            if (playerDeath) // esta variable debe ser activada desde el trigger
            {
                agent.isStopped = true;
                //yield return StartCoroutine(DeathSequence());
                yield break;
            }

            yield return null;
        }
        
    }

    public override IEnumerator EnemyLeave()
    {
        //Depende en que estado estes

        /* Pasar de attack
         * -Eventos al azar
         * ---Sonido de atacar y desvanece (50)
         * ---Sonido cerca y pasa de largo (50)
         * 
         * 
         * Pasar de move
         * -Eventos al azar
         * ---Desvanece (10)
         * ---Aparece enfrente de ti (5)
         * ---Empieza a correr hacia ti y se pasa de largo (20)
         * ---No pasa nada (60)
         * ---Susurra pero desaparece (5)
         */

        this.gameObject.transform.position = new Vector3(1000,1000, 1000);

        yield return new WaitForSeconds(timerToSpawn);
        manager.ChangeState(Manager.enemyStates.Spawn);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        movePoints.AddRange(GameObject.FindGameObjectsWithTag("MovePoints"));
    }

    // Update is called once per frame
    void Update()
    {

        //Cambiar a ataque
        if (manager.currentState == Manager.enemyStates.Move && ShouldStartAttacking())
        {
            manager.ChangeState(Manager.enemyStates.Attack);
        }

        
        
    }

    bool ShouldStartAttacking()
    {
        Debug.Log(HeadCanSeePlayer() + "head");
        Debug.Log(IsVisibleToCamera(Camera.main));
        return HeadCanSeePlayer() && IsVisibleToCamera(Camera.main);
    }

    bool HeadCanSeePlayer()
    {
        Vector3 directionToPlayer = (new Vector3(player.transform.position.x,player.transform.position.y + 1,player.transform.position.z) - head.transform.position).normalized;
        float dotProduct = Vector3.Dot(head.transform.forward, directionToPlayer);

        Debug.DrawRay(head.transform.position, directionToPlayer * 50f, Color.red);
        //Debug.Log(dotProduct + " dot");
        //if (dotProduct > 0.2f) // El jugador está casi en frente de la cabeza
        //{
            if (Physics.Raycast(head.transform.position, directionToPlayer, out hit, 50f))
            {
                Debug.Log(hit.transform.name);

                return hit.transform.name == "Player";
            }
        //}

        return false;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" &&  player.GetComponent<PlayerStats>().hasEyesClosed)
        {
            manager.ChangeState(Manager.enemyStates.Flee);
        }
        else if(other.gameObject.name == "Player" && !player.GetComponent<PlayerStats>().hasEyesClosed)
        {

            playerDeath = true;
            StartCoroutine(DeathSequence());
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPlayerInRange = false;
        }
    }*/
}




