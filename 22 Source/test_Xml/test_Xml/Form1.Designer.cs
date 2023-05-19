
namespace test_Xml
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
            this.btnXmlWriter = new System.Windows.Forms.Button();
            this.btnXmlReader = new System.Windows.Forms.Button();
            this.btnXmlDReader = new System.Windows.Forms.Button();
            this.btnXmlDWriter = new System.Windows.Forms.Button();
            this.btnXmlNavigator = new System.Windows.Forms.Button();
            this.btnXElementWriter = new System.Windows.Forms.Button();
            this.btnXElementReader = new System.Windows.Forms.Button();
            this.btnLinq = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnXmlWriter
            // 
            this.btnXmlWriter.Location = new System.Drawing.Point(12, 12);
            this.btnXmlWriter.Name = "btnXmlWriter";
            this.btnXmlWriter.Size = new System.Drawing.Size(75, 23);
            this.btnXmlWriter.TabIndex = 0;
            this.btnXmlWriter.Text = "Write";
            this.btnXmlWriter.UseVisualStyleBackColor = true;
            this.btnXmlWriter.Click += new System.EventHandler(this.btnXmlWriter_Click);
            // 
            // btnXmlReader
            // 
            this.btnXmlReader.Location = new System.Drawing.Point(12, 41);
            this.btnXmlReader.Name = "btnXmlReader";
            this.btnXmlReader.Size = new System.Drawing.Size(75, 23);
            this.btnXmlReader.TabIndex = 1;
            this.btnXmlReader.Text = "Read";
            this.btnXmlReader.UseVisualStyleBackColor = true;
            this.btnXmlReader.Click += new System.EventHandler(this.btnXmlReader_Click);
            // 
            // btnXmlDReader
            // 
            this.btnXmlDReader.Location = new System.Drawing.Point(62, 197);
            this.btnXmlDReader.Name = "btnXmlDReader";
            this.btnXmlDReader.Size = new System.Drawing.Size(218, 23);
            this.btnXmlDReader.TabIndex = 2;
            this.btnXmlDReader.Text = "Xml Document Read";
            this.btnXmlDReader.UseVisualStyleBackColor = true;
            this.btnXmlDReader.Click += new System.EventHandler(this.btnXmlDReader_Click);
            // 
            // btnXmlDWriter
            // 
            this.btnXmlDWriter.Location = new System.Drawing.Point(62, 168);
            this.btnXmlDWriter.Name = "btnXmlDWriter";
            this.btnXmlDWriter.Size = new System.Drawing.Size(218, 23);
            this.btnXmlDWriter.TabIndex = 3;
            this.btnXmlDWriter.Text = "Xml Document Write";
            this.btnXmlDWriter.UseVisualStyleBackColor = true;
            this.btnXmlDWriter.Click += new System.EventHandler(this.btnXmlDWriter_Click);
            // 
            // btnXmlNavigator
            // 
            this.btnXmlNavigator.Location = new System.Drawing.Point(175, 266);
            this.btnXmlNavigator.Name = "btnXmlNavigator";
            this.btnXmlNavigator.Size = new System.Drawing.Size(218, 23);
            this.btnXmlNavigator.TabIndex = 4;
            this.btnXmlNavigator.Text = "XPath Navigator";
            this.btnXmlNavigator.UseVisualStyleBackColor = true;
            this.btnXmlNavigator.Click += new System.EventHandler(this.btnXmlNavigator_Click);
            // 
            // btnXElementWriter
            // 
            this.btnXElementWriter.Location = new System.Drawing.Point(230, 311);
            this.btnXElementWriter.Name = "btnXElementWriter";
            this.btnXElementWriter.Size = new System.Drawing.Size(218, 23);
            this.btnXElementWriter.TabIndex = 5;
            this.btnXElementWriter.Text = "XElement Write";
            this.btnXElementWriter.UseVisualStyleBackColor = true;
            this.btnXElementWriter.Click += new System.EventHandler(this.btnXElementWriter_Click);
            // 
            // btnXElementReader
            // 
            this.btnXElementReader.Location = new System.Drawing.Point(230, 340);
            this.btnXElementReader.Name = "btnXElementReader";
            this.btnXElementReader.Size = new System.Drawing.Size(218, 23);
            this.btnXElementReader.TabIndex = 6;
            this.btnXElementReader.Text = "XElement Read";
            this.btnXElementReader.UseVisualStyleBackColor = true;
            this.btnXElementReader.Click += new System.EventHandler(this.btnXElementReader_Click);
            // 
            // btnLinq
            // 
            this.btnLinq.Location = new System.Drawing.Point(230, 369);
            this.btnLinq.Name = "btnLinq";
            this.btnLinq.Size = new System.Drawing.Size(218, 23);
            this.btnLinq.TabIndex = 7;
            this.btnLinq.Text = "Linq To Xml";
            this.btnLinq.UseVisualStyleBackColor = true;
            this.btnLinq.Click += new System.EventHandler(this.btnLinq_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLinq);
            this.Controls.Add(this.btnXElementReader);
            this.Controls.Add(this.btnXElementWriter);
            this.Controls.Add(this.btnXmlNavigator);
            this.Controls.Add(this.btnXmlDWriter);
            this.Controls.Add(this.btnXmlDReader);
            this.Controls.Add(this.btnXmlReader);
            this.Controls.Add(this.btnXmlWriter);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnXmlWriter;
        private System.Windows.Forms.Button btnXmlReader;
        private System.Windows.Forms.Button btnXmlDReader;
        private System.Windows.Forms.Button btnXmlDWriter;
        private System.Windows.Forms.Button btnXmlNavigator;
        private System.Windows.Forms.Button btnXElementWriter;
        private System.Windows.Forms.Button btnXElementReader;
        private System.Windows.Forms.Button btnLinq;
    }
}

