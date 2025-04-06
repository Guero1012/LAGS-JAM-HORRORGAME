using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{

    public GameManager gamemanager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (collision.gameObject.name == "Player" && gamemanager.hasWonDarts && gamemanager.hasWonMarble && gamemanager.hasWonSoccer)
        {
            SceneManager.LoadScene("Ganar");
        }
    }
}
