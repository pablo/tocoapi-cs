using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace tocorre.XmlRpc
{
    enum Type
    {
            XMLRPC_TYPE_UNTYPED
        ,   XMLRPC_TYPE_ARRAY
        ,   XMLRPC_TYPE_BASE64
        ,   XMLRPC_TYPE_BOOLEAN
        ,   XMLRPC_TYPE_DATETIME
        ,   XMLRPC_TYPE_DOUBLE
        ,   XMLRPC_TYPE_INTEGER
        ,   XMLRPC_TYPE_STRING
        ,   XMLRPC_TYPE_STRUCT
        ,   XMLRPC_TYPE_NIL
    }



    class Value
    {
        public const String VALUE_TAG = "value";
        public const String MEMBER_TAG = "member";
        public const String NAME_TAG = "name";
        public const String ARRAY_TAG = "array";
        public const String DATA_TAG = "data";

        private Type _type;
        private String _string_value;
        private Boolean _boolean_value;
        private Int32 _int_value;
        private Double _double_value;
        private Dictionary<String, Value> _struct_value;
        private List<Value> _array_value;

        public Value(Type p_Type)
        {
            this._type = p_Type;
            if (p_Type == Type.XMLRPC_TYPE_ARRAY)
                _array_value = new List<Value>();
            else if (p_Type == Type.XMLRPC_TYPE_STRUCT)
                _struct_value = new Dictionary<string, Value>();
        }

        public Value(Type p_Type, String p_Value)
            : this(p_Type)
        {
            this._string_value = p_Value;
        }

        public Value(Type p_Type, DateTime p_Value)
            : this(p_Type)
        {
            this._string_value = p_Value.ToString("s");
        }

        public Value(Type p_Type, Boolean p_Value)
            : this(p_Type)
        {
            this._boolean_value = p_Value;
        }

        public Value(Type p_Type, Int32 p_Value)
            : this(p_Type)
        {
            this._int_value = p_Value;
        }

        public Value(Type p_Type, Double p_Value)
            : this(p_Type)
        {
            this._double_value = p_Value;
        }

        public void AddToArray(Value p_Value)
        {
            if (_type != Type.XMLRPC_TYPE_ARRAY)
                throw new Exception("Value type is not ARRAY");
            _array_value.Add(p_Value);
        }

        public void AddToStruct(String p_Member, Value p_Value)
        {
            if (_type != Type.XMLRPC_TYPE_STRUCT)
                throw new Exception("Value type is not STRUCT");
            _struct_value[p_Member] = p_Value;
        }

        public static Type GetType(String p_Name)
        {
            switch (p_Name)
            {
                case "array":
                    return Type.XMLRPC_TYPE_ARRAY;
                case "base64":
                    return Type.XMLRPC_TYPE_BASE64;
                case "boolean":
                    return Type.XMLRPC_TYPE_BOOLEAN;
                case "dateTime.iso8601":
                    return Type.XMLRPC_TYPE_DATETIME;
                case "double":
                    return Type.XMLRPC_TYPE_DOUBLE;
                case "int":
                    return Type.XMLRPC_TYPE_INTEGER;
                case "nil":
                    return Type.XMLRPC_TYPE_NIL;
                case "string":
                    return Type.XMLRPC_TYPE_STRING;
                case "struct":
                    return Type.XMLRPC_TYPE_STRUCT;
                default:
                    return Type.XMLRPC_TYPE_UNTYPED;
            }

        }

        public static String GetTypeName(Type p_Type)
        {
            switch (p_Type)
            {
                case Type.XMLRPC_TYPE_ARRAY:
                    return "array";
                case Type.XMLRPC_TYPE_BASE64:
                    return "base64";
                case Type.XMLRPC_TYPE_BOOLEAN:
                    return "boolean";
                case Type.XMLRPC_TYPE_DATETIME:
                    return "dateTime.iso8601";
                case Type.XMLRPC_TYPE_DOUBLE:
                    return "double";
                case Type.XMLRPC_TYPE_INTEGER:
                    return "int";
                case Type.XMLRPC_TYPE_NIL:
                    return "nil";
                case Type.XMLRPC_TYPE_STRING:
                    return "string";
                case Type.XMLRPC_TYPE_STRUCT:
                    return "struct";
                case Type.XMLRPC_TYPE_UNTYPED:
                default:
                    return "";
            }
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public String ToString(bool p_PrintTypes)
        {
            StringBuilder sbuf = new StringBuilder();

            if (p_PrintTypes)
            {
                sbuf.Append("Type "  + Value.GetTypeName(_type)).Append(": ");
            }

            switch (_type)
            {
                case Type.XMLRPC_TYPE_BOOLEAN:
                    if (_boolean_value)
                        sbuf.Append("1");
                    else
                        sbuf.Append("0");
                    break;
                case Type.XMLRPC_TYPE_DOUBLE:
                    sbuf.Append(_double_value);
                    break;
                case Type.XMLRPC_TYPE_INTEGER:
                    sbuf.Append(_int_value);
                    break;
                case Type.XMLRPC_TYPE_BASE64:
                case Type.XMLRPC_TYPE_DATETIME:
                case Type.XMLRPC_TYPE_STRING:
                    sbuf.Append(_string_value);
                    break;
                case Type.XMLRPC_TYPE_NIL:
                    sbuf.Append("nil");
                    break;
                case Type.XMLRPC_TYPE_ARRAY:
                    if (p_PrintTypes)
                        sbuf.Append("\n");
                    int i = 0;
                    // iterate over elements and output
                    foreach (Value v in _array_value)
                    {
                        sbuf.Append("Element #").Append(i++).Append(": ").Append(v.ToString(p_PrintTypes)).Append("\n");
                    }
                    break;
                case Type.XMLRPC_TYPE_STRUCT:
                    if (p_PrintTypes)
                        sbuf.Append("\n");
                    // iterate over elements and output
                    foreach (KeyValuePair<String, Value> kvp in _struct_value)
                    {
                        sbuf.Append("Member ").Append(kvp.Key).Append(": ").Append(kvp.Value.ToString(p_PrintTypes)).Append("\n");
                    }
                    break;
                default:    
                    throw new Exception("Not implemented :(");

            }

            return sbuf.ToString();
        }

        public Type type
        {
            get { return this._type; }
        }
        public String string_value
        {
            get { return this._string_value; }
        }
        public Boolean boolean_value
        {
            get { return this._boolean_value; }
        }
        public Int32 int_value
        {
            get { return this._int_value; }
        }
        public Dictionary<String, Value> struct_value
        {
            get { return this._struct_value; }
        }
        public List<Value> array_value
        {
            get { return this._array_value; }
        }

        public static Value FromXml(XmlNode p_Node)
        {
            Value v = null;

            foreach (XmlNode x in p_Node.ChildNodes)
            {
                if (x.NodeType != XmlNodeType.Element && x.NodeType != XmlNodeType.Text)
                    continue;

                if (x.NodeType == XmlNodeType.Text)
                {
                    // by default, it is a string
                    v = new Value(Type.XMLRPC_TYPE_STRING, x.Value);
                }
                else
                {    // it's an element
                    if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_BOOLEAN))
                    {
                        if (x.InnerText == "0")
                            v = new Value(Type.XMLRPC_TYPE_BOOLEAN, false);
                        else
                            v = new Value(Type.XMLRPC_TYPE_BOOLEAN, true);
                    }
                    else if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_DOUBLE))
                    {
                        Double d;
                        if (Double.TryParse(x.InnerText, out d))
                        {
                            v = new Value(Type.XMLRPC_TYPE_DOUBLE, d);
                        }
                        else
                        {
                            throw new Exception("Invalid double value: " + x.InnerText);
                        }
                    }
                    else if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_INTEGER) || x.Name == "i4") // i4 is alternate typing for int
                    {
                        Int32 i;
                        if (Int32.TryParse(x.InnerText, out i))
                        {
                            v = new Value(Type.XMLRPC_TYPE_INTEGER, i);
                        }
                        else
                        {
                            throw new Exception("Invalid integer value: " + x.InnerText);
                        }
                    }
                    else if (
                           x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_STRING)
                        || x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_BASE64)
                        || x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_DATETIME)
                        )
                    {
                        v = new Value(Value.GetType(x.Name), x.InnerText);
                    }
                    else if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_NIL))
                    {
                        v = new Value(Type.XMLRPC_TYPE_NIL);
                    }
                    else if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_ARRAY))
                    {
                        v = new Value(Type.XMLRPC_TYPE_ARRAY);

                        foreach (XmlNode y in x.ChildNodes)
                        {
                            if (y.NodeType != XmlNodeType.Element)
                                continue;
                            if (y.Name != Value.DATA_TAG)
                                throw new Exception("Got " + y.Name + " tag. Expected " + Value.DATA_TAG);
                            foreach (XmlNode z in y.ChildNodes)
                            {
                                if (z.NodeType != XmlNodeType.Element)
                                    continue;
                                if (z.Name != Value.VALUE_TAG)
                                    throw new Exception("Got " + z.Name + " tag. Expected " + Value.VALUE_TAG);
                                v.AddToArray(Value.FromXml(z));
                            }

                        }
                    }
                    else if (x.Name == Value.GetTypeName(Type.XMLRPC_TYPE_STRUCT))
                    {
                        v = new Value(Type.XMLRPC_TYPE_STRUCT);

                        foreach (XmlNode y in x.ChildNodes)
                        {
                            if (y.NodeType != XmlNodeType.Element)
                                continue;
                            if (y.Name != Value.MEMBER_TAG)
                                throw new Exception("Got " + y.Name + " tag. Expected " + Value.DATA_TAG);

                            String name = null;
                            Value innerValue = null;

                            foreach (XmlNode z in y.ChildNodes)
                            {
                                if (z.NodeType != XmlNodeType.Element)
                                    continue;
                                if (z.Name == Value.NAME_TAG)
                                    name = z.InnerText;
                                else if (z.Name == Value.VALUE_TAG)
                                    innerValue = Value.FromXml(z);
                                else
                                    throw new Exception("Got " + z.Name + " tag. Expected " + Value.VALUE_TAG);

                            }
                            if (name != null && innerValue != null)
                                v.AddToStruct(name, innerValue);
                            else
                                throw new Exception("name or value tags are missing");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid type name: " + p_Node.Name);
                    }
                }
            }

            return v;
        }

        public String ToXml()
        {
            StringBuilder sbuf = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;
            xws.IndentChars = "  ";
            xws.Encoding = Encoding.UTF8;
            xws.OmitXmlDeclaration = true;
            xws.ConformanceLevel = ConformanceLevel.Fragment;
            xws.CloseOutput = false;
            XmlWriter xw = XmlWriter.Create(sbuf, xws);

            xw.WriteStartElement(Value.VALUE_TAG);
            if (this._type != Type.XMLRPC_TYPE_UNTYPED)
            {
                switch (this._type)
                {
                    case Type.XMLRPC_TYPE_BOOLEAN:
                        if (_boolean_value)
                            xw.WriteElementString(Value.GetTypeName(_type), "1");
                        else
                            xw.WriteElementString(Value.GetTypeName(_type), "0");
                        break;
                    case Type.XMLRPC_TYPE_DOUBLE:
                        xw.WriteElementString(Value.GetTypeName(_type), _double_value.ToString());
                        break;
                    case Type.XMLRPC_TYPE_INTEGER:
                        xw.WriteElementString(Value.GetTypeName(_type), _int_value.ToString());
                        break;
                    case Type.XMLRPC_TYPE_BASE64:
                    case Type.XMLRPC_TYPE_DATETIME:
                    case Type.XMLRPC_TYPE_STRING:
                        xw.WriteElementString(Value.GetTypeName(_type), _string_value);
                        break;
                    case Type.XMLRPC_TYPE_NIL:
                        xw.WriteStartElement(Value.GetTypeName(_type));
                        xw.WriteEndElement();
                        break;
                    case Type.XMLRPC_TYPE_ARRAY:
                        // iterate over elements and output
                        xw.WriteStartElement(Value.ARRAY_TAG);
                        xw.WriteStartElement(Value.DATA_TAG);
                        foreach (Value v in _array_value)
                        {
                            xw.WriteRaw(v.ToXml());
                        }
                        xw.WriteEndElement(); // Value.DATA_TAG
                        xw.WriteEndElement(); // Value.ARRAY_TAG
                        break;
                    case Type.XMLRPC_TYPE_STRUCT:
                        // iterate over elements and output
                        xw.WriteStartElement(Value.GetTypeName(_type)); // <struct>
                        foreach (KeyValuePair<String, Value> kvp in _struct_value)
                        {
                            xw.WriteStartElement(Value.MEMBER_TAG);
                                xw.WriteElementString(Value.NAME_TAG, kvp.Key);
                                xw.WriteRaw(kvp.Value.ToXml());
                            xw.WriteEndElement(); // Value.MEMBER_TAG
                        }
                        xw.WriteEndElement(); // </struct>
                        break;
                    default:
                        throw new Exception("Not implemented :(");
                }
            }
            xw.WriteEndElement(); // Value.VALUE_TAG
            xw.Flush();

            return sbuf.ToString();
        }
    }

    class MethodCall
    {

        public const String METHOD_CALL_TAG = "methodCall";
        public const String METHOD_NAME_TAG = "methodName";
        public const String PARAMS_TAG = "params";
        public const String PARAM_TAG = "param";

        private String _method_name;
        private List<Value> _parameters;

        public String method_name
        {
            get { return this._method_name; }
        }
        public List<Value> parameters
        {
            get { return this._parameters; }
        }

        public MethodCall(String p_MethodName)
        {
            _parameters = new List<Value>();
            this._method_name = p_MethodName;
        }

        public void AddParameter(Value p_Parameter)
        {
            _parameters.Add(p_Parameter);
        }

        public String ToXml()
        {
            StringBuilder sbuf = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Encoding = Encoding.UTF8;
            xws.Indent = true;
            xws.IndentChars = "  ";
            
            XmlWriter xw = XmlWriter.Create(sbuf, xws);

            xw.WriteStartDocument();
            xw.WriteStartElement(MethodCall.METHOD_CALL_TAG);

            // set method name
            xw.WriteElementString(MethodCall.METHOD_NAME_TAG, this._method_name);

            // add parameters
            xw.WriteStartElement(MethodCall.PARAMS_TAG);
            foreach (Value v in _parameters)
            {
                xw.WriteStartElement(MethodCall.PARAM_TAG);
                xw.WriteRaw(v.ToXml());
                xw.WriteEndElement(); // Call.PARAM_TAG
            }
            xw.WriteEndElement(); // Call.PARAMS_TAG

            xw.WriteEndElement();   // end Call.METHOD_CALL_TAG
            xw.WriteEndDocument();
            xw.Flush();
            return sbuf.ToString();
        }
    }

    class MethodResponse
    {

        public const String METHOD_RESPONSE_TAG = "methodResponse";
        public const String PARAMS_TAG = "params";
        public const String PARAM_TAG = "param";
        public const String FAULT_TAG = "fault";
        public const String VALUE_TAG = "value";

        private String _raw_response;
        private Boolean _fault;
        private Value _fault_value;
        private List<Value> _values;

        public String raw_response
        {
            get { return this._raw_response; }
        }

        public Boolean fault
        {
            get { return this._fault; }
        }

        public List<Value> values
        {
            get { return this._values; }
        }

        public Value fault_value
        {
            get { return this._fault_value; }
        }

        private void Parse()
        {
            Boolean found = false;
            StringReader sr = new StringReader(_raw_response);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(sr);

            // won't use GetElementsByTagName to validate the actual document structure.

            // find methodResponse tag
            found = false;
            XmlNode methodResponseNode = null;
            foreach (XmlNode x in xdoc.ChildNodes)
            {
                if (x.NodeType == XmlNodeType.Element && x.Name == MethodResponse.METHOD_RESPONSE_TAG)
                {
                    methodResponseNode = x;
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new Exception(MethodResponse.METHOD_RESPONSE_TAG + " tag not found on response");

            // find params or fault element
            found = false;
            foreach (XmlNode x in methodResponseNode.ChildNodes)
            {
                if (x.NodeType != XmlNodeType.Element)
                    continue;
                if (x.Name == MethodResponse.FAULT_TAG)
                {
                    foreach (XmlNode y in x.ChildNodes)
                    {
                        if (y.NodeType != XmlNodeType.Element)
                            continue;
                        if (y.Name == Value.VALUE_TAG)
                        {
                            _fault_value = Value.FromXml(y);
                            found = true;
                            _fault = true;
                        }
                    }
                    break;
                }
                else if (x.Name == MethodResponse.PARAMS_TAG)
                {
                    _fault = false;
                    // read all parameters returned
                    foreach (XmlNode y in x.ChildNodes)
                    {
                        if (y.NodeType != XmlNodeType.Element)
                            continue;
                        if (y.Name != MethodResponse.PARAM_TAG)
                            throw new Exception("Invalid " + y.Name + " tag inside params");
                        else {
                            // find value node
                            foreach (XmlNode z in y.ChildNodes)
                            {
                                if (z.NodeType != XmlNodeType.Element)
                                    continue;
                                if (z.Name == MethodResponse.VALUE_TAG)
                                    // add to values
                                    _values.Add(Value.FromXml(z));
                                else
                                    throw new Exception("Invalid tag " + z.Name + " inside param tag");
                            }
                        }
                    }

                    found = true;
                    break;
                }
            }

            if (!found)
                throw new Exception(MethodResponse.FAULT_TAG + " or " + MethodResponse.PARAMS_TAG + " not found!");

        }

        public MethodResponse(String p_RawResponse)
        {
            this._raw_response = p_RawResponse;
            _values = new List<Value>();
            Parse();
        }

        public override string ToString()
        {
            StringBuilder sbuf = new StringBuilder();

            sbuf.Append("Method Response. Faulted: ").Append(_fault).Append("\n");
            if (_fault)
                sbuf.Append("Fault Value:\n").Append(_fault_value);
            else {
                int i = 0;
                foreach (Value v in _values) {
                    sbuf.Append("Value #").Append(i).Append(":\n").Append(v.ToString());
                }
            }

            return sbuf.ToString();
        }
    }

    class Call
    {
        private String _url;
        private MethodCall _method_call;

        public String url
        {
            get { return this._url; }
        }
        public MethodCall method_call
        {
            get { return this._method_call; }
        }

        public Call(String p_Url, MethodCall p_MethodCall)
        {
            this._url = p_Url;
            this._method_call = p_MethodCall;
        }

        public MethodResponse Execute()
        {
            StreamWriter sw = null;

            // prepare data
            
            String xmlstr = _method_call.ToXml();

            // prepare request
            HttpWebRequest hwreq = (HttpWebRequest)WebRequest.Create(url);
            hwreq.Method = "POST";
            hwreq.UserAgent = "tocorre.XmlRpc .NET Client"; // make this a property later
            hwreq.ContentType = "text/xml";
            hwreq.ContentLength = xmlstr.Length;
            
            try
            {
                sw = new StreamWriter(hwreq.GetRequestStream());
                sw.Write(xmlstr);
            }
            catch (WebException we)
            {
                // some unexpected error occurred. Log it?
                Console.WriteLine(we.Message);
            }
            finally
            {
                sw.Close();
            }

            // read response
            HttpWebResponse hwresp = (HttpWebResponse)hwreq.GetResponse();
            MethodResponse method_response = null;
            using (StreamReader sr = new StreamReader(hwresp.GetResponseStream()))
            {
                String result = sr.ReadToEnd();
                method_response = new MethodResponse(result);
                sr.Close();
            }
            return method_response;
        }
    }
}
