using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject Camera;
    private bool up;

    void Start()
    {
        up = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 direction = this.transform.position;
        if (collision.tag == "Player")
        {
            if (up)
            {
                direction += new Vector3(0, 1.1f);
                Camera.transform.position += new Vector3(0, 12f, 0);
                up = false;
            }
            else
            {
                direction += new Vector3(0, -1.1f);
                Camera.transform.position += new Vector3(0, -12f, 0);
                up = true;
            }
            collision.transform.position = direction;
        }
    }
}
