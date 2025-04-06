using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            EnemyNahual nahual = FindFirstObjectByType<EnemyNahual>();
            if (nahual != null)
            {
                nahual.playerDeath = true;
            }
        }
    }
}
