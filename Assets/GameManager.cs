using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector3 spawnPosition;  // V? trí có th? nh?p ???c t? Inspector

    void Start()
    {
        StartCoroutine(TaoCube());
    }

    IEnumerator TaoCube()
    {
        for (int i = 0; i < 5; i++)
        {
            // ??i 3 giây
            yield return new WaitForSeconds(3);
            GameObject cubeClone = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
    }
}
