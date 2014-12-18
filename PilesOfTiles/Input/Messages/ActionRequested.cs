using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilesOfTiles.Input.Messages
{
    public class ActionRequested
    {
        public Action Action { get; set; }
    }

    public enum Action
    {
        MoveLeft,
        MoveDown,
        MoveRight,
        Rotate
    }
}
