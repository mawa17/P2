using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Classes
{

    public interface IFruit
    {
        string GetColor();
    }

    public class Apple : IFruit
    {
        public virtual string GetColor() => "Rød";
    }
    public class Banana : Apple
    {
        public override string GetColor() => "Gul";
    }
}
