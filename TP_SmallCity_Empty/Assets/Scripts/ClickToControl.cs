using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToControl : MonoBehaviour
{

    private Boolean played = false;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        //partie 2, detecter un clique droit
        //provenant de la souris
        if (Input.GetMouseButton(0))
        {
            Debug.Log("the mouse Right clicked");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.gameObject.layer);

                if (hit.transform.gameObject.layer == 8)
                {
                    Debug.Log("is is a vehicle!");

                    GameObject car = GameObject.Find(hit.transform.name);
                    Debug.Log(car.name);
                    SelectCar(car.GetComponent<CarController>());
                    //SelectCar(car);
                    StartCoroutine(startEngine());
                    

                }



            }




        }

    }

    //Contient la voiture séléctionnée actuelement. C'est un variable static, elle existe donc dans la classe et non dans instance d'objet de la classe.
    //Cela signifie que cette variable est partagée entre tous les carControllers.
    public static CarController currentlySelectedCar;

    public void SelectCar(CarController car)
    {
        //Si une voiture était séléctionnée, désactive son carController
        if (currentlySelectedCar != null)
            currentlySelectedCar.enabled = false;


        //Séléctionne la nouvelle voiture
        currentlySelectedCar = car;
        //Active le carController pour rendre la voiture mobile
        currentlySelectedCar.carSpeed = 5;
        currentlySelectedCar.carTurningSpeed = 30;
        currentlySelectedCar.enabled = true;
        //Envoi un message pour appeler une eventuelle fonction Flash qui serait sur un composant de notre gameObject.
        currentlySelectedCar.SendMessage("Flash", SendMessageOptions.DontRequireReceiver);
    }

    public AudioClip engineStartSound;
    IEnumerator startEngine()
    {
        if(!played)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(engineStartSound);
            played = true;
            yield return new WaitForSeconds(engineStartSound.length);
            played = false;
        }

        

    }
}