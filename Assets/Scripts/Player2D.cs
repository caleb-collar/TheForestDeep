using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player2D : MonoBehaviour
{
    // Move player in 2D space
    [SerializeField] public float maxSpeed = 3.4f;
    [SerializeField] public float jumpHeight = 6.5f;
    [SerializeField] public float gravityScale = 1.5f;
    [SerializeField] public float att1Cooldown = -0.5f;
    [SerializeField] public float att2Cooldown = -2f;
    [SerializeField] private AudioClip orbSound;
    [SerializeField] private AudioClip[] stepSounds, slashSounds, stabSounds, hitTerrain, hitTarget, jumpSounds, landSounds;
    [SerializeField] private SceneLoader loader;
    private AudioSource georgeAudio;
    private GameObject orb;
    private Light2D orbLight;
    private SpriteRenderer orbRenderer;
    private bool orbState;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    //float currVelocity = 0.0f;
    Vector3 lockPos;
    float att1Delta, att2Delta;
    Rigidbody2D r2d;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animations;
    Transform t;

    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        animations = GetComponentInChildren<Animator>();
        animations.enabled = true;
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        att1Delta = att1Cooldown;
        att2Delta = att2Cooldown;
        orb = GameObject.Find("MagicOrb");
        orbLight = orb.GetComponent(typeof(Light2D)) as Light2D;
        orbRenderer = orb.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        georgeAudio = GetComponent(typeof(AudioSource)) as AudioSource;
    }

    // Update is called once per frame
    void Update()
    {
        //Animations
        if (isGrounded == false)
        {
            animations.SetBool("isJumping", true);
            animations.SetBool("isRunning", false);
        }
        else if (moveDirection != 0)
        {
            animations.SetBool("isJumping", false);
            animations.SetBool("isRunning", true);
            if (!georgeAudio.isPlaying)
            {
                georgeAudio.clip = stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)];
                georgeAudio.Play();
            }
        }
        else
        {
            animations.SetBool("isJumping", false);
            animations.SetBool("isRunning", false);
            animations.SetBool("isAttacking_1", false);
            animations.SetBool("isAttacking_2", false);
        }
        
        
        
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }
        
        //Apply Attack Cooldown Counter
        att1Delta += Time.deltaTime;
        att2Delta += Time.deltaTime;
        
        //Attack Controls
        if (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f)
        {
            //Light Attack
            if(Input.GetKey(KeyCode.R) && att1Delta >= 0)
            {
                animations.SetBool("isJumping", false);
                animations.SetBool("isRunning", false);
                animations.SetBool("isAttacking_1", true);
                animations.SetBool("isAttacking_2", false);
                georgeAudio.PlayOneShot(slashSounds[UnityEngine.Random.Range(0, slashSounds.Length)], 0.9f);
                att1Delta = att1Cooldown;
            }
            //Heavy Attack
            if (Input.GetKey(KeyCode.F) && att2Delta >= 0)
            {
                animations.SetBool("isJumping", false);
                animations.SetBool("isRunning", false);
                animations.SetBool("isAttacking_1", false);
                animations.SetBool("isAttacking_2", true);
                georgeAudio.PlayOneShot(stabSounds[UnityEngine.Random.Range(0, stabSounds.Length)], 0.9f);
                att2Delta = att2Cooldown;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                lockPos = transform.position;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                transform.position = lockPos;
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                lockPos = transform.position;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                transform.position = lockPos;
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            georgeAudio.PlayOneShot(jumpSounds[UnityEngine.Random.Range(0, jumpSounds.Length)], 0.9f);
        }
        
        // Toggle light
        if (Input.GetKeyDown(KeyCode.Q))
        {
            orbLight.enabled = !orbLight.isActiveAndEnabled;
            orbRenderer.enabled = orbLight.isActiveAndEnabled;
            georgeAudio.PlayOneShot(orbSound, 0.9f);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            loader.LoadGameOver();
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = feetCollider.bounds;
        float colliderRadius = feetCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != feetCollider && colliders[i] != bodyCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }
}