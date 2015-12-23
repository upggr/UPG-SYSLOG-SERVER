using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace UpgSyslogServer
{
    /// <summary>
    /// A tiny Syslog server
    /// http://www.upg.gr
    /// </summary>
    internal class UdpState
    {
        internal UdpState(UdpClient c, IPEndPoint e)
        {
            this.c = c;
            this.e = e;
        }

        internal UdpClient c;
        internal IPEndPoint e;
    }
}
