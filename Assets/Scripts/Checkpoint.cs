using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100;

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the checkpoint.");
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.SetCurrentCheckpoint(this);

        }
    }
}
