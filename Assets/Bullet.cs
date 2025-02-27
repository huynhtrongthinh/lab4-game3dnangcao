using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public int damage = 20;
    public GameObject hitEffectPrefab;

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (target != null)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // Lấy hiệu ứng va chạm từ Pool
        if (hitEffectPrefab != null)
        {
            GameObject effectInstance = ObjectPooler.Instance.GetPooledObject(hitEffectPrefab, transform.position, Quaternion.identity);
            effectInstance.SetActive(true);

            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                ObjectPooler.Instance.ReturnToPool(effectInstance, ps.main.duration);
            }
            else
            {
                ObjectPooler.Instance.ReturnToPool(effectInstance, 2f);
            }
        }

        ObjectPooler.Instance.ReturnToPool(gameObject);
    }

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("DisableBullet", 5f); // Nếu không va chạm, tự động trả về Pool sau 5 giây
    }

    void DisableBullet()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject);
    }
}
