using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModbusTcpAPI.Controllers;
using NModbus;
using System.Net.Sockets;

namespace ModbusTcpAPI.domainServer
{
    public class ModbusTCPServer
    {
        private readonly ModbusTCPConfig _modbusConfig;

        public ModbusTCPServer(IOptions<ModbusTCPConfig> modbusConfig)
        {
            _modbusConfig = modbusConfig.Value;
        }


        /// <summary>
        /// 读取线圈寄存器
        /// </summary>
        /// <returns></returns>
        public async Task<bool[]> ReadCoils()
        {
            using (var client = new TcpClient(_modbusConfig.IP, _modbusConfig.Port))
            {
                var factory = new ModbusFactory();
                var master = factory.CreateMaster(client);

                ushort startAddress = _modbusConfig.StartAddress;
                ushort numRegisters = _modbusConfig.Num; // 读取的寄存器数量

                var result = await master.ReadCoilsAsync(_modbusConfig.SlaveID, startAddress, numRegisters);
                return result;
            }
        }
        /// <summary>
        /// 写入线圈寄存器
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> WriteCoils(bool value)
        {
            using (var client = new TcpClient(_modbusConfig.IP, _modbusConfig.Port))
            {
                var factory = new ModbusFactory();
                var master = factory.CreateMaster(client);
                ushort startAddress = _modbusConfig.StartAddress;
                await master.WriteSingleCoilAsync(_modbusConfig.SlaveID, startAddress, value);
                return true;
            }
        }

        /// <summary>
        /// 读取离散寄存器
        /// </summary>
        /// <returns></returns>
        public async Task<bool[]> ReadDiscreteInputsAsync()
        {
            using (var client = new TcpClient(_modbusConfig.IP, _modbusConfig.Port))
            {
                var factory = new ModbusFactory();
                var master = factory.CreateMaster(client);

                ushort startAddress = _modbusConfig.StartAddress;
                ushort numRegisters = _modbusConfig.Num; // 读取的寄存器数量

                var result = await master.ReadInputsAsync(_modbusConfig.SlaveID, startAddress, numRegisters);
                return result;
            }
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <returns></returns>
        public async Task<ushort[]> ReadHoldingRegisters()
        {
            using (var client = new TcpClient(_modbusConfig.IP, _modbusConfig.Port))
            {
                var factory = new ModbusFactory();
                var master = factory.CreateMaster(client);

                ushort startAddress = _modbusConfig.StartAddress;
                ushort numRegisters = _modbusConfig.Num; // 读取的寄存器数量

                var registers = await master.ReadHoldingRegistersAsync(_modbusConfig.SlaveID, startAddress, numRegisters);
                return registers;
            }
        }

        /// <summary>
        /// 写入保持寄存器
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> WriteSingleRegister( ushort value)
        {
            using (var client = new TcpClient(_modbusConfig.IP, _modbusConfig.Port))
            {
                var factory = new ModbusFactory();
                var master = factory.CreateMaster(client);

                ushort startAddress = _modbusConfig.StartAddress;

                await  master.WriteSingleRegisterAsync(_modbusConfig.SlaveID, startAddress, value);
                return true;
            }
        }
    }
}
