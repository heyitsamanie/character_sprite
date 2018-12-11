using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private AudioSource coinSound;

    private void Start()
    {
        coinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            coinSound.Play();

           // count = count + 1;
           // SetCountText();
        }
    }
}
