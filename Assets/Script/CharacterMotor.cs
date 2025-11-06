using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMotor : MonoBehaviour
{
    //viens du script PlayerInventory
    PlayerInventory playerInv;

    //animation du personnage
    Animation animations;

    //vitesse de déplacement
    [Header("Mouvement Settings")]
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    //attaque du personnage
    [Header("Attack Settings")]
    public float attackColdown;
    private float currentColdown;
    private bool isAttacking;
    public float attackRange;
    private GameObject rayHit;

    //Inputs
    [Header("Input Settings")]
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    //si le joueur perd toutes sa vie 
    public bool isDead = false;

    // Spell
    [Header("Spell Settings")]
    public GameObject raySpell;
    private GameObject spellHolderImg;
    public int currentSpell = 1;
    public int totalSpell;

    // Lightning Spell
    [Header("Lightning Spell")]
    public GameObject lightningSpellGO;
    public float lightningSpellCost;
    public float lightningSpellSpeed;
    public int ligthningSpellID;
    public Sprite ligthninigSpellImage;

    // Heal Spell
    [Header("Heal Spell")]
    public GameObject healSpellGO;
    public float healSpellCost;
    public float healSpellAmount;
    public int healSpellID;
    public Sprite healSpellImage;




    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        rayHit = GameObject.Find("RayHit");
        playerInv = gameObject.GetComponent<PlayerInventory>();
        spellHolderImg = GameObject.Find("SpellHolderImg");
    }

    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, layerMask:3);
    }
// Update is called once per frame
    void Update()
    {
        //pour la mort du joueur
        if (!isDead)
        {
            //avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);

                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Spell();
                }
            }

            //courir
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");
            }

            // reculer
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);

                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Spell();
                }
            }

            // aller à gauche
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }

            // aller à droite 
            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }

            // au repos
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                if (!isAttacking)
                {
                    animations.Play("idle");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Spell();
                }
            }

            // pour sauter 
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                // preparation  du saut 
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                // saut 
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            }
        }

        // Système de cooldown 
        if (isAttacking)
        {
            currentColdown -= Time.deltaTime;
        }

        if (currentColdown <= 0)
        {
            currentColdown = attackColdown;
            isAttacking = false;
        }

        // Changement de sort avec la molette de la souris
        // Arrière
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentSpell <= totalSpell &&  currentSpell != 1)
            {
                currentSpell--;
            }
        }

        // Avant
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentSpell >= 0 && currentSpell != totalSpell)
            {
                currentSpell++;
            }
        }

        // Changement d'image en fonction du sort selectionne
        if(currentSpell == ligthningSpellID)
        {
            spellHolderImg.GetComponent<Image>().sprite = ligthninigSpellImage;
        }

        if(currentSpell == healSpellID)
        {
            spellHolderImg.GetComponent<Image>().sprite = healSpellImage;
        }
    }

    //fonction de l'attaque 
    public void Attack()
    {
        if (!isAttacking)
        {
            animations.Play("attack");

            RaycastHit hit;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                if(hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyAi>().ApplyDamage(playerInv.currentDamage);
                }
            }
            isAttacking = true;
        }
    }
    //fonction de sort 
    public void Spell()
    {
        // Sort de foudre 
        if (currentSpell == ligthningSpellID && !isAttacking && playerInv.currentMana >= lightningSpellCost)
        {
            animations.Play("attack");
            GameObject spell = Instantiate(lightningSpellGO, raySpell.transform.position, transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce(transform.forward * lightningSpellSpeed);
            playerInv.currentMana -= lightningSpellCost;
            isAttacking = true;
        }

        // Sort de soin
        if (currentSpell == healSpellID && !isAttacking && playerInv.currentMana >= healSpellCost && playerInv.currentHealth < playerInv.maxHealth)
        {
            animations.Play("attack");
            Instantiate(healSpellGO, raySpell.transform.position, transform.rotation);
            playerInv.currentMana -= healSpellCost;
            playerInv.currentHealth += healSpellAmount;
            isAttacking = true;
        }
    }
}
