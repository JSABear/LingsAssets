using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraDraw : MonoBehaviour
{
    public Camera cam;
    public LayerMask bridgeLayer;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public bool CheckRay(Vector3 testPosition, bool bridge)
    {

        int layerMask = ~(1 << LayerMask.NameToLayer("Bridge"));

        RaycastHit hit;

        if (bridge)
        {
            if (!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(testPosition)), out hit, float.MaxValue, layerMask))
            {
                Debug.Log(!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(testPosition)), out hit, float.MaxValue, layerMask));
                Debug.Log(layerMask);
                return false;
            }
        }
        else
        {
            if (!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(testPosition)), out hit))
            {
                return false;
            }
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshcollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshcollider == null)
        {
            return false;
        }
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PaintRay(Vector3 location, int radius)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(location)), out hit))
        {
            return false;
        }

        if ((bridgeLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
        {
            // The hit object is on the "bridge" layer, don't paint
            return false;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshcollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshcollider == null)
        {
            return false;
        }

        Texture2D originalTex = rend.material.mainTexture as Texture2D;
        Texture2D copiedTex = new Texture2D(originalTex.width, originalTex.height);
        Color[] pixels = originalTex.GetPixels();
        copiedTex.SetPixels(pixels);

        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= copiedTex.width;
        pixelUV.y *= copiedTex.height;

        if (copiedTex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            Rectangle(copiedTex, (int)pixelUV.x, (int)pixelUV.y, 100, 120, Color.clear);

            copiedTex.Apply();

            rend.material.mainTexture = copiedTex;
            return true;
        }
        else
        {
            return false;
        }
    }




    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);
            }
        }
    }

    public void Rectangle(Texture2D tex, int x, int y, int width, int height, Color col)
    {
        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                tex.SetPixel(i, j, col);
            }
        }
    }
}
