using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class Env
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

          public object get(Token name)
        {
            if (values.ContainsKey(name.lexeme))
            {
                return values[name.lexeme];
            }
            throw new RuntimeError(name, $"Tanımlanmamış değişken: '{name.lexeme}' .");
        }
        public void Assign(Token name, Object value)
        {
            if (values.ContainsKey(name.lexeme))
            {
                values[name.lexeme] = value;
                return;
            }

            throw new RuntimeError(name,
                "Undefined variable '" + name.lexeme + "'.");
        }
        public void Define(string name, object value)
          {
              values.Add(name, value);
          }
    }
}
