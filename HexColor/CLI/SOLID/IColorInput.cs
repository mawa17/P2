using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.SOLID;

public interface IColor
{
    byte GetColor(string componentName);
}
