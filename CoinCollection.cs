using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    
    
    
    private int Coin = 0; 

    public TMP_Text CoinText;



    private void OnTriggerEnter(Collider other) 
    {

            if(other.transform.tag == "Coin") 
            {
                Coin++;
                CoinText.text = "Coin:" + Coin.ToString();
                Debug.Log(Coin);
                Destroy(other.gameObject);
            }




    }
      

}
