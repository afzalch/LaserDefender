using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBtwShots = 0.2f;
    [SerializeField] float maxTimeBtwShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserSpeed = -5f;

	// Use this for initialization
	void Start () {
        shotCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            //resets timer between shots!
            shotCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage(); 
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
