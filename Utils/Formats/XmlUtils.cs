// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using NetExtender.Utils.Types;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace NetExtender.Utils.Formats
{
    /// <summary>
    /// Extensions for XLinq.
    /// </summary>
    [PublicAPI]
    public static class XmlUtils
    {
        public static String IntendXml([NotNull] this XmlDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.ToXDocument().ToString();
        }
        
        public static XmlDocument FromIntendXml([NotNull] String xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return XDocument.Parse(xml).ToXmlDocument();
        }
        
        public static XmlDocument ToXmlDocument([NotNull] this XDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            XmlDocument xml = new XmlDocument();
            
            using XmlReader reader = document.CreateReader();
            xml.Load(reader);
            
            return xml;
        }

        public static XDocument ToXDocument([NotNull] this XmlDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            using XmlNodeReader reader = new XmlNodeReader(document);
            
            reader.MoveToContent();
            return XDocument.Load(reader);
        }

        public static XmlDocument Parse([NotNull] String xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            
            return document;
        }
        
        public static Boolean TryParse([NotNull] String xml, out XmlDocument document)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            document = new XmlDocument();

            try
            {
                document.LoadXml(xml);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static String ToXml([NotNull] String json)
        {
            return ToXml(json, null);
        }

        public static String ToXml([NotNull] String json, [CanBeNull] String root)
        {
            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonConvert.DeserializeXmlNode(json, root ?? "Root")?.IntendXml();
        }

        public static void SplitOnce(this String value, String separator, out String first, out String second)
        {
            if (value is not null)
            {
                Int32 idx = value.IndexOf(separator, StringComparison.InvariantCulture);
                if (idx >= 0)
                {
                    first = value.Substring(0, idx);
                    second = value.Substring(idx + separator.Length);
                }
                else
                {
                    first = value;
                    second = null;
                }
            }
            else
            {
                first = String.Empty;
                second = null;
            }
        }

        public static XmlNode CreateXPath([NotNull] this XmlDocument document, String xpath)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            XmlNode node = document;
            foreach (String part in xpath.Substring(1).Split('/'))
            {
                XmlNodeList nodes = node.SelectNodes(part);
                if (nodes.Count > 1)
                {
                    throw new XmlException("Xpath '" + xpath + "' was not found multiple times!");
                }

                if (nodes.Count == 1)
                {
                    node = nodes[0];
                    continue;
                }

                if (part.StartsWith("@"))
                {
                    XmlAttribute anode = document.CreateAttribute(part.Substring(1));
                    node.Attributes.Append(anode);
                    node = anode;
                }
                else
                {
                    String elName, attrib = null;
                    if (part.Contains("["))
                    {
                        part.SplitOnce("[", out elName, out attrib);
                        if (!attrib.EndsWith("]"))
                        {
                            throw new XmlException("Unsupported XPath (missing ]): " + part);
                        }

                        attrib = attrib.Substring(0, attrib.Length - 1);
                    }
                    else
                    {
                        elName = part;
                    }

                    XmlNode next = document.CreateElement(elName);
                    node.AppendChild(next);
                    node = next;

                    if (attrib is null)
                    {
                        continue;
                    }

                    if (!attrib.StartsWith("@"))
                    {
                        throw new XmlException("Unsupported XPath attrib (missing @): " + part);
                    }

                    attrib.Substring(1).SplitOnce("='", out String name, out String value);
                    if (String.IsNullOrEmpty(value) || !value.EndsWith("'"))
                    {
                        throw new XmlException("Unsupported XPath attrib: " + part);
                    }

                    value = value.Substring(0, value.Length - 1);
                    XmlAttribute anode = document.CreateAttribute(name);
                    anode.Value = value;
                    node.Attributes.Append(anode);
                }
            }

            return node;
        }

        public static void Set(this XmlDocument document, String xpath, String value)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (String.IsNullOrEmpty(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            XmlNodeList nodes = document.SelectNodes(xpath);

            if (nodes is null || nodes.Count == 0)
            {
                CreateXPath(document, xpath).InnerText = value;
                return;
            }

            if (nodes.Count > 1)
            {
                throw new XmlException("Xpath '" + xpath + "' was not found multiple times!");
            }
            
            XmlNode node = nodes[0];
            if (node is not null)
            {
                node.InnerText = value;
            }
        }
        
        /// <summary>
        /// Returns <paramref name="document"/> root, or throw an exception, if root is null.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Document root</returns>
        /// <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        /// <exception cref="XmlException">Document has no root.</exception>
        [NotNull]
        [Pure]
        public static XElement RequiredRoot([NotNull] this XDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (document.Root is null)
            {
                throw new XmlException("Document root is required");
            }

            return document.Root;
        }

        /// <summary>
        /// Returns <paramref name="document"/> root, or throws an exception, if root is null or has another name.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="root">Name of the root tag</param>
        /// <returns>Document root</returns>
        /// <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        /// <exception cref="XmlException">Document has no root with specified name.</exception>
        [NotNull]
        [Pure]
        public static XElement RequiredRoot([NotNull] this XDocument document, [NotNull] XName root)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            XElement element = document.RequiredRoot();
            if (element.Name != root)
            {
                throw new XmlException($"Document root '{root}' not found, '{element.Name}' found instead.");
            }

            return element;
        }

        /// <summary>
        /// Returns child element with name <paramref name="name"/>, or throws an exception if element does not exists.
        /// </summary>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Name of the element.</param>
        /// <returns>First element with specified name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parent"/> or <paramref name="name"/> is null.</exception>
        /// <exception cref="XmlException">Element with specified name does not exists.</exception>
        [NotNull]
        [Pure]
        public static XElement RequiredElement([NotNull] this XElement parent, [NotNull] XName name)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            XElement element = parent.Element(name);
            if (element is null)
            {
                throw new XmlException($"Element with name '{name}' does not exists.");
            }

            return element;
        }

        /// <summary>
        /// Returns child element with one of names in <paramref name="names"/>,
        /// or throws an exception if element does not exists.
        /// </summary>
        /// <param name="parent">Parent element.</param>
        /// <param name="names">Possible names of the element.</param>
        /// <returns>First element that match one of specified names.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="names"/> is null.
        /// </exception>
        /// <exception cref="XmlException">Element with one of specified names does not exists.</exception>
        [NotNull]
        [Pure]
        public static XElement RequiredElement([NotNull] this XElement parent, [NotNull, ItemNotNull] params XName[] names)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            HashSet<XName> namesSet = new HashSet<XName>(names);
            foreach (XElement element in parent.Elements())
            {
                if (namesSet.Contains(element.Name))
                {
                    return element;
                }
            }

            throw new XmlException($"Element with names {names.Join(", ")} not exists.");
        }

        /// <summary>
        /// Returns attribute with name <paramref name="name"/>, or throws an exception if attribute does not exists.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <returns>Attribute with specified name.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element"/> or <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="XmlException">Attribute with specified name not found.</exception>
        [NotNull]
        [Pure]
        public static XAttribute RequiredAttribute([NotNull] this XElement element, [NotNull] XName name)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            XAttribute attr = element.Attribute(name);
            if (attr is null)
            {
                throw new XmlException($"Element contains no attribute '{name}'");
            }

            return attr;
        }

        /// <summary>
        /// Returns value of optional attribute.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="element">Element with attribute</param>
        /// <param name="attribute">Attribute name.</param>
        /// <param name="parser">Value parser</param>
        /// <param name="default">Default value.</param>
        /// <returns>Parsed value or <paramref name="default"/> if attribute not exists.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element"/> or <paramref name="attribute"/> or <paramref name="parser"/> is null.
        /// </exception>
        [Pure]
        public static T AttributeValueOrDefault<T>([NotNull] this XElement element, [NotNull] XName attribute, [NotNull, InstantHandle] Func<String, T> parser, T @default)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (parser is null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            XAttribute attr = element.Attribute(attribute);
            return attr is not null ? parser(attr.Value) : @default;
        }

        /// <summary>
        /// Returns string value of optional attribute.
        /// </summary>
        /// <param name="element">Element with attribute</param>
        /// <param name="attribute">Attribute name.</param>
        /// <param name="default">Default value.</param>
        /// <returns>Parsed value or <paramref name="default"/> if attribute does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element"/> or <paramref name="attribute"/> is null.
        /// </exception>
        [Pure]
        public static String AttributeValueOrDefault([NotNull] this XElement element, [NotNull] XName attribute, String @default)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return element.Attribute(attribute)?.Value ?? @default;
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="selector">Function to select element value</param>
        /// <param name="default">Default value.</param>
        /// <param name="names">Array of possible element names.</param>
        /// <returns>Selected element value or <paramref name="default"/> if element does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="selector"/> or <paramref name="names"/> is null.
        /// </exception>
        [Pure]
        public static T ElementAltValueOrDefault<T>([NotNull] this XElement parent, [NotNull, InstantHandle] Func<XElement, T> selector, T @default, [NotNull, ItemNotNull] params XName[] names)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            XElement elem = names.Select(parent.Element).FirstOrDefault(e => e is not null);
            return elem is null ? @default : selector(elem);
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="selector">Function to select element value</param>
        /// <param name="default">Default value.</param>
        /// <returns>Selected element value or <paramref name="default"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="selector"/> is null.
        /// </exception>
        [Pure]
        public static T ElementValueOrDefault<T>([NotNull] this XElement parent, [NotNull] XName name, [NotNull, InstantHandle] Func<XElement, T> selector, T @default)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, selector, @default, name);
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="selector">Function to parse element value</param>
        /// <param name="default">Default value.</param>
        /// <returns>Selected element value or <paramref name="default"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="name"/> or <paramref name="selector"/> is null.
        /// </exception>
        [Pure]
        public static T ElementValueOrDefault<T>([NotNull] this XElement parent, [NotNull] XName name, [NotNull, InstantHandle] Func<String, T> selector, T @default)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, elem => selector(elem.Value), @default, name);
        }

        /// <summary>
        /// Returns string value of optional element.
        /// </summary>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="default">Default value.</param>
        /// <returns>Selected element value or <paramref name="default"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="name"/> is null.
        /// </exception>
        [Pure]
        [NotNull]
        public static String ElementValueOrDefault([NotNull] this XElement parent, [NotNull] XName name, String @default)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, e => e.Value, @default, name);
        }
        
        /// <summary>
        /// Converts an object to Xml
        /// </summary>
        /// <param name="temp">Object to convert</param>
        /// <param name="root"></param>
        /// <param name="path">File to save the Xml to</param>
        /// <returns>string representation of the object in Xml format</returns>
        public static String ObjectToXml(Object temp, String root, String path)
        {
            String xml = ObjectToXml(temp, root);
            File.AppendAllText(path, xml);
            return xml;
        }

        /// <summary>
        /// Converts an object to Xml
        /// </summary>
        /// <param name="temp">Object to convert</param>
        /// <param name="root"></param>
        /// <returns>string representation of the object in Xml format</returns>
        public static String ObjectToXml(Object temp, String root)
        {
            if (temp is null)
            {
                throw new ArgumentException("Object can not be null");
            }

            using MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(temp.GetType(), new XmlRootAttribute(root));
            serializer.Serialize(stream, temp);
            stream.Flush();
            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (Int32) stream.Position);
        }

        /// <summary>
        /// Takes an Xml file and exports the Object it holds
        /// </summary>
        /// <param name="path">File name to use</param>
        /// <param name="result">Object to export to</param>
        /// <param name="root"></param>
        public static void XmlToObject<T>(String path, out T result, String root)
        {
            using StreamReader reader = File.OpenText(path);
            result = XmlToObject<T>(reader.ReadToEnd(), root);
        }

        /// <summary>
        /// Converts an Xml string to an object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="xml">Xml string</param>
        /// <param name="root"></param>
        /// <returns>The object of the specified type</returns>
        public static T XmlToObject<T>([NotNull] String xml, String root)
        {
            if (String.IsNullOrEmpty(xml))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(xml));
            }

            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(root));
            return (T) serializer.Deserialize(stream);
        }

        /// <summary>
        /// Takes an Xml file and exports the Object it holds
        /// </summary>
        /// <param name="path">File name to use</param>
        /// <param name="result">Object to export to</param>
        /// <param name="type">Object type to export</param>
        /// <param name="root"></param>
        public static void XmlToObject([NotNull] String path, Type type, String root, out Object result)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using StreamReader reader = File.OpenText(path);
            result = XmlToObject(reader.ReadToEnd(), type, root);
        }

        /// <summary>
        /// Converts an Xml string to an object
        /// </summary>
        /// <param name="xml">Xml string</param>
        /// <param name="type">Object type to export</param>
        /// <param name="root"></param>
        /// <returns>The object of the specified type</returns>
        public static Object XmlToObject([NotNull] String xml, Type type, String root)
        {
            if (String.IsNullOrEmpty(xml))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(xml));
            }

            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlSerializer serializer = new XmlSerializer(type, new XmlRootAttribute(root));
            return serializer.Deserialize(stream);
        }
        
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static String SerializeXml<T>(T value)
        {
            if (value is null)
            {
                return null;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(value.GetType());
                using MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, value);
                stream.Position = 0;
                return Encoding.ASCII.GetString(stream.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Boolean TryDeserializeXml<T>(String obj, out T value)
        {
            if (String.IsNullOrEmpty(obj))
            {
                value = default;
                return false;
            }

            try
            {
                using StringReader read = new StringReader(obj);
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using XmlReader reader = new XmlTextReader(read);

                value = (T) serializer.Deserialize(reader);

                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }
    }
}