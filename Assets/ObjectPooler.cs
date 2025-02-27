using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Singleton để truy cập từ mọi nơi

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    // Khởi tạo một Pool với số lượng mặc định
    public void CreatePool(GameObject prefab, int size)
    {
        string key = prefab.name;
        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                poolDictionary[key].Enqueue(obj);
            }
        }
    }

    // Lấy một GameObject từ Pool
    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string key = prefab.name;

        if (poolDictionary.ContainsKey(key) && poolDictionary[key].Count > 0)
        {
            GameObject obj = poolDictionary[key].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        // Nếu Pool hết, tạo mới
        GameObject newObj = Instantiate(prefab, position, rotation);
        return newObj;
    }

    // Trả GameObject về Pool
    public void ReturnToPool(GameObject obj, float delay = 0f)
    {
        string key = obj.name.Replace("(Clone)", "").Trim();

        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
        }

        if (delay > 0)
        {
            StartCoroutine(ReturnAfterDelay(obj, delay));
        }
        else
        {
            obj.SetActive(false);
            poolDictionary[key].Enqueue(obj);
        }
    }

    private System.Collections.IEnumerator ReturnAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        string key = obj.name.Replace("(Clone)", "").Trim();
        poolDictionary[key].Enqueue(obj);
    }
}
