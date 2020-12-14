using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public Enemy enemy;

    public LayerMask whatIsPlayer;
    public Rigidbody2D bullet;

    private Animator animator;

    [Header ("Droppables")]
    public GameObject bitCoin;
    public GameObject healthDrop;

    private float attackCounter;

    [HideInInspector]
    public int currentHealth;

    [HideInInspector]
    public RoomManager _roomManager;

    [Header ("Movement")]
    public float pursuitSpeed;
    public float wanderSpeed;
    private float currentSpeed;
    public float directionChangeInterval;
    private Rigidbody2D _rb2d;
    private Vector3 endPosition;
    private Transform targetTransform = null;
    private float currentAngle = 0;
    private Coroutine moveCoroutine;
    private bool shooting = false;
    private bool exploding = false;

    // Start is called before the first frame update
    void Start () {
        GetComponent<SpriteRenderer> ().sprite = enemy.sprite;

        attackCounter = enemy.attackSpeed;
        currentHealth = enemy.maxHealth;

        _roomManager = transform.parent.GetComponent<RoomManager> ();

        animator = GetComponent<Animator>();

        if (enemy.enemyType == Enemy.ENEMY_TYPE.WANDERING) {
            _rb2d = GetComponent<Rigidbody2D> ();
            currentSpeed = wanderSpeed;
        }
    }

    // Update is called once per frame
    void Update () {
        Attack ();
        Death ();
    }

    public void GetDamaged (int damage) {
        currentHealth -= damage;
    }

    private void Attack () {
        switch (enemy.enemyType) {
            case Enemy.ENEMY_TYPE.STATIONARY:
                if (attackCounter <= 0) {
                    Collider2D hit = Physics2D.OverlapCircle (transform.position, enemy.detectRange, whatIsPlayer);
                    if (hit != null) {
                        Rigidbody2D bulletClone = (Rigidbody2D) Instantiate (bullet, transform.position, transform.rotation);
                        bulletClone.gameObject.tag = "Enemy";
                        bulletClone.GetComponent<Bullet> ().damage = enemy.damage;
                        Vector2 direction = new Vector2 (hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y);
                        bulletClone.velocity = direction * enemy.bulletSpeed * Time.deltaTime;

                        attackCounter = enemy.attackSpeed;
                    }
                } else {
                    attackCounter -= Time.deltaTime;
                }

                break;
            case Enemy.ENEMY_TYPE.WANDERING:
                WanderingEnemy();
                break;
            case Enemy.ENEMY_TYPE.EXPLODING:
                ExplodingEnemy();
                break;
        }
    }

    private void Death () {
        if (currentHealth <= 0) {
            DropItem ();
            _roomManager.populatedEnemies.Remove (gameObject);
            Destroy (gameObject);
        }
    }

    private void DropItem () {
        Vector3 itemSpawnPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        float chanceToDrop = Random.Range (0f, 1f);
        if (chanceToDrop >= 0.5f) {
            int whichItem = Random.Range (0, 2);
            switch (whichItem) {
                case 0:
                    //Drop bitCoins
                    int numOfCoins = Random.Range (1, 9);
                    for (int i = 0; i < numOfCoins; i++) {
                        Instantiate (bitCoin, itemSpawnPoint, transform.rotation);
                    }
                    break;
                case 1:
                    //Drop Health
                    Instantiate (healthDrop, itemSpawnPoint, transform.rotation);
                    break;
            }
        }
    }

    private void WanderingEnemy () {
        StartCoroutine (WanderRoutine ());
        Collider2D hit = Physics2D.OverlapCircle (transform.position, enemy.detectRange, whatIsPlayer);
        if (hit != null) {
            //Player detected
            currentSpeed = pursuitSpeed;
            targetTransform = hit.gameObject.transform;
            if (moveCoroutine != null) {
                StopCoroutine (moveCoroutine);
            }
            moveCoroutine = StartCoroutine (Move (_rb2d, currentSpeed, true));
        } else {
            animator.SetBool("isMoving", false);
            currentSpeed = wanderSpeed;
            if (moveCoroutine != null) {
                StopCoroutine (moveCoroutine);
                StopCoroutine (Shoot ());
            }
            targetTransform = null;
        }
    }

    private void ExplodingEnemy()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.detectRange, whatIsPlayer);
        if(hit != null)
        {
            if(!exploding)
            {
                animator.SetBool("exploding", true);
                StartCoroutine(Explode());
            }
        }
    }

    private IEnumerator Explode()
    {
        exploding = true;
        yield return new WaitForSeconds(3.4f);
        Collider2D explosionHitBox = transform.GetChild(0).GetComponent<Collider2D>();
        explosionHitBox.gameObject.SetActive(true);
        exploding = false;
        StartCoroutine(DealDamage());
    }
    private IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(0.8f);
        _roomManager.populatedEnemies.Remove (gameObject);
        Destroy(gameObject);
    }

    private IEnumerator WanderRoutine () {
        while (true) {
            ChooseNewEndpoint ();
            if (moveCoroutine != null) {
                StopCoroutine (moveCoroutine);
                StopCoroutine (Shoot ());
            }
            moveCoroutine = StartCoroutine (Move (_rb2d, currentSpeed, false));

            yield return new WaitForSeconds (directionChangeInterval);
        }
    }

    private IEnumerator Move (Rigidbody2D rb2dToMove, float speed, bool targetAquired) {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        while (remainingDistance > float.Epsilon) {
            if (targetTransform != null) {
                endPosition = targetTransform.position;
                if (!shooting && targetAquired) {
                    StartCoroutine (Shoot ());
                }
            }
            if (rb2dToMove) {
                animator.SetBool("isMoving", true);
                Vector3 newPosition = Vector3.MoveTowards(rb2dToMove.position, endPosition, speed * Time.deltaTime);
                _rb2d.MovePosition(newPosition);
                animator.SetFloat("xMov", _rb2d.velocity.x);
                animator.SetFloat("yMov", _rb2d.velocity.y);

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate ();
        }
        animator.SetBool("isMoving", false);
    }

    private IEnumerator Shoot () {
        shooting = true;
        while (true) {
            shooting = true;
            if (targetTransform != null) {
                Rigidbody2D bulletClone = (Rigidbody2D) Instantiate (bullet, transform.position, transform.rotation);
                bulletClone.gameObject.tag = "Enemy";
                bulletClone.GetComponent<Bullet> ().damage = enemy.damage;
                Vector2 direction = new Vector2 (targetTransform.position.x - transform.position.x, targetTransform.position.y - transform.position.y);
                bulletClone.velocity = direction * enemy.bulletSpeed * Time.deltaTime;
            }

            yield return new WaitForSeconds (attackCounter);
            shooting = false;
        }
    }

    private void ChooseNewEndpoint () {
        currentAngle += Random.Range (0, 360);
        currentAngle = Mathf.Repeat (currentAngle, 360);

        endPosition += Vector3FromAngle (currentAngle);
    }

    private Vector3 Vector3FromAngle (float inputAngleDegrees) {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3 (Mathf.Cos (inputAngleRadians), Mathf.Sin (inputAngleRadians), 0);
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, enemy.detectRange);
    }
}