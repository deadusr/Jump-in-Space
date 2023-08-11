using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetName {
    Moon,
    Mars,
    Neptune
}
public class PlanetController : MonoBehaviour {
    public static PlanetController Instance { get; private set; }

    void Awake() {
        Instance = this;
    }

    List<Planet> planets = new List<Planet>();

    public List<Planet> Planets => planets;

    public void AddPlanet(Planet planet) {
        if (planets.Contains(planet))
            return;
        planets.Add(planet);
    }
}