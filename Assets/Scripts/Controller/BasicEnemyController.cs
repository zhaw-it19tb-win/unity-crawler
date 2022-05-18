using Components;
using UnityEngine;
using Random = System.Random;
using PotionType = Item.PotionType;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(AIMovement))]
public class BasicEnemyController : MonoBehaviour
{
    private Health health;
    private Shooting shooting;
    private AIMovement aiMovement;

    private bool isAttacking;

    private bool attackAnimationFinished = false;

    private float attackIntervall; 
    private float passedAttackTime = 0f; //s

    void Start()
    {
        health.OnDied += OnDied;
    }

    void Awake()
    {
        attackIntervall = UnityEngine.Random.Range(0.5f, 1.0f); //s defines how fast the archer attackes to make it less likely for the bullets to colide
        health = GetComponent<Health>();
        shooting = GetComponent<Shooting>();
        aiMovement = GetComponent<AIMovement>();
        Debug.Log("attackIntervall" + attackIntervall); 
        InvokeRepeating(nameof(ToggleAttack), 0, attackIntervall);
    }

    private int counter = 0;

    void ToggleAttack()
    {
        isAttacking = !isAttacking;
        counter++; // heitmtim: temporary
    }

    // Update is called once per frame
    private void Update()
    {
        if (counter % 10 == 0)
        {
            OnDied(); // heitmtim: temporary
        }

        if (!isAttacking)
        {
            aiMovement.Move();
        }
        else if (isAttacking && passedAttackTime <= attackIntervall)
        {
            aiMovement.Shoot();
            passedAttackTime += Time.deltaTime;
            if (passedAttackTime >= attackIntervall)
            {
                passedAttackTime = 0f;
                shooting.Shoot();
            }
        }
    }

    void FixedUpdate()
    {
    }

    private void OnDied()
    {
        Destroy(this.gameObject);

        Random random = new System.Random();
        int val = random.Next(0, 100);
        if (val < 100)
        {
            // change this number for probability of a potion 2 spawn
            PotionType potionType = (PotionType)random.Next(3);
            SpawnPotion(potionType);
        }
    }

    private void SpawnPotion(PotionType potionType)
    {
        var gameObject = new GameObject();
        gameObject.transform.position = transform.position;
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // dont move order of initialization, dependencies between setting the sprite and sorting layer.
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.name = "Potion: " + potionType.ToString();

        var sprite = Resources.Load<Sprite>(GetSpritePathByPotionType(potionType));
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName = "Player";
        spriteRenderer.sortingOrder = -1;

        var boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(1, 1);

        var item = gameObject.AddComponent<Item>();
        item.potionType = potionType;
    }

    private string GetSpritePathByPotionType(PotionType potionType)
    {
        return potionType switch
        {
            PotionType.Health => "pot1red",
            PotionType.Strength => "pot1green",
            _ => "pot1blue",
        };
    }
}