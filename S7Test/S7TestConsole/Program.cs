// See https://aka.ms/new-console-template for more information
using S7.Net;

using(Plc plc = new Plc(CpuType.S71200,"111.111.111.111",0,0))
{
	try
	{
        // 2. 打开连接
        plc.Open();
        if(plc.IsConnected)
        {
            Console.WriteLine("PLC连接成功！！");
        }
        else
        {
            Console.WriteLine("PLC连接失败！！");
        }
        // 3. 写入 DB10.1 的布尔值
        plc.Write("DB10.DBX1.0", true); // 写入 True 到 DB10.DBX1.0

        // 4. 读取 DB10.1 的布尔值
        bool value = (bool)plc.Read("DB10.DBX1.0");
        Console.WriteLine($"读取到 DB10.DBX1.0 的值: {value}");

        // 5. 关闭连接
        plc.Close();
    }
	catch (Exception ex)
	{

        Console.WriteLine($"操作失败: {ex.Message}");
    }
}
