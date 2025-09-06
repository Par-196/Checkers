using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model.Enums
{
    public enum TypeCell
    {
        BlackCell = 1,
        WhiteCell,
        ImpossibleMove,
        PossibleMoveCell,
        SelectedChecker,
        GrayCheckersCell,
        WhiteCheckersCell
    }
}
