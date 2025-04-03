using UnityEngine;

public class Manager : MonoBehaviour
{

    public EnemyNahual enemyNahual;
    public EnemyOruga enemyOruga;
    public EnemyChaneque enemyChaneque;

    EnemyBase currentEnemy;
    Coroutine currentCoroutine;

    //Maquina de estados como la de mi alumno carlos
    public enum enemyStates { Attack, Move, Spawn, Flee, GoAway };

    enemyStates currentState;



    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // enemyNahual.EnemySpawn();
        StartCoroutine(enemyNahual.EnemySpawn());
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
        currentEnemy.enabled = true;

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
                currentEnemy.enabled = false;
                break;
        }
    }


    public void ChangeStates()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
