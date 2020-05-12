using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCorpus.Interfaces
{
    public interface IAdvertiseAndDiscoverBluetoothDevice
    {
      void Advertise();
      void  Discover();
    }
}
