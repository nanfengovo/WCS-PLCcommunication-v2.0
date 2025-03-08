namespace ModbusTcpAPI.Controllers
{
    public class ModbusTCPConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? ProxyName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public byte SlaveID { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public byte FunctionCode { get; set; }


        /// <summary>
        /// 变量地址
        /// </summary>
        public ushort StartAddress { get; set; }

        /// <summary>
        /// 个数
        /// </summary>
        public ushort Num { get; set; }

    }
}
