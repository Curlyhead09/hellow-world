
using System.Data;
using System.Windows.Forms;

namespace test_Binding
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";


            // Create the form and its controls.
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();

            this.text1 = new System.Windows.Forms.TextBox();
            this.text2 = new System.Windows.Forms.TextBox();
            this.text3 = new System.Windows.Forms.TextBox();

            this.DateTimePicker1 = new DateTimePicker();

            this.Text = "Binding Sample";
            this.ClientSize = new System.Drawing.Size(450, 200);

            button1.Location = new System.Drawing.Point(24, 16);
            button1.Size = new System.Drawing.Size(64, 24);
            button1.Text = "<";
            button1.Click += new System.EventHandler(button1_Click);

            button2.Location = new System.Drawing.Point(90, 16);
            button2.Size = new System.Drawing.Size(64, 24);
            button2.Text = ">";
            button2.Click += new System.EventHandler(button2_Click);

            button3.Location = new System.Drawing.Point(90, 100);
            button3.Size = new System.Drawing.Size(64, 24);
            button3.Text = "<";
            button3.Click += new System.EventHandler(button3_Click);

            button4.Location = new System.Drawing.Point(150, 100);
            button4.Size = new System.Drawing.Size(64, 24);
            button4.Text = ">";
            button4.Click += new System.EventHandler(button4_Click);

            text1.Location = new System.Drawing.Point(24, 50);
            text1.Size = new System.Drawing.Size(150, 24);

            text2.Location = new System.Drawing.Point(190, 50);
            text2.Size = new System.Drawing.Size(150, 24);

            text3.Location = new System.Drawing.Point(290, 150);
            text3.Size = new System.Drawing.Size(150, 24);

            DateTimePicker1.Location = new System.Drawing.Point(90, 150);
            DateTimePicker1.Size = new System.Drawing.Size(200, 800);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
            this.Controls.Add(button3);
            this.Controls.Add(button4);
            this.Controls.Add(text1);
            this.Controls.Add(text2);
            this.Controls.Add(text3);
            this.Controls.Add(DateTimePicker1);


        }

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox text1;
        private TextBox text2;
        private TextBox text3;

        private BindingManagerBase bmCustomers;
        private BindingManagerBase bmOrders;
        private DataSet ds;
        private DateTimePicker DateTimePicker1;

        #endregion
    }
}

