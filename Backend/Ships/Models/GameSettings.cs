using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships.Models
{
    public class GameSettings
    {
        public const string SectionName = "GameSettings";
        public int BoardSize { get; init; }
        public int NumberOfShips { get; init; }
    }
}