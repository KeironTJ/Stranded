using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; 

    [Header("Bullet Properties")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackDamage;

    private Vector2 moveDirection;


    // Call this when firing the bullet, passing the target's position at that moment
    public void SetDirection(Vector2 targetPosition)
    {
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * bulletSpeed;
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void SetDamage(float damage)
    {
        attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
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
