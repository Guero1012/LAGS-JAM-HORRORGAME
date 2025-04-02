using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNahual : EnemyBase
{
   


    

    public IEnumerator SpawnEnemy()
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
        transform.position = new Vector3(1000, 1000, 1000);



    }

    public override void EnemyMove()
    {
        /* Nahual movimiento
         *  -Se mueva lentamente 
         *  -se detenga en algun punto del mapa
         *  ---Grite
         *  ---Porcentaje de que te voltee a ver inmediatamente
         *  ---se quede en idle, oliendo o algo similar
         * 
         * 
         */
    }

    public override void EnemyAttack()
    {
      //Se avalanza contra ti
      // 
    }

    public override void EnemyLeave()
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, player.transform.position * 100, Color.red);
    }
}
