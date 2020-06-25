using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineFlash : MonoBehaviour
{

    public AnimationCurve outlineWidthCurve;
    public float flashEffectDuration = 0.3f;
    float outlineWidth = 0f;
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
        outlineWidth = rend.material.GetFloat("_Outline");
        carShader = rend.material.shader;

    }

    public void Flash()
    {

        StartCoroutine(carFlash());
    }

    IEnumerator carFlash()
    {
        //rend.material.shader = Shader.Find("Diffuse/Transparent");
        Color color = new Color(255, 255, 255, 0);
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Outline", 0.005f);
        yield return new WaitForSeconds(flashEffectDuration);
        rend.material.SetColor("_OutlineColor", originalColor);
        rend.material.SetFloat("_Outline", outlineWidth);

    }


}
