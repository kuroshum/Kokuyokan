using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour {
    public Camera preCamera;
    public Camera nextCamera;
    public GameObject door;
    public bool up;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 direction = door.transform.position;
        if (collision.tag == "Player")
        {
            if (up)
            {
                direction += new Vector3(0, 1.1f);
            }
            else
            {
                direction += new Vector3(0, -1.1f);
            }
            collision.transform.position = direction;
            preCamera.enabled = false;
            nextCamera.enabled = true;
        }
    }
}

