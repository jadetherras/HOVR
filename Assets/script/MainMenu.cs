using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayTuto() {  
        SceneManager.LoadScene("tutorial");  
    }
     
    public void PlayLevel1() {  
        SceneManager.LoadScene("level1");  
    }

    public void PlayLevel2() {  
        SceneManager.LoadScene("level2");  
    }  

    public void PlayLevel3() {  
        SceneManager.LoadScene("level3");  
    }  

    public void PlayQuit() {  
        Application.Quit();  
    } 
     

    //pas sûre que ça fonctionne !!
    public void QuitGame() {  
        Debug.Log("QUIT");  
        SceneManager.LoadScene("Quit");  
    }  
    
}
