﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerEnemy=20;
    [SerializeField] int HP = 5;
    ScoreBoard scoreBoard;
    void Start()
    {
        AddCollider();  
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddCollider()
    {
        Collider myCollider = gameObject.AddComponent<BoxCollider>();
        myCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        reduceHP();
        if(HP<=0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        scoreBoard.ScoreHit(scorePerEnemy);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }

    void reduceHP(){
        HP = HP-1;
    }
}
