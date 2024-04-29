using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    public GameObject ui_EndGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CallEndGame();   
    }

    private void CallEndGame()
    {
        ui_EndGame.SetActive(true);
    }
    


}
