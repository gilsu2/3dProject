using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻��� ��ź ������
    public Transform firePoint;          // ��ź�� �߻�� ��ġ (������ �Ա�)
    public float fireForce = 500f;       // ��ź �߻� ��
    public float fireInterval = 2f;      // �߻� ���� (1��)

    private void Start()
    {
        // 1�ʸ��� ��ź �߻�
        InvokeRepeating(nameof(FireProjectile), 0f, fireInterval);        
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile Prefab �Ǵ� Fire Point�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // 180�� ȸ�� �߰� (Y�� ����)
        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 180f, 0);

        // 180�� ȸ���� ��ź ����
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
        Destroy(projectile, 2f);

        // ��ź�� Rigidbody�� �ִ��� Ȯ�� �� force ����
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce);
        }
        else
        {
            Debug.LogError("Projectile Prefab�� Rigidbody�� �����ϴ�.");
        }
    }
}
