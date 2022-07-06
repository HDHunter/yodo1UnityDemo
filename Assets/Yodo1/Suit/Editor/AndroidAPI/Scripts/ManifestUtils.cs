using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;

public class ManifestUtils : Editor
{

	private static string TAG_MANIFEST = "manifest";
	private static string TAG_APPLICATION = "application";
	private static string TAG_SUPPORTS_SCREENS = "supports-screens";
	private static string TAG_USERS_SDK = "uses-sdk";

	private static string TAG_USERS_PERMISSION = "uses-permission";
	private static string TAG_USERS_FEATURE = "uses-feature";
	private static string TAG_META_DATA = "meta-data";
	private static string TAG_ACTIVITY = "activity";

	public static void SetManifestAttribute (string filePath, string attributeName, string value)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_MANIFEST, attributeName, value, null, null);
	}

	public static void SetApplicationAttribute (string filePath, string attributeName, string value)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_APPLICATION, attributeName, value, null, null);
	}

	public static void SetSupportsScreensAttribute (string filePath, string attributeName, string value)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_SUPPORTS_SCREENS, attributeName, value, null, null);
	}

	public static void SetUsersSdkAttribute (string filePath, string attributeName, string value)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_MANIFEST, attributeName, value, null, null);
	}

	public static void SetUsersPermissionAttribute (string filePath, string attributeName, string attributeValue, string modifyAttributeName, string modifyAttributeValue)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_MANIFEST, attributeName, attributeValue, modifyAttributeName, modifyAttributeValue);
	}

	public static void SetUsersFeatureAttribute (string filePath, string attributeName, string attributeValue, string modifyAttributeName, string modifyAttributeValue)
	{
		SetChildNode (filePath, TAG_MANIFEST, TAG_MANIFEST, attributeName, attributeValue, modifyAttributeName, modifyAttributeValue);
	}

	public static void SetMetaDataAttribute (string filePath, string attributeName, string attributeValue, string modifyAttributeName, string modifyAttributeValue)
	{
		SetChildNode (filePath, TAG_APPLICATION, TAG_META_DATA, attributeName, attributeValue, modifyAttributeName, modifyAttributeValue);
	}

	public static void SetActivityAttribute (string filePath, string attributeName, string attributeValue, string modifyAttributeName, string modifyAttributeValue)
	{
		SetChildNode (filePath, TAG_APPLICATION, TAG_ACTIVITY, attributeName, attributeValue, modifyAttributeName, modifyAttributeValue);
	}

	private static void SetChildNode (string filePath, string parentName, string tagName, string attributeName, string attributeValue, string modifyAttributeName, string modifyAttributeValue)
	{
		XmlDocument doc = new XmlDocument ();
		doc.Load (filePath);

		if (doc == null) {
			Debug.LogError ("Couldn't load " + filePath);
			return;
		}

		XmlNode manNode = FindChildNode (doc, "manifest");
		XmlNode appNode = FindChildNode (manNode, "application");
		string ns = manNode.GetNamespaceOfPrefix ("android");

		if (manNode == null) {
			Debug.LogError ("Error parsing " + filePath + ",tag for manifest not found.");
			return;
		}

		XmlNode node = null;

		if (TAG_APPLICATION.Equals (tagName) ||
		    TAG_SUPPORTS_SCREENS.Equals (tagName) ||
		    TAG_USERS_SDK.Equals (tagName)) {
			node = FindChildNode (manNode, tagName);
		} else if (TAG_MANIFEST.Equals (tagName)) {
			node = manNode;
		} else if (TAG_USERS_PERMISSION.Equals (tagName) ||
		           TAG_USERS_FEATURE.Equals (tagName) ||
		           TAG_META_DATA.Equals (tagName) ||
		           TAG_ACTIVITY.Equals (tagName)) {
			XmlNode parentNode = manNode;
			if (TAG_APPLICATION.Equals (parentName)) {
				parentNode = appNode;
			}
			node = FindChildNodeWithAttribute (parentNode, tagName, attributeName, attributeValue);
			if (node == null) {
				node = (XmlElement)doc.CreateNode (XmlNodeType.Element, tagName, null);
				if (parentNode != null) {
					parentNode.AppendChild (node);
				}
			}
		}

		if (node == null) {
			Debug.LogError ("Error parsing " + filePath + ",tag for " + tagName + " not found.");
			return;
		}
		XmlElement elem = (XmlElement)node;
		if (!string.IsNullOrEmpty (attributeName)) {
            //elem.SetAttribute (attributeName, ns, attributeValue);

            //修复Unity Scripting Runtime Version 在.NET 4.x Equivalent环境下报“The ':' character, hexadecimal value 0x3A, cannot be included in a name”的错bug
            XmlAttribute attribute = doc.CreateAttribute(attributeName, ns);
            attribute.Value = attributeValue;
            elem.Attributes.Append(attribute);
            //
        }
		if (!string.IsNullOrEmpty (modifyAttributeName)) {
            //elem.SetAttribute (modifyAttributeName, ns, modifyAttributeValue);

            //修复Unity Scripting Runtime Version 在.NET 4.x Equivalent环境下报“The ':' character, hexadecimal value 0x3A, cannot be included in a name”的错bug
            XmlAttribute attribute = doc.CreateAttribute(modifyAttributeName, ns);
            attribute.Value = modifyAttributeValue;
            elem.Attributes.Append(attribute);
            //end
        }
		doc.Save (filePath);
    }

	private static XmlNode FindChildNode (XmlNode parent, string tagName)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null) {
			if (curr.Name.Equals (tagName)) {
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static XmlNode FindChildNodeWithAttribute (XmlNode parent, string tagName, string attribute, string value)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null) {
			if (curr.Name.Equals (tagName) && curr.Attributes [attribute].Value.Equals (value)) {
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static void SetChildNodeWithAttribute (string filePath, string tagName, string attributeName, string value)
	{
		XmlDocument doc = new XmlDocument ();
		doc.Load (filePath);

		if (doc == null) {
			Debug.LogError ("Couldn't load " + filePath);
			return;
		}

		XmlNode manNode = FindChildNode (doc, "manifest");
		string ns = manNode.GetNamespaceOfPrefix ("android");

		if (manNode == null) {
			Debug.LogError ("Error parsing " + filePath + ",tag for manifest not found.");
			return;
		}

		XmlNode node = FindChildNodeWithAttribute (manNode, tagName, attributeName, value);

		if (node == null) {
			Debug.LogError ("Error parsing " + filePath + ",tag for " + tagName + " not found.");
			return;
		}

		XmlElement elem = (XmlElement)node;
        //elem.SetAttribute (attributeName, ns, value);

        //修复Unity Scripting Runtime Version 在.NET 4.x Equivalent环境下报“The ':' character, hexadecimal value 0x3A, cannot be included in a name”的错bug
        XmlAttribute attribute = doc.CreateAttribute(attributeName, ns);
        attribute.Value = value;
        elem.Attributes.Append(attribute);
        //end

        doc.Save (filePath);
	}

}
