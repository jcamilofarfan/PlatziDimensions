using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private const string control = "GotoControl";
    private const string credits = "GotoCredits";
    private const string start = "GotoStart";
    private const string paused = "paused";
    private const string Death = "Death";
    private const string Change = "Change";
    private Animator canvasAnimator;
    void Start()
    {
        canvasAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager.shareInstance.currentGameState == GameState.pause) PauseGame();
        if (GameManager.shareInstance.currentGameState == GameState.ingame) NotPauseGame();
        if (GameManager.shareInstance.currentGameState == GameState.change) ChangeRoom();
        if (GameManager.shareInstance.currentGameState == GameState.playerDeath) DeathPlayer();

    }

    public void GoToAnimation(bool creditsb, bool controlb, bool startb)
    {
        canvasAnimator.SetBool(control, controlb);
        canvasAnimator.SetBool(credits, creditsb);
        canvasAnimator.SetBool(start, startb);
    }
    public void PauseGame()
    {
        Debug.Log("si se envia");
        canvasAnimator.SetBool(paused, true);
        canvasAnimator.SetBool(Death, false);
        canvasAnimator.SetBool(Change, false);
    }
    public void NotPauseGame()
    {
        canvasAnimator.SetBool(paused, false);
        canvasAnimator.SetBool(Death, false);
        canvasAnimator.SetBool(Change, false);
    }
    public void ChangeRoom()
    {
        Debug.Log("si se envia");
        canvasAnimator.SetBool(paused, false);
        canvasAnimator.SetBool(Death, false);
        canvasAnimator.SetBool(Change, true);
    }
    public void DeathPlayer()
    {
        Debug.Log("si se envia");
        canvasAnimator.SetBool(paused, false);
        canvasAnimator.SetBool(Death, true);
        canvasAnimator.SetBool(Change, false);
    }


}
