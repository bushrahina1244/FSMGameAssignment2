using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetection : MonoBehaviour
{

    public Text health;
    public int playerhealth;
    private bool isDead; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     //For player and same is for enemy  
  
    void OnTriggerEnter(Collider other)  
    {
        if (other.gameObject.tag=="enemy")
        {
            playerhealth = playerhealth - 5;
            health.text = playerhealth.ToString();
           
            print("Dead");
            
        }
        else if(playerhealth == 0)
        {
            print("Dead");
        }
        
    }
   
}
