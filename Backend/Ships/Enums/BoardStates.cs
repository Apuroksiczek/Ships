using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships.Enums
{
    public enum BoardStates
    {
        Empty = ' ',
        Hit = 'X',
        Missed = '·',
        Sinked = 'S',
        Ship = 'A'
    }
}