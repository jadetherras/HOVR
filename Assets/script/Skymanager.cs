using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Skymanager : MonoBehaviour
{
    public Material menu;
    public Material tutorial;
    public Material level1;
    public Material level2;
    public Material level3;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
 
        if(scene.name == "tutorial")
            RenderSettings.skybox = tutorial;
        else if(scene.name == "level1")
            RenderSettings.skybox = level1;
        else if(scene.name == "level2")
            RenderSettings.skybox = level2;
        else if(scene.name == "level3")
            RenderSettings.skybox = level3;
        else if(scene.name == "menu")
            RenderSettings.skybox = menu;
    }

}
