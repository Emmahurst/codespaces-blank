using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int worth = 1;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Checks for trigger collision that matches the tag "Player".
        if (col.CompareTag("Player"))
        {
            //Destroys the collectable object.
            Destroy(gameObject);
            //Increases the players White Blood Cell Count, based on the objects worth varaible for future scaling.
            col.GetComponent<PlayerController>().score += worth;
        }

        
    }
}
