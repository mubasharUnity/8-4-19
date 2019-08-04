
using System.Xml;
using UnityEngine;

public class XmlReader : MonoBehaviour
{
    public static XmlReader instance;

    private XmlElement m_xmlElement;

    private const string xmlFileName = "info";
    void Awake()
    {
        instance = this;

        TextAsset xmlText = Resources.Load<TextAsset>(xmlFileName);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlText.text);

        m_xmlElement = xmlDocument.DocumentElement;
    }

    //return value of eleemt passed by name. returns null if no element of such type
    public bool TryGetElementValue(string name, out string value)
    {
        var element = m_xmlElement[name];
        if(element == null)
        {
            value = null;
            return false;
        }
        else {
            value = element.InnerText;
            return true;
        }
    }
}