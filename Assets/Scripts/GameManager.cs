using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    //pantalla de inicio
    start,
    //menude home en partida
    home,
    //pausa ingame
    pause,
    //reanudarjuego
    reanude,
    //jugando
    ingame,
    //cuando muere un jugador lo envia al home
    playerDeath,
    //desde el home accede a la tienda
    inStore,
    //cambio de Dungeon
    change,
    inchange,
}

public class GameManager : MonoBehaviour
{
    public static bool gameControllerCreate;
    RoomsCreate setParentChild;
    public float timeToDestroy;
    private string submit = "Submit";

    //public List<GameObject> players;

    public int rooms;
    public int  inGame;
    public GameObject[] enemys;
    public GameObject player;
    public GameObject canvasStart;
    public GameObject animationStart;
    public GameObject playerSpawn, spawnRooms, Rooms,canvasingame;
    public GameState currentGameState = GameState.start;
    public static GameManager shareInstance;

    public int numberBackGround;
    public int numberplayer;
    private int numberGamePlay;
    public int levelPlayer;
    public int expPlayer;
    Vector2 startPosition;
    void Awake()
    {
        if(shareInstance== null)
        {
            shareInstance = this;
        }
        
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!gameControllerCreate)
        {
            gameControllerCreate = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        levelPlayer=0;
        expPlayer=0;

        rooms = 0;
        inGame = 0;
        currentGameState = GameState.start;
    }
    void Update()
    {

        if(currentGameState == GameState.change)
        {
            ChangeDungeon();
        }
        if (currentGameState == GameState.playerDeath)
        {
            PlayerDeath();
        }

        if (currentGameState == GameState.start)
        {
            if (Input.GetButtonDown(submit))
            {
                StartGame();
            }
        }
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        /*

        if (inGame == 1)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                inGame++;
                StartGame();
            }
        }

        if (enemys.Length == 0) enemys = GameObject.FindGameObjectsWithTag("Enemy");


        


        if (currentGameState == GameState.start)
        {
            if (Input.GetButtonDown(submit)) {
                if(inGame== 0)
                {
                    inGame++;
                    GameObject animation = (GameObject)Instantiate(animationStart, startPosition, Quaternion.identity);
                    Destroy(GameObject.FindGameObjectWithTag("CanvasStart"));

                }
                else
                {
                    StartGame();
                }
            }
        }
        */
        if (Input.GetButtonDown("Fire2"))
        {
            if(currentGameState == GameState.ingame)
            {
                PauseGame();
                    
            }else if (currentGameState == GameState.pause)
            {
                ReanudeGame();
            }
        }
    }



    public void StartGame()
    {
        numberplayer = Random.Range(1, 3);
        if (numberplayer == 3)
        {
            numberplayer = 2;
        }
        numberBackGround = Random.Range(1, 5);
        if (numberBackGround == 5)
        {
            numberBackGround = 4;
        }
        SetGameState(GameState.ingame);
    }
    public void ChangeDungeon()
    {
        SetGameState(GameState.change);
    }
    public void PlayerDeath()
    {
        SetGameState(GameState.playerDeath);
    }
    public void BackToHome()
    {
        SetGameState(GameState.home);
    }
    public void GameOpen()
    {
        SetGameState(GameState.start);
    }
    public void PauseGame()
    {

        SetGameState(GameState.pause);
    }
    public void ReanudeGame()
    {
        SetGameState(GameState.reanude);
    }
    public void PlayerStore()
    {
        SetGameState(GameState.inStore);
    }

    public void CurrentLevel(int level)
    {
        levelPlayer = level;
    }


    public void CurrentExp(int Exp)
    {
        expPlayer = Exp;
    }


    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.start)
        {
            currentGameState = newGameState;
            SceneManager.LoadScene("Start");
        }
        else if(newGameState == GameState.ingame)
        {
            currentGameState = newGameState;
            if (0 >= rooms) rooms++;
            SceneManager.LoadScene("Niveles");
        }
        else if(newGameState == GameState.inStore)
        {
            currentGameState = newGameState;
        }
        else if (newGameState == GameState.home)
        {
            currentGameState = newGameState;
        }
        else if (newGameState == GameState.pause)
        {
            currentGameState = newGameState;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (newGameState == GameState.reanude)
        {
            currentGameState = GameState.ingame;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (newGameState == GameState.playerDeath)
        {
            levelPlayer = 0;
            rooms = 0;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (newGameState == GameState.change)
        {

            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //SceneManager.LoadScene("NextRoom");
            //newGameState = GameState.inchange;
            //rooms++;
        }
        
    }
}
