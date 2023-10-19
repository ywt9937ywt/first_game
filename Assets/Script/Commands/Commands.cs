using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public abstract class Commands
    {
        public abstract void Execute();
        public abstract void Undo();
}
}

