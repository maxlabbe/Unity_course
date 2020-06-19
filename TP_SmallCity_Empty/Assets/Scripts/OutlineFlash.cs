using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineFlash : MonoBehaviour
{

    public AnimationCurve outlineWidthCurve;
    public float flashEffectDuration = 0.3f;
    public Color flashEffectColor;
    Color originalColor;

    Renderer rend;
    void Start()
    {
        //Récupère le material de notre objet
        rend = GetComponentInChildren<Renderer>();
        //Récupère la couleur originale
        originalColor = rend.material.GetColor("_OutlineColor");
        //On pourrait récupérer l'epaisseur de l'outline ainsi
        float outlineWidth = rend.material.GetFloat("_Outline");

    }

    public void Flash()
    {

        rend.material.shader = Shader.Find("Transparent/Diffuse");
    }



}
