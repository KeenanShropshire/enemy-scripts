using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement script for animal enemies
public class EnemyMove : MonoBehaviour
{

    public Transform target;
    Rigidbody rb;
    public bool hit;
    public int force = 30;
    public int speed = 10;
    public bool radius = false;
    string type;
    public float obstacleRange = 50;
    public GameObject projectilePrefab;
    private GameObject _projectile;
    public AudioClip dinoSound;
    int count = 1;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hit = false;
        target = GameObject.Find("Player").transform; //"Player Variant"
        type = gameObject.tag;
        source = GetComponent<AudioSource>();

    }

     //Update is called once per frame
    void FixedUpdate()
    {


        if (hit == true)
        {
            rb.velocity = -transform.forward * force;
            hit = false;

        }


        switch (type)
        {
            case "Enemy":
                {
                    if (radius == true)
                    {

                        transform.LookAt(target);
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        //AudioSource.raptor_sound(dinoSound);
                        if (count == 1)
                        {
                            source.PlayOneShot(dinoSound);
                            count--;
                        }

                        if (hit == true)
                        {
                            rb.velocity = -transform.forward * force;
                            hit = false;
                        }
                    }
                    break;
                }

        }

    }


    public void AttackPlayer()
    {
        while (target.GetComponent<DetectionRadius>().DetectionArea)
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (hit == true)
            {
                rb.velocity = -transform.forward * force;
                hit = false;

            }
        }
        Debug.Log("Hi");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player Variant"  || collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
    }
}