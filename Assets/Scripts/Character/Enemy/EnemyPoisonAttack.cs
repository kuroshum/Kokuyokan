using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoisonAttack : MonoBehaviour {

    [SerializeField]
    private int direction;
    private Vector3 vector;
    private float abs = 1.5f;
	// Use this for initialization
	void Start () {
        switch (direction)
        {
            case 1:
                vector = new Vector3(1, 0, 0) * abs;
                break;
            case 2:
                vector = new Vector3(-1, 0, 0) * abs;
                break;
            case 3:
                vector = new Vector3(0, -1, 0) * abs;
                break;
            case 4:
                vector = new Vector3(0, 1, 0) * abs;
                break;
            default:
                vector = new Vector3(0, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.position += vector * 0.7f * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }

   
}
