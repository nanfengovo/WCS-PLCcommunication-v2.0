using NModbus;
using System.Net;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Modbus_Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Read
            await Task.Run(async () =>
            {
                #region 定义初始的变量

                //ip
                string IP = "127.0.0.1";
                //port
                int Port = 502;
                //Slaveid
                byte SlaveID = 1;
                //Function
                int Function = 1;
                //Address
                ushort Address = 1000;
                //Query
                ushort Query = 6;

                while (true)
                {
                    
                    using (var Client = new TcpClient(IP, Port))
                    {
                        //使用NMdobus工厂创建NModbus主站
                        var factory = new ModbusFactory();
                        var master = factory.CreateMaster(Client);

                        switch (Function)
                        {
                            case 1:
                                var result1 = master.ReadCoils(SlaveID, Address, Query);
                                //PrintResult(result1,Address);
                                foreach (var item in result1)
                                {
                                    Console.WriteLine(item);
                                    await Task.Delay(2000);
                                }

                                break;
                            case 2:
                                var result2 = master.ReadInputs(SlaveID, Address, Query);
                                //PrintResult(result1,Address);
                                Console.WriteLine(result2);
                                break;
                            case 3:
                                var result3 = master.ReadHoldingRegisters(SlaveID, Address, Query);
                                //PrintResult(result3,Address);
                                Console.WriteLine(result3);
                                break;
                            case 4:
                                var result4 = master.ReadInputRegisters(SlaveID, Address, Query);
                                //PrintResult(result3,Address);
                                Console.WriteLine(result4);
                                break;
                            default:
                                // 当Function不是1, 2, 3, 或4时执行的代码
                                Console.WriteLine("Function不是1, 2, 3, 或4时执行的代码");
                                break;
                        }
                    }
                }
                #endregion
               

            });

            #endregion

            #region Write
            //using (var Client = new TcpClient("127.0.0.1", 502))
            //{
            //    var factory = new ModbusFactory();
            //    var master = factory.CreateMaster(Client);

            //    #region 写入单线圈 --01 Read Coils (0x)
            //    master.WriteSingleCoil(1, 1, true);
            //    Console.WriteLine("写入成功！！");
            //    Console.ReadKey();
            //    #endregion

            //    #region 写入多线圈 --15 Write Multiple Coil) 不知道为什么不行
            //    master.WriteMultipleCoils(1, 0, new bool[] { true, true, false, true });
            //    Console.WriteLine("写入成功！！");
            //    Console.ReadKey();
            //    #endregion

            //    #region 写入单寄存器 --03 Read Holding Registers (4x)
            //    master.WriteSingleRegister(1, 0, 22);
            //    Console.WriteLine("写入成功！！");
            //    Console.ReadKey();
            //    #endregion

            //    #region 写入多寄存器--03 Read Holding Registers (4x)
            //    master.WriteMultipleRegisters(1, (ushort)2, new ushort[] { 22, 33, 44, 55 });
            //    Console.WriteLine("写入成功！！");
            //    Console.ReadKey();
            //    #endregion
            //}
            #endregion

        }
    }
}
