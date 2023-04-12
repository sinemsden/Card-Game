using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<Card> pooledObjects;
    public Card minionCardToPool;
    public Card spellCardToPool;
    public int amountToPool;

    void Awake()
    {
        pooledObjects = new List<Card>();
        for (int i = 0; i < amountToPool; i++)
        {
            MinionCard obj0 = (MinionCard)Instantiate(minionCardToPool);
            SpellCard obj1 = (SpellCard)Instantiate(spellCardToPool);
            obj0.transform.parent = this.transform;
            obj1.transform.parent = this.transform;
            pooledObjects.Add(obj0);
            pooledObjects.Add(obj1);
        }
    }

    void Start()
    {
        DisableObjects();
    }

    void DisableObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public Card GetMinionCard()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i] is MinionCard)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
    public Card GetSpellCard()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i] is SpellCard)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
