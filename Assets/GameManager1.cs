using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject bulletPrefab; // THÊM biến này để tránh lỗi
    public GameObject hitEffectPrefab; // THÊM biến này để tránh lỗi
    public Vector3 spawnPosition;  // Vị trí có thể nhập được từ Inspector

    void Start()
    {
        // Đảm bảo ObjectPooler đã khởi tạo trước khi sử dụng
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.CreatePool(bulletPrefab, 20); // Tạo Pool 20 viên đạn
            ObjectPooler.Instance.CreatePool(hitEffectPrefab, 10); // Tạo Pool 10 hiệu ứng va chạm
        }
        else
        {
            Debug.LogError("ObjectPooler Instance is null!");
        }

        StartCoroutine(TaoCube());
    }

    IEnumerator TaoCube()
    {
        for (int i = 0; i < 5; i++)
        {
            // Đợi 3 giây
            yield return new WaitForSeconds(3);
            GameObject cubeClone = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
