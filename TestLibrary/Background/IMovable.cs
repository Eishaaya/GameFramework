using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public interface IMovable
    {
        MoveComponent Movement { get; set; }
    }
}
