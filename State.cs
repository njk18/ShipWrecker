using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{
    public class State
    {

        private StateType stateType;

        private enum StateType
        {
            Carrier,
            BattleShip
        }

        public State(String stateName)
        {

            this.stateType = (StateType)System.Enum.Parse(typeof(Tile), stateName);
        }
    }
}
