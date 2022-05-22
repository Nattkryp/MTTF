using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    int amount;
    int maxAmount;
    public List<Goods> goodsList = new List<Goods>();

    public Goods material {get;set;}


    private void Start()
    {
        Goods nuts = new Goods();
        Goods bolts = new Goods();
        Goods ducttape = new Goods();
        Goods zipties = new Goods();

        AddMaterial(nuts, 5);
        AddMaterial(bolts, 5);
        AddMaterial(ducttape, 5);
        AddMaterial(zipties, 5);

        foreach (Goods item in goodsList)
        {
            //Debug.Log(item.name.ToString());
        }
    }

    public void AddMaterial(Goods item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            goodsList.Add(item);
        }
    }
}

public class Goods
{
    public string name;
    public string description;
    public Sprite sprite;
}

