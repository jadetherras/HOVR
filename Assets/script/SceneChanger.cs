using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Player perso")]
    public GameObject player;
    public GameObject god;

    [Header("Player cameras")]
    public GameObject playerHuman;

    protected bool mode = false;
    protected bool test = true;
    // Update is called once per frame
    void Update()
    {
        
        bool b_3 = OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.Touch);
        bool b_2 = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.Touch);

        if(b_3 && !b_2){
            if(test){
                if(!playerHuman.GetComponent<MainPlayerController>().GetMode()){
                    playerHuman.GetComponent<CharacterController>().enabled = false;
                    player.transform.SetPositionAndRotation(playerHuman.transform.position, playerHuman.transform.rotation);

                    playerHuman.transform.position = god.transform.position;
                    playerHuman.transform.localScale = new Vector3(10,10,10);
                    player.SetActive(true);
                    playerHuman.GetComponent<CharacterController>().enabled = true;
                    playerHuman.GetComponent<MainPlayerController>().SetMode(true);
                    test = false;
                }else{
                    player.SetActive(false);
                    playerHuman.GetComponent<CharacterController>().enabled = false;
                    playerHuman.transform.localScale = new Vector3(1,1,1);
                    playerHuman.transform.position = player.transform.position;

                    playerHuman.GetComponent<CharacterController>().enabled = true;
                    playerHuman.GetComponent<MainPlayerController>().SetMode(false);
                    test = false;
                }
            }
        }else{
            test = true;
        }
    }
}
