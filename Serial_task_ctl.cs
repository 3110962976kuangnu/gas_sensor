using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gas_sensor.Model;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Windows;
using System.ComponentModel;

namespace gas_sensor
{
    class One_task
    {
        public bool Is_wait_receive = false;
        public byte[] request_cmd;
        public Func<byte[], bool> parse_fun;

        public One_task(byte[] request_cmd, Func<byte[], bool> parse_fun)
        {
            this.request_cmd = request_cmd;
            if (parse_fun == null) { throw new Exception("parse_fun mast not null"); }
            this.parse_fun = parse_fun;
        }
    }
    internal class Serial_task_ctl(SerialPort serialPort)
    {
        private SerialPort sp = serialPort;
        //private Queue<One_task> TaskList = new();
        private BlockingCollection<One_task> TaskList = [];
        BackgroundWorker worker;

        public bool Add_task(byte[] send_cmd, Func<byte[], bool> parse_rec_data)
        {
            One_task one_Task = new One_task(send_cmd, parse_rec_data);
            TaskList.Add(one_Task);
            return true;
        }

        #region 串口阻塞超时接收
        /// <summary>
        /// 阻塞方式读取串口内容
        /// </summary>
        /// <param name="idleTimeout">空闲超时时长</param>
        /// <param name="maxWaitTimeout"> 最大超时时长</param>
        /// <returns> 从串口中接收的内容</returns>
        public byte[] ReadFromSerialPort(int idleTimeout, int maxWaitTimeout)
        {
            // 设置串口接收的空闲超时
            sp.ReadTimeout = idleTimeout;
            // 使用 List<byte> 来存储接收到的字节
            List<byte> receivedData = new List<byte>();
            DateTime startTime = DateTime.Now;
            DateTime lastread_time = startTime;
            try
            {
                while (true)
                {
                    // 检查是否超时
                    if ((DateTime.Now - startTime).TotalMilliseconds > maxWaitTimeout)
                    {
                        throw new TimeoutException("最大等待超时已达到");
                    }
                    if ((DateTime.Now - lastread_time).TotalMilliseconds > idleTimeout)
                    {
                        throw new TimeoutException("空闲超时以达到");
                    }

                    // 检查是否有可读取的字节
                    if (sp.BytesToRead > 0)
                    {
                        // 读取字节
                        byte[] buffer = new byte[sp.BytesToRead];
                        int bytesRead = sp.Read(buffer, 0, buffer.Length);
                        receivedData.AddRange(buffer);
                        lastread_time = DateTime.Now; // 重置开始时间
                    }
                    else
                    {
                        // 等待一段时间后继续读取
                        Thread.Sleep(1);
                    }
                }
            }
            catch (TimeoutException)
            {
                // 空闲超时异常处理
                // 可以选择返回已接收到的数据或进行其他处理
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine("发生异常: " + ex.Message);
            }
            finally
            {
                // 这里可以添加是否需要关闭串口的逻辑
            }

            return receivedData.ToArray();
        }
        #endregion

        void serial_ctl_poll()
        {
            foreach (var one_Task in TaskList.GetConsumingEnumerable())
            {
                if (!sp.IsOpen)
                {
                    MessageBox.Show("请打开串口");
                    TaskList = new BlockingCollection<One_task>();
                    continue;
                }

                sp.Write(one_Task.request_cmd, 0, one_Task.request_cmd.Length);
                byte[] bytes = ReadFromSerialPort(5, 5000);
                if ((one_Task.parse_fun != null)
                    && bytes.Length != 0)
                {
                    one_Task.parse_fun(bytes);
                }
                Thread.Sleep(1);
            }
        }

    }

}
