using UnityEngine;

public class Manager : MonoBehaviour
{

    public EnemyNahual enemyNahual;
    public EnemyOruga enemyOruga;
    public EnemyChaneque enemyChaneque;

    EnemyBase currentEnemy;
    Coroutine currentState;

    //Maquina de estados como la de mi alumno carlos
    private enum{ attack, move, spawn, flee };

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // enemyNahual.EnemySpawn();
        StartCoroutine(enemyNahual.SpawnEnemy());
    }

    /* Enemy logic
     * vars:
     * -tiempo cerca del nahual
     * 
     * -Monedas que llevas
     * -veces que ha salido el gusano
     * 
     * 
     */


    public void ChangeStates()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
