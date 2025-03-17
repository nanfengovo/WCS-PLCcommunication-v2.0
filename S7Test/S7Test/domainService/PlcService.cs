using Microsoft.Extensions.Options;
using S7.Net;
using S7Test.Model;

namespace S7Test.domainService
{
    public class PlcService : IDisposable
    {
        private readonly Plc _plc;

        public PlcService(IOptions<PlcSettings> settings)
        {
            _plc = new Plc(CpuType.S71200,
                          settings.Value.IpAddress,
                          settings.Value.Port,
                          settings.Value.Rack,
                          settings.Value.Slot);

            if(!_plc.IsConnected)
            {
                _plc.Open();
            }

            
        }

        public object Read(string address, DataType dataType)
        {
            ValidateAddress(address, dataType);
        }

        private void ValidateAddress(string address, DataType type)
        {
            var validPrefix = type switch
            {
                DataType.Bool => "DBX",
                DataType.Int or DataType.Short => "DBW",
                _ => throw new ArgumentException()
            };

            if (!address.Contains(validPrefix))
                throw new FormatException($"地址格式错误，{type}类型应使用{validPrefix}");
        }
    }
}
