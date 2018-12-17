using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
   public void ContinueGame()
    {
        if (Input.GetButtonDown("Respawn"))
            SceneManager.GetActiveScene();
    }

}