using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOruga : EnemyBase
{

    Rigidbody body;

    [Header("Oruga Settings")]
    [SerializeField] private float _forceMultiplier = 10f; // Fuerza de movimiento
    [SerializeField] private float _directionChangeChance = 0.2f; // 20% de chance de cambiar direcci�n
    [SerializeField] private float _minRespawnTime = 3f; // Tiempo m�nimo antes de reaparecer
    [SerializeField] private float _maxRespawnTime = 6f; // Tiempo m�ximo antes de reaparecer

    private Vector3 _initialDirection;
    private bool _isMoving = false;

    private int untilAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        untilAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void FixedUpdate()
    {

        RotateTowardsMovement();

        
    }

    private void RotateTowardsMovement()
    {
        Vector3 moveDirection = body.linearVelocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                5 * Time.fixedDeltaTime
            );
        }
    }

    /* Oruga movimiento
     * 
         * Addforce hacia en frente apuntando cerca del jugador 
         * Addforce hacia una posicion random
         */

    /* Oruga Ataque
     * De frente y abre la boca
     * 
     * 
     * 
     */

    public override IEnumerator EnemySpawn()
    {
        List<GameObject> _spawnPoints = new List<GameObject>(spawnPoints);


        while (_spawnPoints.Count > 0)
        {
            int x = Random.Range(0, _spawnPoints.Count);
            transform.position = _spawnPoints[x].transform.position;

            yield return new WaitForSeconds(0.1f);
            Debug.Log(canSpawnNear + "" + canSpawnFar + IsVisibleToCamera(Camera.main));
            
            if (canSpawnFar && !canSpawnNear && !IsVisibleToCamera(Camera.main))
            {
                Debug.Log("Punto v�lido encontrado, enemigo spawneado.");
                manager.ChangeState(Manager.enemyStates.Move);
                yield break; // Termina la corrutina al encontrar un lugar adecuado
            }

            Debug.Log("Punto no v�lido, probando otro...");
            _spawnPoints.RemoveAt(x); // Elimina el punto inv�lido de la lista

        }

        Debug.Log("No se encontr� un punto adecuado, enemigo no spawneado.");
        manager.ChangeState(Manager.enemyStates.GoAway);

    }

    public override IEnumerator EnemyMove()
    {
        if (_isMoving) yield break; // Evita m�ltiples corrutinas
        _isMoving = true;

        // 1. Calcula direcci�n inicial (cerca del jugador pero sin chocar)
        Vector3 playerPos = player.transform.position;
        Vector3 randomOffset = new Vector3(
            Random.Range(-20f, 20f),
            0,
            Random.Range(-20f, 20f)
        );

        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        

        _initialDirection = (playerPos + randomOffset - transform.position).normalized;
        body.AddForce(_initialDirection * _forceMultiplier, ForceMode.Impulse);

        // 2. Movimiento con cambios aleatorios de direcci�n
        while (body.linearVelocity.magnitude > 0f)
        {
            // Chance de cambiar direcci�n (sin apuntar directamente al jugador)
            if (Random.value < _directionChangeChance)
            {
                Vector3 newDirection = new Vector3(
                    Random.Range(-20f, 20f),
                    0,
                    Random.Range(-20f, 20f)
                ).normalized;

                body.AddForce(newDirection * _forceMultiplier * 0.5f, ForceMode.Impulse);

                //transform.rotation = Quaternion.LookRotation(newDirection);
            }

            yield return new WaitForSeconds(0.5f); // Revisa cada medio segundo
        }

        // 3. Desaparece y programa respawn
        _isMoving = false;
        //manager.ChangeState(Manager.enemyStates.GoAway);

        yield return new WaitForSeconds(Random.Range(_minRespawnTime, _maxRespawnTime));

        if (untilAttack == 0)
        {
            manager.ChangeState(Manager.enemyStates.Attack);
            untilAttack = Random.Range(4, 12);
        }
        else
        {
            untilAttack--;
            manager.ChangeState(Manager.enemyStates.Spawn);
        }

        
    }

    public override IEnumerator EnemyAttack()
    {
        transform.position = player.transform.position +
                                player.transform.forward * 20 +
                                Vector3.up * 2.5f;


        if (_isMoving) yield break; // Evita m�ltiples corrutinas
        _isMoving = true;

        // 1. Calcula direcci�n inicial (cerca del jugador pero sin chocar)
        Vector3 playerPos = player.transform.position;


        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;


        _initialDirection = (playerPos - transform.position).normalized;
        body.AddForce(_initialDirection * _forceMultiplier, ForceMode.Impulse);

        yield return new WaitForSeconds(.9f);

        anim.SetBool("Attack", true);

        // 2. Movimiento con cambios aleatorios de direcci�n
       

        // 3. Desaparece y programa respawn
        _isMoving = false;
        //manager.ChangeState(Manager.enemyStates.GoAway);

        yield return new WaitForSeconds(Random.Range(_minRespawnTime, _maxRespawnTime));

    }

    

    public override IEnumerator EnemyLeave()
    {
        return base.EnemyLeave();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && manager.currentState == Manager.enemyStates.Attack)
        {
            //if()
            playerDeath = true;
            StartCoroutine(DeathSequence());
        }


        if (other.gameObject.name == "Player")
        {
            Vector3 escapeDirection = (transform.position - player.transform.position).normalized;
            body.AddForce(escapeDirection * _forceMultiplier, ForceMode.Impulse);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "AreaOruga")
        {
            StartCoroutine(EnemySpawn());
        }
    }*/


}
