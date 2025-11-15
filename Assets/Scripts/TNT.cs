using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{

    public GameObject Effect;
    public List<Rigidbody> rigidbodies;

    public bool explode;

    [Space(5)]
    [Header("Properties")]
    public float timer = 5;
    public float force = 15;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (explode == true)
        {
            if (timer < 0)
            {
                explode = false;
                Effect.SetActive(false);
                for (int i = 0; i < rigidbodies.Count; i++)
                {

                    rigidbodies[i].AddForce((rigidbodies[i].transform.position - gameObject.transform.position) * force / rigidbodies[i].mass, ForceMode.VelocityChange);
                    print($"add force to {rigidbodies[i].name}: { force / rigidbodies[i].mass}");
                }

                Destroy(gameObject);
            }
            rigidbodies.Clear();
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
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
