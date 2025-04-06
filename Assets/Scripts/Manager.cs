using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public EnemyNahual enemyNahual;
    public EnemyOruga enemyOruga;
    public EnemyChaneque enemyChaneque;

    [HideInInspector]
    public EnemyBase currentEnemy;
    Coroutine currentCoroutine;

    //Maquina de estados como la de mi alumno carlos
    public enum enemyStates { Attack, Move, Spawn, Flee, GoAway };

    [HideInInspector]
    public enemyStates currentState;

    public int TryToSpawnAgain;

    float timer;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // enemyNahual.EnemySpawn();
        currentEnemy = enemyOruga;
        StartCoroutine(enemyOruga.EnemySpawn());
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



    private void SetActiveEnemy(EnemyBase newEnemy)
    {
        if (currentEnemy != null)
            currentEnemy.gameObject.SetActive(false); // Desactiva el enemigo anterior

        currentEnemy = newEnemy;
        currentEnemy.gameObject.SetActive(true); // Activa el nuevo enemigo
    }

    public void ChangeState(enemyStates newState)
    {
        currentEnemy.gameObject.SetActive(true);        


        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine); // Detiene la corrutina anterior

        currentState = newState;

        switch (currentState)
        {
            case enemyStates.Spawn:
                currentCoroutine = StartCoroutine(currentEnemy.EnemySpawn());
                break;
            case enemyStates.Move:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyMove());
                break;
            case enemyStates.Attack:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyAttack());
                break;
            case enemyStates.Flee:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyLeave());
                break;
            case enemyStates.GoAway:
                currentEnemy.gameObject.SetActive(false);
                break;
        }
    }


    public void ChangeStates()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(currentState == enemyStates.GoAway)
        {
            timer += Time.deltaTime;
            if(timer > TryToSpawnAgain)
            {
                ChangeState(enemyStates.Spawn);
                timer = 0;
            }
        }
    }
}
