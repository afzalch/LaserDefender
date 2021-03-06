﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 0.47f;
    [SerializeField] float yPadding = 0.4f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 8f;
    [SerializeField] float projectileFiringPeriod=0.1f;
    //purpose of padding is to make sure no part of goes outside of playspace

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update () {
        Move();
        Fire();
    }

    private void Fire()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine =  StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator FireContinuously()
    {
        while (true)
        {

            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }

    }


    private void Move()
    {
        //can check edit->Project Settings -> Input for all the axises and how to control/move
        //time.deltaTime - makes all computers regardless of number of fps to run at same speed
        var deltaX = Input.GetAxis("Horizontal")*Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);

        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
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
