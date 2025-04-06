using System.Drawing;
using UnityEngine;

public class ChangeArea : MonoBehaviour
{
    public GameObject Points1,Points2,Points3,Points4;

    Manager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AreaNahual")
        {
            if(other.gameObject.name == "Area1")
            {
                TurnOn(Points1);
            }
            else if(other.gameObject.name == "Area2")
            {
                TurnOn(Points2);
            }
            else if(other.gameObject.name == "Area3")
            {
                TurnOn(Points3);
            }

            manager.ChangeState(Manager.enemyStates.GoAway);

            manager.currentEnemy = manager.enemyNahual;


        }
        else if(other.gameObject.tag == "AreaOruga")
        {
            TurnOn(Points4);

            manager.ChangeState(Manager.enemyStates.GoAway);

            manager.currentEnemy = manager.enemyOruga;
        }
    }

    public void TurnOn(GameObject point)
    {
        Points1.SetActive(false);
        Points2.SetActive(false);
        Points3.SetActive(false);
        Points4.SetActive(false);

        point.SetActive(true);
    }

    
}
