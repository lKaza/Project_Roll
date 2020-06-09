using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene",3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadNextScene(){
        SceneManager.LoadScene(1);
    }
}
