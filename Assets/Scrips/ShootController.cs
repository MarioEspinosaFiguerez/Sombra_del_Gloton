using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootController;

    public void Shoot(bool lookRight)
    {
        shootController.localScale = new Vector3(lookRight ? 1 : -1, 1, 1);
        GameObject bullet = Instantiate(bulletPrefab, shootController.position, Quaternion.identity);

        if (bullet is not null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript is not null)
            {
                bulletScript.SetDirection(lookRight);
            }
        }
    }
}