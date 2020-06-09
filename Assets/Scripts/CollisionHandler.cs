using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float LevelDelay=5f;
    [Tooltip("Particle effect")][SerializeField] GameObject DeathFX;
    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        
    }

    private void StartDeathSequence()
    {
        gameObject.SendMessage("Dying");
        DeathFX.SetActive(true);
        Invoke("ReloadLevel",LevelDelay);
        
    }
    void  ReloadLevel(){ //string reference
        SceneManager.LoadScene(1);
    }
}
