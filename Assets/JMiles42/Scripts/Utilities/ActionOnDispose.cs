using System;
using JMiles42.Extensions;

namespace JMiles42.Utilities
{
    public class ActionOnDispose: IDisposable
    {
        private readonly Action action;
        public ActionOnDispose(Action action) { this.action = action; }
        public void Dispose() { action.Trigger(); }
    }
}