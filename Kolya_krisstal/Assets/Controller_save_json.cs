using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Controller_save_json : MonoBehaviour
{
    public List<Vector3> transform_monetok = new List<Vector3>();
    public List<Vector3> transform_krisstallov = new List<Vector3>();
    public Vector3 Kolya_pos;
    public Vector3 pol_pos;
    public int pol_lvl;
    string data;
    public void load(int n)
    {
        Kolya kol = FindObjectOfType<Kolya>();
        if (File.Exists("/save_lvl_" + n + ".txt"))
        {
            data = File.ReadAllText("/save_lvl_" + n + ".txt");
            JsonUtility.FromJsonOverwrite(data, this);
            kol.Generic_new_lvl(transform_monetok, transform_krisstallov, pol_pos, Kolya_pos, pol_lvl);
        }
        else
        {
            kol.Win();
        }
    }
    public void save(int n)
    {
        transform_monetok.Clear();
        transform_krisstallov.Clear();

        BoxCollider2D[] objs = FindObjectsOfType<BoxCollider2D>();
        foreach (BoxCollider2D b in objs)
        {
            if (b.name.IndexOf("Кристалл")==0)
            {
                transform_krisstallov.Add(b.GetComponent<Transform>().position);
            }
            else if (b.name.IndexOf("Монетка")==0)
            {
                transform_monetok.Add(b.GetComponent<Transform>().position);
            }
        }
        Kolya k = FindObjectOfType<Kolya>();
        if(!k)
        {
            Debug.LogError("Забыли Колю");
        }
        Kolya_pos = k.transform.position;
        GameObject[] pols = FindObjectsOfType<GameObject>();
        foreach(GameObject pol in pols)
        {
            if(pol.name.IndexOf("пол_")==0)
            {
                pol_lvl = Convert.ToInt32(pol.name[4].ToString());
                pol_pos=pol.transform.position;
            }    
        }
        int c = 0;
        c = 10 + (pol_lvl - 1) * 20;      
        if(transform_krisstallov.Count>0 && transform_monetok.Count>0 && transform_monetok.Count+transform_krisstallov.Count<=c)
        {
            data = JsonUtility.ToJson(this, true);
            File.WriteAllText("/save_lvl_" + n + ".txt", data);
        }
        else
        {
            Debug.LogError("Напортачили с количеством объектов");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}