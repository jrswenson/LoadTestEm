using System;
using System.Collections.Generic;
using System.Text;

namespace LoadTestEm.ValueGetters
{
    public class RandomStringValueGetter : IValueGetter<string>
    {
        private int _length;
        private string _prepend;
        private string _append;
        private string _value;

        public RandomStringValueGetter(int length, string prepend = "", string append = "")
        {
            _length = length;
            _prepend = prepend;
            _append = append;
        }

        public string Value
        {
            get
            {
                if (_length < 1)
                    _length = 1;

                var value = string.Format("{0}{1}{2}", _prepend, GetRandomString(_length), _append);
                _value = value;
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        object IValueGetter.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = value.ToString();
            }
        }

        private static Random r = new Random();
        private static object randomLock = new object();
        public static string GetRandomString(int length)
        {
            var data = new List<byte>(length);

            lock (randomLock)
            {
                //per https://tools.ietf.org/html/rfc3986#section-2.3
                //only allow unreserved characters
                while (data.Count < length - 1)
                {
                    var next = r.Next(32, 127);
                    if ((next >= 48 && next <= 57) ||
                        (next >= 65 && next <= 90) ||
                        (next >= 97 && next <= 122) ||
                        next == 45 || next == 46 || next == 95 || next == 126)
                    {
                        data.Add((byte)next);
                    }
                }
            }

            var encoded = new ASCIIEncoding().GetString(data.ToArray());
            return encoded;
        }
    }
}
