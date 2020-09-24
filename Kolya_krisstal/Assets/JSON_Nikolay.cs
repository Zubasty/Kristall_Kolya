using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON_Nikolay : MonoBehaviour
{
    public float speed;
    public int N_Sled_lvl_Kolya;
    string data;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void save()
    {
        Kolya k = FindObjectOfType<Kolya>();
        if (!k)
        {
            Debug.LogError("Забыли Колю");
        }
        speed = k.speed;
        N_Sled_lvl_Kolya = k.N_Sled_lvl_Kolya;
        data = JsonUtility.ToJson(this, true);
        File.WriteAllText("/save_setting_Kolya.txt", data);
    }

    public void load()
    {
        data = File.ReadAllText("/save_setting_Kolya.txt");
        JsonUtility.FromJsonOverwrite(data, this);
        Kolya k = FindObjectOfType<Kolya>();
        k.speed = speed;
        k.N_Sled_lvl_Kolya = N_Sled_lvl_Kolya;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
