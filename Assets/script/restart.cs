using UnityEngine.SceneManagement;

public class restart : InteractiveItem
{
    public override void interacted_with ( MainPlayerController player ) { Reset(); }

    public override void exit ( MainPlayerController player ) {}

    public void Reset() {
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
