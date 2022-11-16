using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMov : MonoBehaviour
{
    private SpriteRenderer ZombieSprite;
    Rigidbody2D Zombie_Rigidbody;
    Vector3 _Vetorpos;
    bool IsOnFloor;
    private Animator animator;
    private Animator OtherAnimator;

    public float SpeedMov = 1.0f;
    public float Impulse = 400f;
    float HorInput;


    // Start is called before the first frame update
    void Start()
    {
        ZombieSprite = GetComponent<SpriteRenderer>();
        Zombie_Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        IsOnFloor = true;
    }

    // Update is called once per frame
    void Update()
    {
        HorInput = Input.GetAxis("Horizontal") * SpeedMov;

        MovePlayer();
        if (IsOnFloor && Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {

    }
    void MovePlayer()
    {


        if (ZombieSprite != null)
        {
            if (HorInput < 0)
            {
                animator.SetBool("IsWalking", true);
                ZombieSprite.flipX = true;
            }
            else if (HorInput > 0)
            {
                animator.SetBool("IsWalking", true);
                ZombieSprite.flipX = false;
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
            transform.Translate(HorInput, 0, 0);
        }

    }

    void Jump()
    {
        Zombie_Rigidbody.AddForce(transform.up * Impulse);
        IsOnFloor = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Enemy")
            animator.SetBool("IsDead", true);

        if (other.gameObject.name == "Goblin")
        {
            OtherAnimator = other.gameObject.GetComponent<Animator>();
            OtherAnimator.SetBool("CanAttackZombie", true);
        }

        if (other.gameObject.CompareTag("Floor"))
            IsOnFloor = true;


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
            IsOnFloor = false;
    }
}
