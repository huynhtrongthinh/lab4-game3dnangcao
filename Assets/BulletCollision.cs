using UnityEngine;
using UnityEngine.VFX;

public class BulletCollision : MonoBehaviour
{
    public VisualEffect explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
