using UnityEngine;

public class Manager : MonoBehaviour
{

    public EnemyNahual enemyNahual;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
