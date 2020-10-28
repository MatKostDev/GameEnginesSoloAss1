using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    public int numInitialBullets;

    Queue<GameObject> m_bulletQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        GameObject myBullet;

        if (IsPoolEmpty())
        {
            myBullet = SpawnBullet(true);
        }
        else
        {
            myBullet = m_bulletQueue.Dequeue();
            myBullet.SetActive(true);
        }

        return myBullet;
    }
    
    public void ResetBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        m_bulletQueue.Enqueue(bullet);
    }

    public bool IsPoolEmpty()
    {
        return m_bulletQueue.Count <= 0;
    }

    public int GetPoolSize()
    {
        return m_bulletQueue.Count;
    }

    GameObject SpawnBullet(bool a_activate)
    {
        GameObject newBullet = Instantiate(bullet, transform);
        newBullet.GetComponent<BulletController>().bulletPool = this;

        newBullet.SetActive(a_activate);

        return newBullet;
    }

    void BuildBulletPool()
    {
        for (int i = 0; i < numInitialBullets; i++)
        {
            m_bulletQueue.Enqueue(SpawnBullet(false));
        }
    }
}
