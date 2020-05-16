﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShapeGenerator 
{
    PlanetShapeSettings settings;
    NoiseFilter[] noiseFilters;
    public PlanetShapeGenerator(PlanetShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new NoiseFilter[this.settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = 0;
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            elevation += noiseFilters[i].Evaluate(pointOnUnitSphere); 
        }
        
        return pointOnUnitSphere * settings.planetRadius * (1+elevation);
    }

    public PhysicMaterial GetMaterial()
    {
        return settings.material;
    }
}