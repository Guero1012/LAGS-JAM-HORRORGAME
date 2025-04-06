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

    

    public override IEnumerator EnemySpawn()
    {

        

        List<GameObject> _spawnPoints = new List<GameObject>(spawnPoints);


        while (_spawnPoints.Count > 0)
        {
            int x = Random.Range(0, _spawnPoints.Count);
            transform.position = _spawnPoints[x].transform.position;

            yield return new WaitForSeconds(0.5f);

            if (canSpawnFar && !canSpawnNear && !CheckIfPlayerIsInVision(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z)))
            {
                Debug.Log("Punto válido encontrado, enemigo spawneado.");
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

        GameObject nearPoint1 = null;

        foreach (GameObject p in movePoints)
        {
            if(nearPoint1 == null || Vector3.Distance(player.transform.position, nearPoint1.transform.position) > Vector3.Distance(player.transform.position, p.transform.position))
            {
                nearPoint1 = p;
            }

            
        }

        while(Vector3.Distance(nearPoint1.transform.position,transform.position) > .1f)
        {
           transform.position =  Vector3.MoveTowards(transform.position, nearPoint1.transform.position, speed*Time.deltaTime);
            
        }

        yield return new WaitForSeconds(1);

        int n = RandomNumber();

        if(n < 10) //voltear
        {
            
        }
        else if(n < 30) //gritar
        {

        }
        

        
    }

    public override IEnumerator EnemyAttack()
    {
        agent.enabled = true;
        //Se avalanza contra ti
        // 
        while (true)
        {
            agent.SetDestination(player.transform.position);

            // Detecta si el jugador ha sido atrapado
            if (playerDeath) // esta variable debe ser activada desde el trigger
            {
                agent.isStopped = true;
                yield return StartCoroutine(DeathSequence());
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

        return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        movePoints.AddRange(GameObject.FindGameObjectsWithTag("MovePoints"));
    }

    // Update is called once per frame
    void Update()
    {


        if (manager.currentState == Manager.enemyStates.Move && ShouldStartAttacking())
        {
            manager.ChangeState(Manager.enemyStates.Attack);
        }
    }

    bool ShouldStartAttacking()
    {
        return isPlayerInRange && HeadCanSeePlayer() && IsVisibleToCamera(Camera.main);
    }

    bool HeadCanSeePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - head.transform.position).normalized;
        float dotProduct = Vector3.Dot(head.transform.forward, directionToPlayer);

        Debug.DrawRay(head.transform.position, directionToPlayer * 50f, Color.red);

        if (dotProduct > 0.95f) // El jugador está casi en frente de la cabeza
        {
            if (Physics.Raycast(head.transform.position, directionToPlayer, out hit, 50f))
            {
                return hit.transform.name == "Player";
            }
        }

        return false;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPlayerInRange = false;
        }
    }
}




