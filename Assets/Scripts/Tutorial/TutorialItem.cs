using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour {
    private bool flag;
    void Start()
    {
        flag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flag == false && collision.tag == "Player")
        {
            BarriarNearDoor.shelfCounter++;
            flag = true;
        }
    }
}
