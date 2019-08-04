using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManipulator : MonoBehaviour
{
    public PlaneTextureScript plainA, plainB;
    public Material manipulatorMaterial;

    private MeshRenderer m_meshRenderer;
    private Material m_material;
    private RenderTexture m_renderTexture;
    // Start is called before the first frame update
    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_material = m_meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateTexture()
    {
        RenderTexture rtA = plainA.GetRenderTextureOnPlane();
        m_renderTexture = new RenderTexture(rtA.width, rtA.height, 0);
        manipulatorMaterial.SetTexture("_minuend", plainB.GetRenderTextureOnPlane());

        Graphics.Blit(rtA, m_renderTexture, manipulatorMaterial);//rta automatically goes into _MainTexture
        m_material.mainTexture = m_renderTexture;
    }
}
