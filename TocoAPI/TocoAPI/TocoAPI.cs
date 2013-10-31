using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using tocorre.XmlRpc;

namespace tocorre.TocoAPI
{
    
    // all objects returned by TocoAPI must extend this class
    abstract class Tocobject
    {
        public const String TOCOBJECT_TYPE_BASE_URL = "BaseUrl";
        public const String TOCOBJECT_TYPE_FOTO = "Foto";
        public const String TOCOBJECT_TYPE_IMG = "Img";
        public const String TOCOBJECT_TYPE_SESSION = "Session";
        public const String TOCOBJECT_TYPE_GENERIC_VALUE = "GenericValue";
        public const String TOCOBJECT_TYPE_SCRAP = "Scrap";
        public const String TOCOBJECT_TYPE_SCRAP_ENTRY = "ScrapEntry";
        public const String TOCOBJECT_TYPE_MSG = "Msg";
        public const String TOCOBJECT_TYPE_MSGS = "Msgs";

        public abstract String GetTocobjectType();

        protected Boolean _fault;
        protected Value _fault_value;

        public Boolean fault
        {
            get { return this._fault; }
        }
        public Value fault_value
        {
            get { return this._fault_value; }
        }

        public Tocobject()
        {
            _fault = false;
            _fault_value = null;
        }

        public Tocobject(Boolean p_Fault, Value p_FaultValue)
        {
            this._fault = p_Fault;
            this._fault_value = p_FaultValue;
        }


    }

    class BaseUrl
        : Tocobject
    {
        private String _root;
        private String _page;
        private String _data;

        public String root
        {
            get { return this._root; }
        }

        public String page
        {
            get { return this._page; }
        }

        public String data
        {
            get { return this._data; }
        }

        public BaseUrl(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public BaseUrl()
        {
        }

        public static BaseUrl FromValue(Value v)
        {
            BaseUrl b = new BaseUrl();

            b._root = v.struct_value["root"].string_value;
            b._page = v.struct_value["page"].string_value;
            b._data = v.struct_value["data"].string_value;

            return b;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_BASE_URL;
        }
    }

    class Foto
        : Tocobject
    {
        private String _ty;
        private String _sm;
        private String _lg;

        public String ty
        {
            get { return this._ty; }
        }
        public String sm
        {
            get { return this._sm; }
        }
        public String lg
        {
            get { return this._lg; }
        }

        public Foto(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Foto()
        {
        }

        public static Foto FromValue(Value v)
        {
            Foto f = new Foto();

            f._ty = v.struct_value["ty"].string_value;
            f._sm = v.struct_value["sm"].string_value;
            f._lg = v.struct_value["lg"].string_value;

            return f;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_FOTO;
        }

    }

    class Img
        : Tocobject
    {
        private String _icon;
        private Foto _foto;

        public String icon
        {
            get { return this._icon; }
        }
        public Foto foto
        {
            get { return this._foto; }
        }

        public Img(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Img()
        {
        }

        public static Img FromValue(Value v)
        {
            Img i = new Img();

            i._icon = v.struct_value["icon"].string_value;
            i._foto = Foto.FromValue(v.struct_value["foto"]);
            return i;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_IMG;
        }
    }

    class Session
        : Tocobject
    {
        private String _sid_id;
        private Int32 _nid;
        private String _nick;
        private String _fname;
        private String _lname;
        private Int32 _flags;
        private String _gender;
        private String _locale;
        private Int32 _lang;
        private Int32 _prefs;
        private BaseUrl _base_url;
        private String _page;
        private Img _img;

        public String sid_id
        {
            get { return this._sid_id; }
        }
        public Int32 nid
        {
            get { return this._nid; }
        }
        public String nick
        {
            get { return this._nick; }
        }
        public String fname
        {
            get { return this._fname; }
        }
        public String lname
        {
            get { return this._lname; }
        }
        public Int32 flags
        {
            get { return this._flags; }
        }
        public String gender
        {
            get { return this._gender; }
        }
        public String locale
        {
            get { return this._locale; }
        }
        public Int32 lang
        {
            get { return this._lang; }
        }
        public Int32 prefs
        {
            get { return this._prefs; }
        }
        public BaseUrl base_url
        {
            get { return this._base_url; }
        }
        public String page
        {
            get { return this._page; }
        }
        public Img img
        {
            get { return this._img; }
        }

        public Session(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Session()
        {
        }

        public static Session FromValue(Value v)
        {
            Session s = new Session();

            // the answer comes as a struct

            s._sid_id = v.struct_value["sid_id"].string_value;
            s._nid = v.struct_value["nid"].int_value;
            s._nick = v.struct_value["nick"].string_value;
            s._fname = v.struct_value["fname"].string_value;
            s._lname = v.struct_value["lname"].string_value;
            s._flags = v.struct_value["flags"].int_value;
            s._gender = v.struct_value["gender"].string_value;
            s._locale = v.struct_value["locale"].string_value;
            s._lang = v.struct_value["lang"].int_value;
            s._prefs = v.struct_value["prefs"].int_value;
            s._base_url = BaseUrl.FromValue(v.struct_value["base_url"]);
            s._page = v.struct_value["page"].string_value;
            s._img = Img.FromValue(v.struct_value["img"]);

            return s;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_SESSION;
        }
    }

    class ScrapEntry
        : Tocobject
    {
        private Int32 _scrap_id;
        private DateTime _settime;
        private DateTime _regtime;
        private String _text;
        private Int32 _nid;
        private Int32 _flags;

        public Int32 scrap_id
        {
            get { return this._scrap_id; }
        }
        public DateTime settime
        {
            get { return this._settime; }
        }
        public DateTime regtime
        {
            get { return this._regtime; }
        }
        public String text
        {
            get { return this._text; }
        }
        public Int32 nid
        {
            get { return this._nid; }
        }
        public Int32 flags
        {
            get { return this._flags; }
        }

        public static ScrapEntry FromValue(Value p_Value, Int32 p_ScrapId)
        {
            ScrapEntry se = new ScrapEntry();

            se._scrap_id = p_ScrapId;
            se._settime = DateTime.Parse(p_Value.struct_value["settime"].string_value);
            se._regtime = DateTime.Parse(p_Value.struct_value["regtime"].string_value);
            se._text = p_Value.struct_value["text"].string_value;
            Int32.TryParse(p_Value.struct_value["nid"].string_value, out se._nid);
            Int32.TryParse(p_Value.struct_value["flags"].string_value, out se._flags);

            return se;
        }

        public ScrapEntry(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public ScrapEntry()
        {
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_SCRAP_ENTRY;
        }

    }

    class Scrap
        : Tocobject
    {
        private Dictionary<Int32, ScrapEntry> _entries;
        private String _sql;

        public Dictionary<Int32, ScrapEntry> entries
        {
            get { return this._entries; }
        }
        public String sql 
        {
            get { return this._sql; }
        }

        public Scrap(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Scrap()
        {
            _entries = new Dictionary<Int32, ScrapEntry>();
        }

        public static Scrap FromValue(Value p_Value)
        {
            // value would be an struct
            // member -> scrap_id
            // value -> struct with scrapentry info

            Scrap s = new Scrap();

            s._sql = p_Value.struct_value["sql"].string_value;

            foreach (KeyValuePair<String, Value> kvp in p_Value.struct_value["scrap"].struct_value)
            {
                Int32 scrap_id = 0;
                if (Int32.TryParse(kvp.Key, out scrap_id))
                {
                    ScrapEntry se = ScrapEntry.FromValue(kvp.Value, scrap_id);
                    s._entries.Add(scrap_id, se);
                }
                else
                {
                    throw new Exception("Invalid scrap_id -> [" + kvp.Key + "]");
                }
            }

            return s;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_SCRAP;
        }
    }

    class Msg
        : Tocobject
    {
        private Int32 _msg_id;
        private DateTime _settime;
        private DateTime _regtime;
        private String _text;
        private Int32 _nid;
        private Int32 _flags;

        public Int32 msg_id
        {
            get { return this._msg_id; }
        }
        public DateTime settime
        {
            get { return this._settime; }
        }
        public DateTime regtime
        {
            get { return this._regtime; }
        }
        public String text
        {
            get { return this._text; }
        }
        public Int32 nid
        {
            get { return this._nid; }
        }
        public Int32 flags
        {
            get { return this._flags; }
        }

        public Msg(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Msg()
        {
        }

        public static Msg FromValue(Value p_Value, Int32 p_MsgId)
        {
            Msg m = new Msg();

            m._msg_id = p_MsgId;
            m._settime = DateTime.Parse(p_Value.struct_value["settime"].string_value);
            m._regtime = DateTime.Parse(p_Value.struct_value["regtime"].string_value);
            m._text = p_Value.struct_value["text"].string_value;
            Int32.TryParse(p_Value.struct_value["nid"].string_value, out m._nid);
            Int32.TryParse(p_Value.struct_value["flags"].string_value, out m._flags);

            return m;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_MSG;
        }

    }

    class Msgs
        : Tocobject
    {
        private Dictionary<Int32, Msg> _entries;
        private String _sql;

        public Dictionary<Int32, Msg> entries
        {
            get { return this._entries; }
        }
        public String sql
        {
            get { return this._sql; }
        }

        public Msgs(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public Msgs()
        {
            _entries = new Dictionary<Int32, Msg>();
        }

        public static Msgs FromValue(Value p_Value)
        {
            // value would be an struct
            // member -> msg_id
            // value -> struct with msg info

            Msgs ms = new Msgs();

            ms._sql = p_Value.struct_value["sql"].string_value;

            foreach (KeyValuePair<String, Value> kvp in p_Value.struct_value["msg"].struct_value)
            {
                Int32 msg_id = 0;
                if (Int32.TryParse(kvp.Key, out msg_id))
                {
                    Msg m = Msg.FromValue(kvp.Value, msg_id);
                    ms._entries.Add(msg_id, m);
                }
                else
                {
                    throw new Exception("Invalid msg_id -> [" + kvp.Key + "]");
                }
            }

            return ms;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_MSGS;
        }
    }

    class GenericValue
        : Tocobject
    {
        private Value _value;

        public Value value
        {
            get { return this._value; }
        }

        public GenericValue(Boolean p_Fault, Value p_FaultValue)
            : base(p_Fault, p_FaultValue)
        {
        }

        public GenericValue()
        {
        }

        public static GenericValue FromValue(Value p_Value)
        {
            GenericValue gv = new GenericValue();
            gv._value = p_Value;
            return gv;
        }

        public override string GetTocobjectType()
        {
            return Tocobject.TOCOBJECT_TYPE_GENERIC_VALUE;
        }
    }

    // TOCO API!
    class Toco
    {

        private const String LOGIN_METHOD = "toco.login";
        private const String PROFILE_METHOD = "toco.profile";
        private const String SCRAPS_METHOD = "toco.scraps";
        private const String MSG_METHOD = "toco.msg";
        private const String FLUSH_METHOD = "toco.flush";
        private const String SET_MOTD_METHOD = "toco.set_motd";
        private const String SYSTEM_MULTI_CALL_METHOD = "system.multicall";

        private static MethodCall GetLoginMethodCall(String p_User, String p_Password, Int32 p_Nid)
        {
            // prepare call
            MethodCall mc = new MethodCall(Toco.LOGIN_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_STRING, p_User));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_STRING, p_Password));
            if (p_Nid > 0)
            {
                mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Nid.ToString()));
            }
            return mc;
        }

        private static MethodCall GetProfileMethodCall(Int32 p_Nid)
        {
            // prepare call
            MethodCall mc = new MethodCall(Toco.PROFILE_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Nid));
            return mc;
        }

        private static MethodCall GetScrapsMethodCall(Int32 p_Nid, Int32 p_UnixTimeStamp, Int32 p_NoteId, Int32 p_Offset, Int32 p_Qty)
        {
            MethodCall mc = new MethodCall(Toco.SCRAPS_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Nid));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_UnixTimeStamp));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_NoteId));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Qty));
            return mc;
        }

        private static MethodCall GetMsgMethodCall(int p_Nid, int p_UnixTimeStamp, int p_MsgId, int p_Offset, int p_Qty, int p_Folder)
        {
            MethodCall mc = new MethodCall(Toco.MSG_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Nid));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_UnixTimeStamp));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_MsgId));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Qty));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Folder));
            return mc;
        }

        private static MethodCall GetFlushMethodCall(int p_Required)
        {
            MethodCall mc = new MethodCall(Toco.FLUSH_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Required));
            return mc;
        }

        private static MethodCall GetSetMotdMethodCall(int p_Required, string p_Motd)
        {
            MethodCall mc = new MethodCall(Toco.SET_MOTD_METHOD);
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_INTEGER, p_Required));
            mc.AddParameter(new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_STRING, p_Motd));
            return mc;
        }

        private List<MethodCall> m_MethodCalls;

        private void AddMethodCall(MethodCall p_MethodCall)
        {
            m_MethodCalls.Add(p_MethodCall);
        }

        public Toco()
        {
            m_MethodCalls = new List<MethodCall>();
        }

        // Login API function
        public void Login(String p_User, String p_Password)
        {
            this.Login(p_User, p_Password, 0);
        }

        public void Login(String p_User, String p_Password, Int32 p_Nid)
        {
            MethodCall mc = Toco.GetLoginMethodCall(p_User, p_Password, p_Nid);
            this.AddMethodCall(mc);
        }

        // Profile API function
        public void Profile()
        {
            this.Profile(0);
        }

        public void Profile(Int32 p_Nid)
        {
            MethodCall mc = Toco.GetProfileMethodCall(p_Nid);
            this.AddMethodCall(mc);
        }

        // Scraps API function
        public void Scraps(Int32 p_Nid, Int32 p_UnixTimeStamp, Int32 p_NoteId, Int32 p_Offset, Int32 p_Qty)
        {
            MethodCall mc = Toco.GetScrapsMethodCall(p_Nid, p_UnixTimeStamp, p_NoteId, p_Offset, p_Qty);
            this.AddMethodCall(mc);
        }

        // Msg API function
        public void Msg(Int32 p_Nid, Int32 p_UnixTimeStamp, Int32 p_MsgId, Int32 p_Offset, Int32 p_Qty, Int32 p_Folder)
        {
            MethodCall mc = Toco.GetMsgMethodCall(p_Nid, p_UnixTimeStamp, p_MsgId, p_Offset, p_Qty, p_Folder);
            this.AddMethodCall(mc);
        }

        // Flush API function
        public void Flush(Int32 p_Required)
        {
            MethodCall mc = Toco.GetFlushMethodCall(p_Required);
            this.AddMethodCall(mc);
        }

        public void SetMotd(Int32 p_Required, String p_Motd)
        {
            MethodCall mc = Toco.GetSetMotdMethodCall(p_Required, p_Motd);
            this.AddMethodCall(mc);
        }
        
        // Execute multi :(
        public List<Tocobject> ExecuteSystemMultiCall()
        {
            MethodCall mc = new MethodCall(Toco.SYSTEM_MULTI_CALL_METHOD);

            // prepare unique parameter with multiple calls
            Value varray = new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY);
            foreach (MethodCall mcc in m_MethodCalls)
            {
                Value vcall = new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_STRUCT);
                vcall.AddToStruct("methodName", new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_STRING, mcc.method_name));
                Value vparams = new Value(tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY);
                foreach (Value param in mcc.parameters)
                {
                    vparams.AddToArray(param);
                }
                vcall.AddToStruct("params", vparams);
                varray.AddToArray(vcall);
            }
            mc.AddParameter(varray);
            Call c = new Call("http://www.tocorre.com/ext/rpc/rpc_api.php", mc);
            MethodResponse mr = c.Execute();

            // unique return is an array type Value. Each array element has a Value 
            // corresponding to the return value of the nth method call queued
            List<Tocobject> ret = new List<Tocobject>();

            Int32 i = 0;
            foreach (MethodCall mcc in m_MethodCalls)
            {
                Value v = mr.values[0].array_value[i];
                switch (mcc.method_name)
                {
                    case LOGIN_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(Session.FromValue(v.array_value[0]));
                        else 
                            ret.Add(new Session(true, v));
                        break;
                    case PROFILE_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(GenericValue.FromValue(v.array_value[0]));
                        else
                            ret.Add(new GenericValue(true, v));
                        break;
                    case SCRAPS_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(Scrap.FromValue(v.array_value[0]));
                        else
                            ret.Add(new Scrap(true, v));
                        break;
                    case MSG_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(Msgs.FromValue(v.array_value[0]));
                        else
                            ret.Add(new Msgs(true, v));
                        break;
                    case FLUSH_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(GenericValue.FromValue(v.array_value[0]));
                        else
                            ret.Add(new GenericValue(true, v));

                        break;
                    case SET_MOTD_METHOD:
                        if (v.type == tocorre.XmlRpc.Type.XMLRPC_TYPE_ARRAY)
                            ret.Add(GenericValue.FromValue(v.array_value[0]));
                        else
                            ret.Add(new GenericValue(true, v));
                        break;
                    default:
                        throw new Exception("Unexpected method name: " + mcc.method_name);
                }
                i++;
            }

            return ret;
        }

        public static Session DirectLogin(String p_User, String p_Password)
        {
            return Toco.DirectLogin(p_User, p_Password, 0);
        }


        public static Session DirectLogin(String p_User, String p_Password, Int32 p_Nid)
        {
            MethodCall mc = Toco.GetLoginMethodCall(p_User, p_Password, p_Nid);
            // replace later with properties
            Call c = new Call("http://www.tocorre.com/ext/rpc/rpc_api.php", mc);
            MethodResponse mr = c.Execute();
            return Session.FromValue(mr.values[0]);
        }

        public static void DirectProfile(Int32 p_Nid)
        {
            // prepare call
            MethodCall mc = Toco.GetProfileMethodCall(p_Nid);

            // replace later with properties
            Call c = new Call("http://www.tocorre.com/ext/rpc/rpc_api.php", mc);

            MethodResponse mr = c.Execute();

            Console.WriteLine(mr.ToString());

        }

    }
}
