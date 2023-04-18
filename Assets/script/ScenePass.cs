using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenePass : InteractiveItem

{
    public enum Scenepass : int { Restart, LevelUp, LevelDown, Default};

    [Header( "Scenepass" )]
    public Scenepass scenepass;
    public int scenenb=0;

    void Start(){
        switch(scenepass) 
        {
        case Scenepass.Restart:
            scenenb = SceneManager.GetActiveScene().buildIndex;
            break;
        case Scenepass.LevelUp:
            scenenb = SceneManager.GetActiveScene().buildIndex+1;
            break;
        case Scenepass.LevelDown:
            scenenb = SceneManager.GetActiveScene().buildIndex-1;
            break;
        default:
            break;
        }
	}

    public override void interacted_with ( MainPlayerController player ) { 
        if(!player.GetMode()){
            Pass(); 
        }
    }

    public override void exit ( MainPlayerController player ) {}

    public void Pass() {
        SceneManager.LoadScene(scenenb);
    }

}
