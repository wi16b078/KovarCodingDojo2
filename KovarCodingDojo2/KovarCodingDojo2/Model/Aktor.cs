using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KovarCodingDojo2.Model
{
    public class Aktor : Shared.Models.BaseActuator
    {
        public Aktor(string name, string description, int id, string room, Type valueType) : base(name, description, id, room, valueType)
        {
        }
    }
}
