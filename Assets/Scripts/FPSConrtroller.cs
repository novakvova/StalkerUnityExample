using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSConrtroller : MonoBehaviour
{
    public InvertoryManager manager;
    public GrenadeController grenadeController;

    public float radius = 1.5f;


    public Text press;
    public Camera _camera;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);


            foreach (var i in hitColliders)
            {
                Vector3 direction = i.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > .5f)
                    i.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }







        RaycastHit hit; // Посилання,на змінну,якак буде містити інформацію про об'єкт з яким зіткнувся промінь
        press.text = "";
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 4f)) //Перевірка
        {
            if (hit.transform.CompareTag("weapon"))
            {
                if (hit.transform.GetComponent<AbstractObject>() != null)
                    if (hit.transform.GetComponent<AbstractObject>().Equiped)
                        return;


                press.text = "Press F";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    AbstractObject a = hit.transform.GetComponent<AbstractObject>();
                    if (manager.AddGun(a))
                    {
                        hit.transform.gameObject.active = false;
                    }
                }
            }
            else if (hit.transform.CompareTag("grenade"))
            {
                press.text = "Press F";
                if (Input.GetKeyDown(KeyCode.F))
                {                   
                    if (grenadeController.AddGrenade())
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }

}
