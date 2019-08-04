using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTextureScript : MonoBehaviour
{
    public string attachedTextureElement;

    private MeshRenderer m_meshRenderer;
    private Material m_material;
    private RenderTexture m_renderTexture;

    // Start is called before the first frame update
    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_material = m_meshRenderer.material;

        m_material.mainTexture = GetRenderTexture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RenderTexture GetRenderTexture()
    {
        string resourceName = null;
        if(XmlReader.instance.TryGetElementValue(attachedTextureElement, out resourceName))
        {
            Texture texture = Resources.Load<Texture>(resourceName);
            m_renderTexture = new RenderTexture(texture.width, texture.height, 0);
            Graphics.Blit(texture, m_renderTexture);//copy texture to render texture
            return m_renderTexture;
        }
        return null;
    }

    public RenderTexture GetRenderTextureOnPlane() {
        return m_renderTexture;
    }
}
