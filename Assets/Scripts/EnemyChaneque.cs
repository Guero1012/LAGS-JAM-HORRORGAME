using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaneque : EnemyBase
{ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override IEnumerator EnemySpawn()
    {


        List<GameObject> _spawnPoints = new List<GameObject>(spawnPoints);


        while (_spawnPoints.Count > 0)
        {
            int x = Random.Range(0, _spawnPoints.Count);
            transform.position = _spawnPoints[x].transform.position;

            yield return new WaitForSeconds(0.5f);

            if (canSpawnFar && !canSpawnNear)
            {
                manager.ChangeState(Manager.enemyStates.Move);
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
        yield return new WaitForSeconds(Random.Range(2,10));
        manager.ChangeState(Manager.enemyStates.Attack);
    }

    public override IEnumerator EnemyAttack()
    {
        while (Vector3.Distance(transform.position,player.transform.position) >0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }



    public override IEnumerator EnemyLeave()
    {
        this.gameObject.transform.position = new Vector3(1000, 1000, 1000);

        yield return new WaitForSeconds(5);
        manager.ChangeState(Manager.enemyStates.Spawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<PlayerStats>().hasEyesClosed)
        {
            manager.ChangeState(Manager.enemyStates.Flee);
        }
        else
        {
            playerDeath = true;
            StartCoroutine(DeathSequence());
        }
    }

}
