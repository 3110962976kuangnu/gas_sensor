using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gas_sensor.Model;
using System.IO.Ports;

namespace gas_sensor
{
    class One_task
    {
        public bool Is_wait_receive = false;
        public byte[] request_cmd;
        public Action<byte[]> action;

        public One_task(byte[] request_cmd, Action<byte[]> action)
        {
            this.request_cmd = request_cmd;
            this.action = action;
        }
    }
    internal class Serial_task_ctl(SerialPort serialPort)
    {
        private SerialPort SerialPort = serialPort;
        private Queue<One_task> TaskList = new Queue<One_task>();

        public bool Add_task(byte[] send_cmd, Action<byte[]> parse_rec_data)
        {
            One_task one_Task = new One_task(send_cmd, parse_rec_data);
            TaskList.Enqueue(one_Task);
            if (!TaskList.Peek().Is_wait_receive)
            {
            }

            return true;
        }
    }
}
