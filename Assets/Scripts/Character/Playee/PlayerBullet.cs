using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : PoolObj<PlayerBullet> {
    private float Count = 0.3f;
    private Vector3 Velocity;

	// Update is called once per frame
	void Update () {
        StartCoroutine("ShootCoroutine");
        transform.position = Vector3.MoveTowards(transform.position, Velocity, Time.deltaTime*6);
    }

    IEnumerator ShootCoroutine() {
        yield return new WaitForSeconds(Count);
        PlayerBullet.Pool(this);
        
    }

    public void Shoot(Vector3 pos, Vector3 velocity) {
        Velocity = velocity;
        //Debug.Log(Velocity);
        transform.position = pos;
    }

    public override void Init() {
        gameObject.SetActive(true);
    }
    public override void Sleep() {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D co) {
        if(co.gameObject.tag == "Wall") {
            this.gameObject.SetActive(false);
        }
    }
}
