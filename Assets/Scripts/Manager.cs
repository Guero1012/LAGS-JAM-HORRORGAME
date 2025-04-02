using UnityEngine;

public class Manager : MonoBehaviour
{

    public EnemyNahual enemyNahual;
    public EnemyOruga enemyOruga;
    public EnemyChaneque enemyChaneque;

    EnemyBase currentEnemy;
    Coroutine currentCoroutine;

    //Maquina de estados como la de mi alumno carlos
    private enum enemyStates { Attack, Move, Spawn, Flee };

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



    private void SetActiveEnemy(EnemyBase newEnemy)
    {
        if (currentEnemy != null)
            currentEnemy.gameObject.SetActive(false); // Desactiva el enemigo anterior

        currentEnemy = newEnemy;
        currentEnemy.gameObject.SetActive(true); // Activa el nuevo enemigo
    }

    public void ChangeState(enemyStates newState)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine); // Detiene la corrutina anterior

        currentState = newState;

        switch (currentState)
        {
            case enemyStates.Spawn:
                currentCoroutine = StartCoroutine(currentEnemy.SpawnEnemy());
                break;
            case EnemyState.Move:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyMove());
                break;
            case EnemyState.Attack:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyAttack());
                break;
            case EnemyState.Flee:
                currentCoroutine = StartCoroutine(currentEnemy.EnemyLeave());
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
