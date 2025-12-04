using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace client
{
    public partial class client : Form
    {
        public client()
        {
            InitializeComponent();
        }

        // Hàm kiểm tra kết nối
        private bool CheckConnection()
        {
            if (Program.client == null)
            {
                MessageBox.Show("Chưa kết nối đến server");
                return false;
            }
            return true;
        }

        // Hàm gửi lệnh
        private void SendCommand(string command)
        {
            if (CheckConnection())
            {
                Program.nw.WriteLine(command);
                Program.nw.Flush();
            }
        }

        // Xử lý sự kiện "App Running"
        private void butApp_Click(object sender, EventArgs e)
        {
            SendCommand("APPLICATION");
            new listApp().ShowDialog();
        }

        // Kết nối đến server
        private void butConnect_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse(txtIP.Text), 5656);
                Program.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Program.client.Connect(ipServer);
                Program.ns = new NetworkStream(Program.client);
                Program.nr = new StreamReader(Program.ns);
                Program.nw = new StreamWriter(Program.ns);
                MessageBox.Show("Kết nối đến server thành công");
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối đến server");
                Program.client = null;
            }
        }

        // Tắt máy
        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand("SHUTDOWN");
            Program.client = null;
        }

        // Sửa registry
        private void butReg_Click(object sender, EventArgs e)
        {
            SendCommand("REGISTRY");
            new registry().ShowDialog();
        }

        // Thoát ứng dụng
        private void butExit_Click(object sender, EventArgs e)
        {
            SendCommand("QUIT");
            Application.Exit();
        }

        // Chụp màn hình
        private void butPic_Click(object sender, EventArgs e)
        {
            if (Program.client == null)
            {
                MessageBox.Show("Chưa kết nối đến server");
                return;
            }
            String s = "TAKEPIC";
            Program.nw.WriteLine(s);
            Program.nw.Flush();

            pic ViewApp = new pic();
            ViewApp.lam();  // Chụp ảnh
            ViewApp.ShowDialog();  // Mở form lưu ảnh
        }


        // Keylog
        private void butKeyLock_Click(object sender, EventArgs e)
        {
            SendCommand("KEYLOG");
            new keylog().ShowDialog();
        }

        // Đóng ứng dụng
        private void client_Closing(object sender, FormClosingEventArgs e)
        {
            SendCommand("QUIT");
        }

        // Quản lý process
        private void butProcess_Click(object sender, EventArgs e)
        {
            SendCommand("PROCESS");
            new process().ShowDialog();
        }
        private void client_Load(object sender, EventArgs e)
        {
            // Thực hiện các công việc khi form được tải
            // Ví dụ: khởi tạo kết nối, cấu hình giao diện...
        }
    }
}
