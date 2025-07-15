using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public EnemyManager enemyManager;

    [Header("Enemy Properties")]
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float attackDamage;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackSpeed; // Seconds between attacks
    [SerializeField] private float damageInterval = 1f; // Time between damage application to the tower

    private Tower towerRef;
    private Transform target; // Players Tower
    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Move towards the target tower
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;

        }
    }

    public void Initialize(Transform targetTower)
    {
        target = targetTower;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (enemyManager != null)
        {
            enemyManager.UnregisterEnemy(this);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            towerRef = collision.gameObject.GetComponent<Tower>();
            if (towerRef != null && damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamageToTower());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            towerRef = null;
        }
    }

    private IEnumerator ApplyDamageToTower()
    {
        while (towerRef != null)
        {
            towerRef.TakeDamage(attackDamage);
            yield return new WaitForSeconds(damageInterval);
        }
    }

        private void OnDestroy()
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }



}
