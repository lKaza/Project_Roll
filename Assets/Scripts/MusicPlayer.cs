using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour
{
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }   
    [SerializeField] float levelDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene",levelDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadNextScene(){
        SceneManager.LoadScene(1);
        

    }
}
