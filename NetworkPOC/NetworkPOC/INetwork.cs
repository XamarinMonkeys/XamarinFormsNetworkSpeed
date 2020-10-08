using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkPOC
{
    public interface INetwork
    {
        bool IsConnected();
        bool IsConnectedFast();
    }
}
