using System;
using System.Collections.Generic;
using System.Text;

namespace tocorre.TocoAPI
{
    class TestProgram
    {
        static void Main(string[] args)
        {
            Toco t = new Toco();

            t.Login("tu_email@de_tocorre.com", "tu_password");
            t.Profile();
            t.Scraps(0, 0, 0, 0, 3);
            t.Msg(0, 0, 0, 0, 0, 0);
            t.SetMotd(0, "--- Venga Emma! Venga Rafael! --- (seteado desde el glorioso TOCOAPI)");
            List<Tocobject> l = t.ExecuteSystemMultiCall();

            Session session = (Session)l[0];
            GenericValue profile = (GenericValue)l[1];
            Scrap scrap = (Scrap)l[2];
            Msgs msgs = (Msgs)l[3];
            GenericValue motd = (GenericValue)l[4];

            if (!session.fault)
                Console.WriteLine("Got session id: " + session.sid_id);
            else
                Console.WriteLine("Error! - Couldn't get session. Check your username and password -> Server said: " + session.fault_value.struct_value["faultString"].string_value);

            if (!msgs.fault)
            {
                Console.WriteLine("Got " + msgs.entries.Count + " messages");
                Console.WriteLine("----");
                foreach (Msg m in msgs.entries.Values)
                {
                    Console.WriteLine("MsgId: " + m.msg_id);
                    Console.WriteLine(m.text);
                }
                Console.WriteLine("----");
            }

            Console.Read();
        }
    }
}
