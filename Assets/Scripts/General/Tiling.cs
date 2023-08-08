using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    public int diferenciaX = 2;
    public bool tieneDerecha = false;
    public bool tieneIzquierda = false;
    public bool escalaInversa = false;
    private float anchoSprite = 0f;
    public Camera camara;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        anchoSprite = spriteRenderer.sprite.bounds.size.x;
    }


    void Update()
    {
        if (!tieneIzquierda || !tieneDerecha)
        {
            float extensionCamaraHorizontal = camara.orthographicSize * Screen.width / Screen.height;
            float visibilidadDerechaBorde = (transform.position.x + anchoSprite / 2) - extensionCamaraHorizontal;
            float visibilidadIzquierdaBorde = (transform.position.x - anchoSprite / 2) + extensionCamaraHorizontal;
            if (camara.transform.position.x >= visibilidadDerechaBorde - diferenciaX && !tieneDerecha)
            {
                CrearCompañero(1);
                tieneDerecha = true;
            }
            else if (camara.transform.position.x <= visibilidadIzquierdaBorde + diferenciaX && !tieneIzquierda)
            {
                CrearCompañero(-1);
                tieneIzquierda = true;
            }
        }
    }

    void CrearCompañero(int derechaOIzquierda)
    {
        Vector3 nuevaPosicion = new Vector3(transform.position.x + anchoSprite * derechaOIzquierda, transform.position.y, transform.position.z);
        Transform nuevoCompañero = Instantiate(transform, nuevaPosicion, transform.rotation) as Transform;

        if (escalaInversa)
        {
            nuevoCompañero.localScale = new Vector3(nuevoCompañero.localScale.x * -1, nuevoCompañero.localScale.y, nuevoCompañero.localScale.z);
        }
        nuevoCompañero.parent = transform;
        if (derechaOIzquierda > 0)
        {
            nuevoCompañero.GetComponent<Tiling>().tieneIzquierda = true;
        }
        else
        {
            nuevoCompañero.GetComponent<Tiling>().tieneDerecha = true;
        }
    }
}
