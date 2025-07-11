using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Bullet Properties")]
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private float bulletSpeed; // Speed of the bullet

    [Header("Tower Properties")]
    [SerializeField] public float health;
    [SerializeField] public float attackDamage;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackSpeed; // Bullets per second
    [SerializeField] private float rotationSpeed; // Speed of turret rotation

    private Transform target;

    private float shootTimer = 1f; // Timer to control shooting frequency

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Your Tower has Spawned!");
    }

    // Update is called once per frame
    void Update()
    {
        // Handle tower logic here, such as attacking enemies within range
        DrawTargetingRange();
        ActivateShooting();
    }

    // SHOOTING METHODS
    public void ActivateShooting()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= 1f / attackSpeed)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    private void Shoot()
    {
        if (target == null) return;

        GameObject bulletObject = Instantiate(bulletPrefab, turretRotationPoint.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetTarget(target);
        bullet.SetSpeed(bulletSpeed); // Set bullet speed
        bullet.SetDamage(attackDamage); // Set attack damage

        //Debug.Log("Shot fired at: " + target.name);
    }

    // TARGETTING METHODS
    private void DrawTargetingRange()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = 50;
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = false;
        }

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.cyan;
        lineRenderer.endColor = Color.cyan;

        float angle = 20f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * attackRange;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * attackRange;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / lineRenderer.positionCount;
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0f, enemyMask);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(hit.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit.transform;
            }
        }

        if (closestTarget != null && closestDistance <= attackRange)
        {
            target = closestTarget;
        }
        else
        {
            target = null;
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        return distanceToTarget <= attackRange;
    }

    // Damage handling
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
        // Handle tower death (e.g., play animation, drop loot, etc.)
        Debug.Log("Tower destroyed!");
        Destroy(gameObject);

        //CREATE LOGIC TO END ROUND HERE
    }
}
