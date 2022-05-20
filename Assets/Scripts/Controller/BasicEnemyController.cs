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

    // TODO: this is a bad solution
    private float attackTime = 1f; //s
    private float passedAttackTime = 0f; //s

    private Random random = new Random();
    void Start()
    {
        health.OnDied += OnDied;
    }

    void Awake()
    {
        health = GetComponent<Health>();
        shooting = GetComponent<Shooting>();
        aiMovement = GetComponent<AIMovement>();
        InvokeRepeating(nameof(ToggleAttack), 0, 1);
    }

    void ToggleAttack()
    {
        isAttacking = !isAttacking;
    }

    // Update is called once per frame
    private void Update() 
    {
        if (!isAttacking)
        {
            aiMovement.Move();
        }
        else if (isAttacking && passedAttackTime <= attackTime)
        {
            aiMovement.Shoot();
            passedAttackTime += Time.deltaTime;
            if (passedAttackTime >= attackTime)
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

        int val = random.Next(0, 100);
        if (val < 33)
        {
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