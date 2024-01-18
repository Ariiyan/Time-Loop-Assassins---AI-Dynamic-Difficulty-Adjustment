using UnityEngine;
using TMPro;

public class ShootingScript : MonoBehaviour
{
    public Camera fpsCamera;

    public float range = 100f;

    public int maxAmmo = 10;

    public int damage = 10; 


    public GameObject muzzleFlashPrefab;
    public AudioClip shootingSound;


    public TextMeshProUGUI ammoText;

    private AudioSource audioSource;

    private int currentAmmo;
    public Transform muzzlePosition;


    void Start()
    {
        if (fpsCamera == null)
            fpsCamera = Camera.main;


        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();


        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoUI();
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
            return; 

        currentAmmo--;
        UpdateAmmoUI();

        if (shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }

        
        if (muzzleFlashPrefab != null)
        {
            GameObject flashInstance = Instantiate(muzzleFlashPrefab, muzzlePosition.position, muzzlePosition.rotation);

            flashInstance.transform.localScale = new Vector3(1f, 1f, 1f); 

            Destroy(flashInstance, 0.1f); 
        }

        RaycastHit hit;
        Vector3 rayOrigin = fpsCamera.transform.position;
        Vector3 rayDirection = fpsCamera.transform.forward;

      
        Debug.DrawRay(rayOrigin, rayDirection * range, Color.red, 1.0f);


        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {

                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); 
                }
            }
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }
}
