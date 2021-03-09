using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatoController : MonoBehaviour
{
    GameManager gameManager;
    private bool goToControl;
    private bool goToCredits;
    private bool goToStart;
    private bool goToPause, goToDeath, goToChange;
    CanvasController canvasController;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        canvasController = transform.parent.GetComponent<CanvasController>();
    }

    public void GotoCredits()
    {
        goToCredits = false;
        goToControl = false;
        goToStart = false;
        goToCredits = true;
        canvasController.GoToAnimation(goToCredits, goToControl, goToStart);
    }
    public void GotoControl()
    {
        goToCredits = false;
        goToControl = false;
        goToStart = false;
        goToControl = true;
        canvasController.GoToAnimation(goToCredits, goToControl, goToStart);
    }
    public void GotoStart()
    {
        goToCredits = false;
        goToControl = false;
        goToStart = false;
        goToStart = true;
        canvasController.GoToAnimation(goToCredits, goToControl, goToStart);
    }
    public void ResumeButton()
    {
        GameManager.shareInstance.ReanudeGame();
    }
}
