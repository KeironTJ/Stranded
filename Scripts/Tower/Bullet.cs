using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; 

    [Header("Bullet Properties")]
    [SerializeField] private Transform target;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackDamage;

    public void SetTarget(Transform _target) 
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject); // Optionally destroy the bullet if its target is gone
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void SetDamage(float damage)
    {
        attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage); // Pass the bullet's damage value
            Destroy(gameObject); // Destroy the bullet after hitting
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
