using UnityEngine;

public class PCBulletShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;  // The bullet prefab to spawn.
    public Transform firePoint;      // The position and direction where bullets will be spawned.
    public float bulletSpeed = 20f;  // Speed at which the bullet travels.
    public float fireRate = 0.5f;    // Time between shots.

    [Header("Shooting FX")]
    public ParticleSystem muzzleFlash;  // Optional muzzle flash effect.
    public AudioClip shootSound;        // Optional shooting sound.
    private AudioSource audioSource;    // Audio source for playing the sound.

    private float nextFireTime = 0f;    // Time until the next shot is allowed.

    void Start()
    {
        // Initialize the audio source if a sound is assigned.
        if (shootSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = shootSound;
        }
    }

    void Update()
    {
        // Handle shooting input.
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Update the next allowed fire time.
        nextFireTime = Time.time + fireRate;

        // Spawn the bullet at the fire point.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Apply velocity to the bullet's Rigidbody.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Trigger muzzle flash effect if assigned.
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Play shooting sound if assigned.
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Destroy the bullet after a certain time to prevent memory leaks.
        Destroy(bullet, 5f);
    }
}
