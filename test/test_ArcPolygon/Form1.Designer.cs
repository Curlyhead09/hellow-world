namespace test_ArcPolygon
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.x_1 = new System.Windows.Forms.TextBox();
            this.x_2 = new System.Windows.Forms.TextBox();
            this.y_1 = new System.Windows.Forms.TextBox();
            this.y_2 = new System.Windows.Forms.TextBox();
            this.x_3 = new System.Windows.Forms.TextBox();
            this.y_3 = new System.Windows.Forms.TextBox();
            this.x_4 = new System.Windows.Forms.TextBox();
            this.y_4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radius = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // x_1
            // 
            this.x_1.Location = new System.Drawing.Point(63, 13);
            this.x_1.Name = "x_1";
            this.x_1.Size = new System.Drawing.Size(100, 21);
            this.x_1.TabIndex = 0;
            this.x_1.Text = "-20";
            // 
            // x_2
            // 
            this.x_2.Location = new System.Drawing.Point(63, 40);
            this.x_2.Name = "x_2";
            this.x_2.Size = new System.Drawing.Size(100, 21);
            this.x_2.TabIndex = 1;
            this.x_2.Text = "0";
            // 
            // y_1
            // 
            this.y_1.Location = new System.Drawing.Point(169, 13);
            this.y_1.Name = "y_1";
            this.y_1.Size = new System.Drawing.Size(100, 21);
            this.y_1.TabIndex = 2;
            this.y_1.Text = "2";
            // 
            // y_2
            // 
            this.y_2.Location = new System.Drawing.Point(169, 40);
            this.y_2.Name = "y_2";
            this.y_2.Size = new System.Drawing.Size(100, 21);
            this.y_2.TabIndex = 3;
            this.y_2.Text = "0";
            // 
            // x_3
            // 
            this.x_3.Location = new System.Drawing.Point(321, 13);
            this.x_3.Name = "x_3";
            this.x_3.Size = new System.Drawing.Size(100, 21);
            this.x_3.TabIndex = 4;
            this.x_3.Text = "2";
            // 
            // y_3
            // 
            this.y_3.Location = new System.Drawing.Point(427, 12);
            this.y_3.Name = "y_3";
            this.y_3.Size = new System.Drawing.Size(100, 21);
            this.y_3.TabIndex = 5;
            this.y_3.Text = "0";
            // 
            // x_4
            // 
            this.x_4.Location = new System.Drawing.Point(321, 40);
            this.x_4.Name = "x_4";
            this.x_4.Size = new System.Drawing.Size(100, 21);
            this.x_4.TabIndex = 6;
            this.x_4.Text = "0";
            // 
            // y_4
            // 
            this.y_4.Location = new System.Drawing.Point(427, 40);
            this.y_4.Name = "y_4";
            this.y_4.Size = new System.Drawing.Size(100, 21);
            this.y_4.TabIndex = 7;
            this.y_4.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(533, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(63, 90);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(100, 21);
            this.radius.TabIndex = 9;
            this.radius.Text = "0.5";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 332);
            this.panel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 479);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radius);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.y_4);
            this.Controls.Add(this.x_4);
            this.Controls.Add(this.y_3);
            this.Controls.Add(this.x_3);
            this.Controls.Add(this.y_2);
            this.Controls.Add(this.y_1);
            this.Controls.Add(this.x_2);
            this.Controls.Add(this.x_1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox x_1;
        private System.Windows.Forms.TextBox x_2;
        private System.Windows.Forms.TextBox y_1;
        private System.Windows.Forms.TextBox y_2;
        private System.Windows.Forms.TextBox x_3;
        private System.Windows.Forms.TextBox y_3;
        private System.Windows.Forms.TextBox x_4;
        private System.Windows.Forms.TextBox y_4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox radius;
        private System.Windows.Forms.Panel panel1;
    }
}

