using UnityEngine;

public class RotationEnemyFollow : MonoBehaviour
{
    public bool Chase = false;
    public float rotationSpeed = 5f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (Chase && player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Ajustamos la rotación para que sea el eje X el que apunte al jugador
            Quaternion xForward = Quaternion.Euler(
                targetRotation.eulerAngles.z,
                targetRotation.eulerAngles.y - 90f,
                -targetRotation.eulerAngles.x
            );

            transform.rotation = Quaternion.Slerp(transform.rotation, xForward, rotationSpeed * Time.deltaTime);
        }
    }
}
