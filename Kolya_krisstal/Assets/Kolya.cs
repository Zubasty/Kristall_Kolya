using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Kolya : MonoBehaviour
{
    private Rigidbody2D dviz;
    private Animator anim;
    private SpriteRenderer Imag;
    [SerializeField] Camera cam;
    [SerializeField] Button F;
    [SerializeField] Slider Slid;
    [SerializeField] Text Lvl_Kolya_text;
    [SerializeField] Text Slid_text;
    [SerializeField] Text score_text;
    [SerializeField] Text lvl_text;
    [SerializeField] GameObject Panel_win;
    public float speed;
    public int N_Sled_lvl_Kolya;
    int score_monet = 0;
    int Lvl_Kolya = 0;
    int score = 0;
    int lvl = 1;
    int sled_lvl_score = 0;
    [SerializeField] GameObject krisstal_pref;
    [SerializeField] GameObject monetka_pref;
    [SerializeField] GameObject[] pol_pref;
    [SerializeField] Controller_save_json cntrl;
    [SerializeField] JSON_Nikolay jsKolya;
    public bool save_mode;

    private bool mozhn_podbirat;
    Collider2D obj;
    GameObject pol;
    public void Generic_new_lvl(List<Vector3> transform_monetok, List<Vector3> transform_krisstallov, Vector3 pol_pos, Vector3 Kolya_pos, int pol_lvl)
    {
        if(!save_mode)
        {
            foreach (Vector3 t in transform_monetok)
            {
                GameObject m = Instantiate(monetka_pref);
                m.transform.position = t;
            }
            foreach (Vector3 t in transform_krisstallov)
            {
                GameObject k = Instantiate(krisstal_pref);
                k.transform.position = t;
            }
            Destroy(pol);
            pol = Instantiate(pol_pref[pol_lvl - 1]);
            pol.transform.position = pol_pos;
            transform.position = Kolya_pos;
            sled_lvl_score += transform_monetok.Count + transform_krisstallov.Count;
            lvl_text.text = "Level " + lvl.ToString();
            cam.transform.position = new Vector2(transform.position.x, transform.position.y);
        }       
    }
    private void setting_Kolya_load()
    {
        jsKolya.load();
        Slid.maxValue = N_Sled_lvl_Kolya;
        Slid_text.text = Slid.value.ToString() + "/" + Slid.maxValue.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        dviz = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Imag = GetComponent<SpriteRenderer>();
        mozhn_podbirat = false;
        izm_lvl_Kolya();
        cam.transform.position = new Vector2(transform.position.x, transform.position.y);
        if(!save_mode)
        {
            setting_Kolya_load();
            cntrl.load(1);
        }
    }
    private void save()
    {
        if(save_mode)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                cntrl.save(1);
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                cntrl.save(2);
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                cntrl.save(3);
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                cntrl.save(4);
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                cntrl.save(5);
            }
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                cntrl.save(6);
            }
            else if (Input.GetKeyDown(KeyCode.F7))
            {
                cntrl.save(7);
            }
            else if (Input.GetKeyDown(KeyCode.F8))
            {
                cntrl.save(8);
            }
            else if (Input.GetKeyDown(KeyCode.F9))
            {
                cntrl.save(9);
            }
            else if (Input.GetKeyDown(KeyCode.F10))
            {
                cntrl.save(10);
            }
            else if (Input.GetKeyDown(KeyCode.F11))
            {
                cntrl.save(11);
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                cntrl.save(12);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                jsKolya.save();
                Slid.maxValue = N_Sled_lvl_Kolya;
                Slid_text.text = Slid.value.ToString() + "/" + Slid.maxValue.ToString();
            }
        }     
    }
    public void Win()
    {
        Panel_win.SetActive(true);
    }
    public void restart()
    {
        lvl = 0;
        score_monet = 0;
        score_text.text = score_monet.ToString();
        Lvl_Kolya = 0;
        izm_lvl_Kolya();
        score = 0;
        Slid.value = 0;
        sled_lvl_score = 0;
        Sled_lvl();
        Slid_text.text = Slid.value.ToString() + "/" + Slid.maxValue.ToString();
        Panel_win.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void izm_lvl_Kolya()
    {
        Lvl_Kolya++;
        Lvl_Kolya_text.text = "Lvl Kolya " + Lvl_Kolya.ToString();
    }
    private void Sled_lvl()
    {
        lvl++;
        cntrl.load(lvl);
    }
    // Update is called once per frame
    void Update()
    {
        dvizhenie();
        if(Input.GetKeyDown(KeyCode.F))
        {
            podbor();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Cam_size();
        }
        save();
    }
    public void Cam_size()
    {
        if(cam.orthographicSize==0.75f)
        {
            int k = Convert.ToInt32(pol.name[4].ToString());
            switch(k)
            {
                case 1:
                    cam.transform.position = new Vector2(pol.transform.position.x, pol.transform.position.y);
                    cam.orthographicSize = 2;
                    break;
                case 2:
                    cam.transform.position = new Vector2(pol.transform.position.x + 2, pol.transform.position.y - 2);
                    cam.orthographicSize = 4;
                    break;
                case 3:
                    cam.transform.position = new Vector2(pol.transform.position.x + 6, pol.transform.position.y - 6);
                    cam.orthographicSize = 8;
                    break;
            }
        }
        else
        {
            cam.orthographicSize = 0.75f;
            cam.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }
    private void dvizhenie()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            cam.orthographicSize = 0.75f;
            Vector2 napr = new Vector2();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                napr.y = 1;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                napr.y = -1;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                napr.x = 1;
                Imag.flipX = false;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                napr.x = -1;
                Imag.flipX = true;
            }
            dviz.velocity = napr;
            dviz.velocity = dviz.velocity.normalized * speed;
            anim.SetBool("Двигается", true);
            cam.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        else
        {
            dviz.velocity = new Vector2(0, 0);
            anim.SetBool("Двигается", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Image Fi = F.GetComponent<Image>();
        Fi.color = new Color(0, 255, 0);
        mozhn_podbirat = true;
        obj = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        no_object();
    }
    void no_object()
    {
        Image Fi = F.GetComponent<Image>();
        Fi.color = new Color(255, 255, 255);
        mozhn_podbirat = false;
        obj = null;
    }
    private void izm_znach_slid()
    {
        Slid.value++;
        if (Slid.value == Slid.maxValue)
        {
            Slid.value = 0;
            izm_lvl_Kolya();
        }
        Slid_text.text = Slid.value.ToString() + "/" + Slid.maxValue.ToString();
    }
    public void podbor()
    {
        if (mozhn_podbirat)
        {
            if(obj.name.IndexOf("Кристалл") == 0)
            {
                score++;
                izm_znach_slid();
                Destroy(obj.gameObject);
                no_object();
            }    
            else if (obj.name.IndexOf("Монетка") == 0)
            {
                score++;
                score_monet++;
                score_text.text = score_monet.ToString();
                Destroy(obj.gameObject);
                no_object();
            }
            if (score == sled_lvl_score)
            {
                Sled_lvl();
            }
        }
    }
}
