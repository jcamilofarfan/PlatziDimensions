using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController shareInstance;
    public float speed = 300f;
    private float timeIdel = 5.0f;
    private bool walking = false;
    private bool attack = false;
    private bool idel = false;
    public Vector2 lastMovement = Vector2.zero;
    Vector3 startPosition;

    public GameObject target;
    public CharacterStats stats;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string keyAttack = "Fire1";
    private const string attackState = "Attack";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "Walking";
    private const string idealState = "Idel";
    private const string attackEnemyState = "AttackEnemy";

    private Animator animator;
    private Rigidbody2D playerRigidbody;
    void Awake()
    {
        if (shareInstance == null)
        {
            shareInstance = this;
        }

    }

    void Start()
    {
        stats = GetComponent<CharacterStats>();
        target = this.gameObject;

        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    public void StartGame()
    {
        this.transform.position = startPosition;
    }



    void Update()
    {
        float totalSpeed = speed;
        if (stats != null)
        {
            totalSpeed *= stats.speedLevels[stats.currentLevel];
        }
        idel = false;
        attack = false;
        walking = false;
        if (timeIdel <= 0 && walking == false && attack == false){
            idel = true;
        }else{
            timeIdel -= Time.deltaTime;
        }

        if(Mathf.Abs(Input.GetAxisRaw(keyAttack)) > 0.5f)
        {
            attack = true;
            timeIdel = 5.0f;
        }
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f || Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            walking = true;
            timeIdel = 5.0f;
            lastMovement = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
            playerRigidbody.velocity = lastMovement.normalized * totalSpeed * Time.deltaTime;
        }

        if(Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f && 
            Mathf.Abs(Input.GetAxisRaw(keyAttack)) > 0.5f && 
            Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            walking = true;
            attack = true;
        }
        animator.SetFloat(horizontal, Input.GetAxisRaw(horizontal));
        animator.SetFloat(vertical, Input.GetAxisRaw(vertical));
        animator.SetBool(walkingState, walking);
        animator.SetFloat(lastHorizontal, lastMovement.x);
        animator.SetFloat(lastVertical, lastMovement.y);

        animator.SetBool(idealState, idel);
        animator.SetBool(attackState, attack);


        //animator.SetFloat(timeIdel, )


        if (!walking)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

}
}
