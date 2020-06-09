using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicPlayer : MonoBehaviour
{
    private void Awake(){

        int numbMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if( numbMusicPlayers>1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(this.gameObject);
        }
        
    }   
   
    // Start is called before the first frame update
    
}
