using Newinfosoft.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ytg.Chart.Client.NetWork
{
   public class AsyncConnectionEventArgs:EventArgs
    {
       public AsyncConnectionEventArgs(byte[] array, AsyncClient sender)
       {
           this.ContentArray = array;
           this.AsyncClient = sender;
       }

       public byte[] ContentArray { get; private set; }

       public AsyncClient AsyncClient { get; private set; }
    }
}
