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
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Utilities.Serialization
{
    /// <summary>
    /// Extensions for XLinq.
    /// </summary>
    public static class XmlUtilities
    {
        public static String IntendXml(this XmlDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.ToXDocument().ToString();
        }

        public static XmlDocument FromIntendXml(String xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return XDocument.Parse(xml).ToXmlDocument();
        }

        public static XmlDocument ToXmlDocument(this XDocument document)
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

        public static XDocument ToXDocument(this XmlDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            using XmlNodeReader reader = new XmlNodeReader(document);
            reader.MoveToContent();

            return XDocument.Load(reader);
        }

        public static XmlDocument Parse(String xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            return document;
        }

        public static Boolean TryParse(String xml, out XmlDocument document)
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

        public static String? ToXml(String json)
        {
            return ToXml(json, null);
        }

        public static String? ToXml(String json, String? root)
        {
            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonConvert.DeserializeXmlNode(json, root ?? "Root")?.IntendXml();
        }

        public static void SplitOnce(this String? value, String separator, out String? first, out String? second)
        {
            if (separator is null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            if (value is null)
            {
                first = String.Empty;
                second = null;
                return;
            }

            Int32 index = value.IndexOf(separator, StringComparison.InvariantCulture);
            if (index >= 0)
            {
                first = value.Substring(0, index);
                second = value.Substring(index + separator.Length);
                return;
            }

            first = value;
            second = null;
        }

        /// <summary>
        /// Returns <paramref name="document"/> root, or throw an exception, if root is null.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Document root</returns>
        /// <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        /// <exception cref="XmlException">Document has no root.</exception>
        public static XElement RequiredRoot(this XDocument document)
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
        public static XElement RequiredRoot(this XDocument document, XName? root)
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
        public static XElement RequiredElement(this XElement parent, XName name)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return parent.Element(name) ?? throw new XmlException($"Element with name '{name}' does not exists.");
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
        public static XElement RequiredElement(this XElement parent, params XName[] names)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            HashSet<XName> set = new HashSet<XName>(names);

            foreach (XElement element in parent.Elements())
            {
                if (set.Contains(element.Name))
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
        public static XAttribute RequiredAttribute(this XElement element, XName name)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return element.Attribute(name) ?? throw new XmlException($"Element contains no attribute '{name}'");
        }

        /// <summary>
        /// Returns value of optional attribute.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="element">Element with attribute</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="parser">Value parser</param>
        /// <param name="alternate">Default value.</param>
        /// <returns>Parsed value or <paramref name="alternate"/> if attribute not exists.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element"/> or <paramref name="name"/> or <paramref name="parser"/> is null.
        /// </exception>
        public static T? AttributeValueOrDefault<T>(this XElement element, XName name, Func<String, T> parser, T? alternate)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parser is null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            XAttribute? attribute = element.Attribute(name);
            return attribute is not null ? parser(attribute.Value) : alternate;
        }

        /// <summary>
        /// Returns string value of optional attribute.
        /// </summary>
        /// <param name="element">Element with attribute</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="alternate">Default value.</param>
        /// <returns>Parsed value or <paramref name="alternate"/> if attribute does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element"/> or <paramref name="name"/> is null.
        /// </exception>
        public static String? AttributeValueOrDefault(this XElement element, XName name, String? alternate)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return element.Attribute(name)?.Value ?? alternate;
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="selector">Function to select element value</param>
        /// <param name="alternate">Default value.</param>
        /// <param name="names">Array of possible element names.</param>
        /// <returns>Selected element value or <paramref name="alternate"/> if element does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="selector"/> or <paramref name="names"/> is null.
        /// </exception>
        public static T? ElementAltValueOrDefault<T>(this XElement parent, Func<XElement, T> selector, T? alternate, params XName[] names)
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

            XElement? element = names.Select(parent.Element).FirstOrDefault(item => item is not null);
            return element is not null ? selector(element) : alternate;
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="selector">Function to select element value</param>
        /// <param name="alternate">Default value.</param>
        /// <returns>Selected element value or <paramref name="alternate"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static T? ElementValueOrDefault<T>(this XElement parent, XName name, Func<XElement, T> selector, T? alternate)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, selector, alternate, name);
        }

        /// <summary>
        /// Returns value of optional element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="selector">Function to parse element value</param>
        /// <param name="alternate">Default value.</param>
        /// <returns>Selected element value or <paramref name="alternate"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="name"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static T? ElementValueOrDefault<T>(this XElement parent, XName name, Func<String, T> selector, T? alternate)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, element => selector(element.Value), alternate, name);
        }

        /// <summary>
        /// Returns string value of optional element.
        /// </summary>
        /// <param name="parent">Parent element.</param>
        /// <param name="name">Element name.</param>
        /// <param name="alternate">Default value.</param>
        /// <returns>Selected element value or <paramref name="alternate"/> if element does not exist</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent"/> or <paramref name="name"/> is null.
        /// </exception>
        public static String? ElementValueOrDefault(this XElement parent, XName name, String? alternate)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ElementAltValueOrDefault(parent, element => element.Value, alternate, name);
        }

        /// <summary>
        /// Converts an object to Xml
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <param name="root"></param>
        /// <param name="path">File to save the Xml to</param>
        /// <returns>string representation of the object in Xml format</returns>
        public static String ObjectToXml(Object value, String root, String path)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            String xml = ObjectToXml(value, root);
            File.AppendAllText(path, xml);
            return xml;
        }

        /// <summary>
        /// Converts an object to Xml
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <param name="root"></param>
        /// <returns>string representation of the object in Xml format</returns>
        public static String ObjectToXml(Object value, String root)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using MemoryStream stream = new MemoryStream();

            XmlSerializer serializer = new XmlSerializer(value.GetType(), new XmlRootAttribute(root));
            serializer.Serialize(stream, value);
            stream.Flush();

            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (Int32) stream.Position);
        }

        /// <summary>
        /// Takes an Xml file and exports the Object it holds
        /// </summary>
        /// <param name="path">File name to use</param>
        /// <param name="result">Object to export to</param>
        /// <param name="root"></param>
        public static void XmlToObject<T>(String path, String root, out T? result)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        public static T? XmlToObject<T>(String xml, String root)
        {
            if (String.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullOrEmptyStringException(xml, nameof(xml));
            }

            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(root));

            return (T?) serializer.Deserialize(stream);
        }

        /// <summary>
        /// Takes an Xml file and exports the Object it holds
        /// </summary>
        /// <param name="path">File name to use</param>
        /// <param name="result">Object to export to</param>
        /// <param name="type">Object type to export</param>
        /// <param name="root"></param>
        public static void XmlToObject(String path, Type type, String root, out Object? result)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
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
        public static Object? XmlToObject(String xml, Type type, String root)
        {
            if (String.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullOrEmptyStringException(xml, nameof(xml));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
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
        public static String? SerializeXml<T>(T? value)
        {
            if (value is null)
            {
                return null;
            }

            try
            {
                using MemoryStream stream = new MemoryStream();

                XmlSerializer serializer = new XmlSerializer(value.GetType());
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
        public static Boolean TryDeserializeXml<T>(String? value, out T? result)
        {
            if (String.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            try
            {
                Type type = typeof(T);
                XmlSerializer serializer = new XmlSerializer(type);

                using StringReader reader = new StringReader(value);
                using XmlReader xml = new XmlTextReader(reader);

                result = (T?) serializer.Deserialize(xml);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}