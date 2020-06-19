using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnHighSpeedCollision : MonoBehaviour {

    public LayerMask targetLayers;

    // particule qui vont pop
    public GameObject particlePrefab;
    
    // longevité de la particule
    public float effectLifeTime = 2;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {

        // On récupère la rotation voulue pour le particules
        Quaternion particlesRotateVector  = Quaternion.Euler(new Vector3(-90, 180, 0));

        // On fait commencer les particules un peut plus haut (de +1 en y)
        Vector3 particulesTranslateVector = transform.position;
        int hight = 1;
        particulesTranslateVector.y = particulesTranslateVector.y + hight;

        // Si l'objet en collision est du bon layer
        if(IsLayerInLayerMask(collision.gameObject.layer, targetLayers))
        {
            // on créer un game object avec les particules
            GameObject particleEffect = Instantiate(particlePrefab, particulesTranslateVector, particlesRotateVector, null);
            particleEffect.GetComponent<ParticleSystem>().Play();

            //planifier la déstruction de l'effet de particule
            Destroy(particleEffect, effectLifeTime);
        }
    }

    /// <summary>
    /// Returns true if the given layer is contained in the mask, false otherwise.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    bool IsLayerInLayerMask(int layer, LayerMask mask)
    {
        //Une opériation qui compare l'index du layer avec un masque (ex 10010011, les 1 étant les layers contenus dans le masque).
        return mask == (mask | (1 << layer));
    }
}
