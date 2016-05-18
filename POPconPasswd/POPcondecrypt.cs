using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPconPasswd
{
    class POPcondecrypt
    {
        string password;
        ICollection<KeyValuePair<char, byte>> alp = new Dictionary<char, byte>();
        byte[] key = { 22,4,18,6,9,18,12,6,4,12,18,6,22,9,12,18,4,9,6,12,18,9,6,12,4,18,9,6,12,6,16,18,22,6,18,9,6,12,18,22,6,9,12,18,22,6,9,12,18,6,22,9};

        public POPcondecrypt()
        {
            init();
        }

        public POPcondecrypt(string password)
        {
            this.password = password;
            init();
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        private void init()
        {

            //byte[] a = new byte[26 * 2 + 11];
            for (byte i = 0; i < 62; i++)
            {
                byte a = 0;
                if (i < 10)
                {
                    a = i;
                    a += 0x30;
                }

                if (i >= 10 && i <= 35)
                {
                    a = i;
                    a += 0x41;
                    a -= 10;
                }

                if (i > 35)
                {
                    a = i;
                    a += 0x61;
                    a -= 36;
                }
                alp.Add(new KeyValuePair<char, byte>((char)a, i));
            }
        }


        public string decrypt()
        {
            string output = string.Empty;
            if(password.Length > 52)
                return null;
            else
            {
                for (int i = 0; i < password.Length; i++)
                {
                    if (char.IsLetterOrDigit(password[i]))
                    {
                        var val1 = alp.FirstOrDefault(x => x.Key == password[i]).Value;
                        var offset = val1 + key[i];
                        if(offset > 62)
                        {
                            val1 = alp.FirstOrDefault(x => x.Key == 'z').Value;
                            offset = offset - val1-2;
                        }
                        var ch = alp.FirstOrDefault(x => x.Value == offset).Key;
                        output += ch;
                    }
                    else
                    {
                        output += password[i];
                    }
                }
            }
            return output;
        }
    }
}
