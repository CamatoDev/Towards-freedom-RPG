using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //distance entre le joueur et l'ennemi 
    private float Distance;
    
    //distance entre l'ennemi et sa position de base  
    private float DistanceBase;
    private Vector3 basePositions;

    //cible 
    private Transform Target;

    //distance à laquelle la poursuite s'active 
    public float chaseRange = 10;

    //ditance à laquelle on attaque 
    public float attackRange = 3f;

    //porté des attques 
    public float attackRepeatTime = 1;
    private float attackTime;

    //puissance des dégats
    public float TheDamage;

    //agent de nav
    private UnityEngine.AI.NavMeshAgent agent;

    //animation 
    private Animator animations;

    //pour que le joueur l'attaque 
    public float enemyHealth;
    public bool isDeath = false;

    //Loot de l'ennemi 
    public GameObject[] loots;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        //Lors du lancement on stocke la position actuel de l'ennemi
        basePositions = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //on cherche le joueur tout le temps 
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        //si il est vivant 
        if (!isDeath)
        {
            //On calcule distance entre le joueur et l'ennemi pour les différentes actions à effectuer 
            Distance = Vector3.Distance(Target.position, transform.position);

            //On calcule distance entre l'ennemi et la position de base 
            DistanceBase = Vector3.Distance(basePositions, transform.position);

            // Quand le jouer est trop loin : repos 
            if (Distance > chaseRange && DistanceBase <= 1)
            {
                Idle();
            }

            // Quand le joueur est detecté à la distance de chasse 
            if (Distance < chaseRange && Distance > attackRange)
            {
                Chase();
            }

            // Quand le joueur est à la porté d'attaque 
            if (Distance < attackRange)
            {
                Attack();
            }

            // Quand le joueur s'est échappé
            if(Distance > chaseRange && DistanceBase > 1 || Target.GetComponent<CharacterMotor>().isDead)
            {
                BackBase();
            }
        }
    }

    //pour la chase 
    void Chase()
    {
        animations.SetFloat("Walk", 1.0f);
        agent.destination = Target.position;
    }

    //pour l'attaque 
    void Attack()
    {
        //ne pas entrer dans le joueur 
        agent.destination = transform.position;

        //pas de cooldown
        if (Time.time > attackTime && Target.GetComponent<PlayerInventory>().currentHealth > 0)
        {
            animations.SetFloat("Walk", 0.0f);
            animations.SetTrigger("Hit");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDamage);
            Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    //pour le repos 
    void Idle()
    {
        animations.SetFloat("Walk", 0.0f);
    }

    public void ApplyDamage(float TheDamage)
    {
        if (!isDeath)
        {
            animations.SetTrigger("Damage");
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + " à subit" + TheDamage + " points de dégâts");

            if(enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    //Fonction pour retourner à la base  
    public void BackBase()
    {
        animations.SetFloat("Walk", 1.0f);
        agent.destination = basePositions;
    }

    //Fonction qui définit la mort de l'ennemi
    public void Dead()
    {
        animations.SetFloat("Walk", 0.0f);
        //Désactivation du collider de l'ennemi
        gameObject.GetComponent<SphereCollider>().enabled = false;

        isDeath = true;
        animations.SetTrigger("Die");

        //Génération d'un loot aléatoire parmis les loots possible de l'ennemi 
        int randomLootIndex = Random.Range(0, loots.Length);
        GameObject finalLoot = loots[randomLootIndex];  
        Instantiate(finalLoot, transform.position, transform.rotation);

        Destroy(gameObject, 2f);
    }
}
