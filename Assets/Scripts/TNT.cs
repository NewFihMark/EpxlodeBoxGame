using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public float timer = 5;
    public float force = 100;

    public GameObject Effect;
    public List<Rigidbody> rigidbodies;

    public bool explode;

    void Start()
    {
        Effect.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            explode = true;
            Effect.SetActive(false);
            for(int i = 0; i < rigidbodies.Count; i++)
            {

                rigidbodies[i].AddForce((rigidbodies[i].transform.position-gameObject.transform.position)*force/rigidbodies[i].mass, ForceMode.VelocityChange);
                print($"add force to {rigidbodies[i].name}: { force / rigidbodies[i].mass}");
            }

            Destroy(gameObject);
        }
        rigidbodies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null)
        rigidbodies.Add(other.GetComponent<Rigidbody>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
            rigidbodies.Add(other.GetComponent<Rigidbody>());
    }

}
