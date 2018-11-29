using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knife")
        {
            this.gameObject.SetActive(false);
            //            GameObject.activeself
        }
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
