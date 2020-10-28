using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    public int numInitialBullets;

    Queue<GameObject> m_bulletQueue = new Queue<GameObject>();

    private static BulletPoolManager s_instance = null;

    public static BulletPoolManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(BulletPoolManager)) as BulletPoolManager;

                if (!s_instance)
                {
                    throw new NullReferenceException("YOU MUST CREATE A BULLET POOL MANAGER IN THE HIERARCHY!");
                }
            }
            return s_instance;
        }
    }

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
