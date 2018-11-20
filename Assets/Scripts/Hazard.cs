using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered hazard.");
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.Die();
            
            //CharacterController player = collision.GetComponent<CharacterController>();
           // SceneManager.LoadScene("GameOver");
           
        }

        else
        {
            Debug.Log("Something other than the player entered the hazard.");
        }

        Debug.Log("Something entered the hazard");
    }

}
