using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGB : MonoBehaviour {

    FauxGA planet;
    new Rigidbody rigidbody;

    // Use this for initialization
    void Awake () {
        planet = FindClosestPlanet().GetComponent<FauxGA>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        planet.Attract(rigidbody);
    }
    //funkcija za iskanje najbljižjega planeta
    public GameObject FindClosestPlanet()
    {
        GameObject[] Planets;
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        GameObject NearestPlanet = null;
        float Razdalja = Mathf.Infinity;
        Vector3 pozicija = transform.position;
        foreach (GameObject Pl in Planets)
        {
            Vector3 razlika = Pl.transform.position - pozicija;
            float trenutnarazdalja = razlika.sqrMagnitude;
            if (trenutnarazdalja < Razdalja)
            {
                NearestPlanet = Pl;
                Razdalja = trenutnarazdalja;
            }
        }
        return NearestPlanet;
    }
}
