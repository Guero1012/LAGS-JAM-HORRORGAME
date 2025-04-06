using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOruga : EnemyBase
{

    Rigidbody body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

            yield return new WaitForSeconds(0.5f);

            if (canSpawnFar && !canSpawnNear && !IsVisibleToCamera(Camera.main))
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
        return base.EnemyMove();
    }

    public override IEnumerator EnemyAttack()
    {
        return base.EnemyAttack();
    }

    

    public override IEnumerator EnemyLeave()
    {
        return base.EnemyLeave();
    }

    public override bool CheckIfPlayerIsInVision(Vector3 RaycastPoint)
    {
        return true;
    }

}
