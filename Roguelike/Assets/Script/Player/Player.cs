using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(PlayerFSM))]
public class Player : MonoBehaviour
{
    public int Speed = 10; 

    private Rigidbody2D PlayerRd;
    private Transform PlayerTra;
   // [SerializeField] private LayerMask layermask;

    [HideInInspector]public PlayerSO playerSO;
    [HideInInspector]public Health health;
    [HideInInspector]public SpriteRenderer spriteRenderer;
    [HideInInspector]public Animator animator;

    public bool canMove = true; 

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        PlayerRd = GetComponent<Rigidbody2D>();
        
      
    }
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        if(canMove)
        PlayerRd.velocity = moveDirection * Speed;
        else
            PlayerRd.velocity = Vector2.zero;
    } 


    void Update()
    {
       
    }

    private void Move()
    {
   
    }

    public void Initialized(PlayerSO playerSO)
    {
        this.playerSO = playerSO;

        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerSO.playerHealthAmount);
    }


    
}
