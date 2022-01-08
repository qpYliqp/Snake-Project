using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public GameObject FoodHolder;
    public Transform FoodPrefab;

    public Transform SpawnZone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SupprAllApple()
    {
        for (int i = 0; i < FoodHolder.transform.childCount; i++)
        {
            Destroy(FoodHolder.transform.GetChild(i).gameObject);
        }
    }

    public void AddApple()
    {
        Transform segInv = Instantiate(this.FoodPrefab);
        float x = SpawnZone.position.x - SpawnZone.lossyScale.x / 2 + Random.Range(0.15f, SpawnZone.lossyScale.x - 0.15f);
        float y = SpawnZone.position.y - SpawnZone.lossyScale.y / 2 + Random.Range(0.15f, SpawnZone.lossyScale.y - 0.15f);

        segInv.position = new Vector3(x, y, 0);
        segInv.parent = FoodHolder.transform;
    }
}
