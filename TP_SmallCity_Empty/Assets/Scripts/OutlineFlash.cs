using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineFlash : MonoBehaviour
{

    public AnimationCurve outlineWidthCurve;
    public float flashEffectDuration = 0.3f;
    public Color flashEffectColor;
    Color originalColor;
    Shader carShader;

    Renderer rend;
    void Start()
    {
        //Récupère le material de notre objet
        rend = GetComponentInChildren<Renderer>();
        //Récupère la couleur originale
        originalColor = rend.material.GetColor("_OutlineColor");
        //On pourrait récupérer l'epaisseur de l'outline ainsi
        float outlineWidth = rend.material.GetFloat("_Outline");
        carShader = rend.material.shader;
        rend.material.shader = Shader.Find("Transparent/Diffuse");

    }

    public void Flash()
    {

        StartCoroutine(carFlash());
    }

    IEnumerator carFlash()
    {
        rend.material.shader = carShader;
        yield return new WaitForSeconds(flashEffectDuration);
        rend.material.shader = Shader.Find("Transparent/Diffuse");

    }


}
