using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace client
{
    public partial class pic : Form
    {
        public pic()
        {
            InitializeComponent();
        }

        // Hàm xử lý chụp ảnh
        public void lam()
        {
            // Đảm bảo rằng PictureBox không chứa ảnh cũ trước khi chụp ảnh mới
            picture.Image = null;

            // Tiến hành chụp ảnh và nhận dữ liệu từ server
            String s = "TAKE";
            Program.nw.WriteLine(s); Program.nw.Flush();
            s = Program.nr.ReadLine();
            Byte[] data = new Byte[Convert.ToInt32(s)];
            Program.client.Receive(data);

            // Chuyển đổi byte[] thành ảnh và hiển thị lên PictureBox
            MemoryStream ms = new MemoryStream(data);
            picture.Image = Bitmap.FromStream(ms);
        }


        // Xử lý khi nhấn nút "Lưu"
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "Png files (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                picture.Image.Save(save.FileName, ImageFormat.Png);
                MessageBox.Show("Ảnh đã được lưu thành công!");

                // Đóng form và quay lại màn hình client
                this.Close();  // Đóng form 'pic'
            }
        }

        // Đảm bảo khi đóng form, bạn không bỏ qua việc xử lý sự kiện này
        private void pic_closing(object sender, FormClosingEventArgs e)
        {
            // Chỉ gửi lệnh QUIT khi người dùng thực sự muốn đóng kết nối
            // Nếu không, chỉ cần không làm gì hoặc chỉ đóng form mà không ảnh hưởng đến kết nối
            // String s = "QUIT";
            // Program.nw.WriteLine(s);  // Đừng gửi QUIT khi form pic đóng lại
            // Program.nw.Flush();
        }


        private void picture_Click(object sender, EventArgs e)
        {

        }
    }
}
