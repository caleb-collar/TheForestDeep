using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    [SerializeField] private Slider healthBar;
    private bool endGame = false;
    

    // Update is called once per frame
    void Update()
    {
        if (healthBar.value <= 0 && !endGame)
        {
            endGame = true;
            StartCoroutine(PlayerDestroyed()); //End
        }
    }
    
    IEnumerator PlayerDestroyed()
    {
        yield return new WaitForSeconds(2); //For animations and sound.
        loader.LoadNextScene();
    }
}
