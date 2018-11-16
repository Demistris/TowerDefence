﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 10f;

    public int health = 100;

    public int value = 10;

    public GameObject deathEffect;

    private Transform target;
    private int wavePointIndex = 0;
	
	void Start ()
    {
        target = Waypoints.points[0];
	}

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.money += value;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
	
	
	void Update ()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
	}

    void GetNextWaypoint()
    {
        if(wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
