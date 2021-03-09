using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeSceneButtom (string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void changeStategameButtom(int numStateGAme)
    {
        if (numStateGAme == 1)
        {
            GameManager.shareInstance.currentGameState = GameState.ingame;
            GameManager.shareInstance.rooms++;
            GameManager.shareInstance.levelPlayer++;
        }
        else if (numStateGAme == 2)
        {
            GameManager.shareInstance.currentGameState = GameState.start;
        }
    }
}
