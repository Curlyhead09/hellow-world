
namespace Test_loadModel
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
            this.model1 = new devDept.Eyeshot.Model();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.simulation1 = new devDept.Eyeshot.Simulation();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulation1)).BeginInit();
            this.SuspendLayout();
            // 
            // model1
            // 
            this.model1.Cursor = System.Windows.Forms.Cursors.Default;
            this.model1.Dock = System.Windows.Forms.DockStyle.Left;
            this.model1.LayoutMode = devDept.Eyeshot.viewportLayoutType.None;
            this.model1.Location = new System.Drawing.Point(0, 0);
            this.model1.Name = "model1";
            this.model1.Size = new System.Drawing.Size(250, 730);
            this.model1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(585, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(585, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // simulation1
            // 
            this.simulation1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simulation1.Cursor = System.Windows.Forms.Cursors.Default;
            this.simulation1.LayoutMode = devDept.Eyeshot.viewportLayoutType.None;
            this.simulation1.Location = new System.Drawing.Point(0, 0);
            this.simulation1.Name = "simulation1";
            this.simulation1.Size = new System.Drawing.Size(200, 200);
            this.simulation1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 730);
            this.Controls.Add(this.model1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.model1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulation1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private devDept.Eyeshot.Model model1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private devDept.Eyeshot.Simulation simulation1;
    }
}

