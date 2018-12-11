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

   
}
