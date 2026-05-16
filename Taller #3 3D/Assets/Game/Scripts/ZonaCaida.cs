using UnityEngine;

public class ZonaCaida : MonoBehaviour
{
    public Transform puntoOrigen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = puntoOrigen.position;
        }
    }
}