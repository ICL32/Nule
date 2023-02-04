using System;

namespace Nule.Transport
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public int _userID { get; set; }
    }
}
