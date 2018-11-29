using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private Stats health;
    // Here, Stats refers once again to our other script, Stats. This is what will allow us to add any information we want on the variables we set on the Stats script.
    // The Stats script creates the functions needed to have stats, but it is the Player script which actually determines what stats does the player have.

    private void Awake()
    {
        health.Initialize();
    }

        // If we write health.StatMaxVal then our player will start with its max health.
        // To make it increase, we can do for example health.StatCurrentVal + 10.

	
	// Update is called once per frame
	void Update () {

        HealDamage();
 
        TakeDamage();
        
    }

    private void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            health.StatCurrentVal -= 1;
        }
    }

    private void HealDamage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            health.StatCurrentVal += 1;
        }
    }

}
