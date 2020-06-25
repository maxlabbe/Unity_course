using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float carSpeed=5f;
    public float carTurningSpeed=100f;
    Boolean forward = false;
    Boolean backward = false;
    Boolean klaxonIsPlaying = false;
    public AudioClip klaxon;

    Vector2 inputs;
    new Rigidbody rigidbody;

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().volume = 0.1f;
        
    }

    // Use this for initialization
    void Awake () {
        //Récupère le rigibody
        rigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        ReadInputs();

        RotateCar();

        StartCoroutine(SoundFunction());
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //On applique l'accèlération dans fixed update car les forces physiques continues doivent appliquées dans fixedUpdate pour se comporter correctement.
        ApplyAcceleration();
    }

    void ReadInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            forward = true;

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            backward = true;
        }

    }

    IEnumerator SoundFunction()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!klaxonIsPlaying)
            {
                klaxonIsPlaying = true;
                gameObject.GetComponent<AudioSource>().PlayOneShot(klaxon);
                float klaxonLength = klaxon.length;
                yield return new WaitForSeconds(klaxonLength);
                klaxonIsPlaying = false;
            }
            
        }
    }

    void RotateCar()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, -1f * carTurningSpeed, 0f) * Time.deltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * rotation);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 1f * carTurningSpeed, 0f) * Time.deltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * rotation);
        }
    }

    void ApplyAcceleration()
    {
        if (forward)
        {
            rigidbody.AddForce(transform.forward * carSpeed, ForceMode.Impulse);
            forward = false;
        }

        if(backward)
        {
            rigidbody.AddForce(transform.forward * carSpeed * -1, ForceMode.Impulse);
            backward = false;
        }
        
    }
}
