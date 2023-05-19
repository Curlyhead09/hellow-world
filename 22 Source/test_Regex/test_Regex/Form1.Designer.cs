
namespace test_Regex
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
            this.btnStrPattern = new System.Windows.Forms.Button();
            this.btnMetaCharacter = new System.Windows.Forms.Button();
            this.btnStrSplit = new System.Windows.Forms.Button();
            this.btnMatchGroup = new System.Windows.Forms.Button();
            this.btnNamedGroup = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnValidEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStrPattern
            // 
            this.btnStrPattern.Location = new System.Drawing.Point(12, 12);
            this.btnStrPattern.Name = "btnStrPattern";
            this.btnStrPattern.Size = new System.Drawing.Size(136, 23);
            this.btnStrPattern.TabIndex = 0;
            this.btnStrPattern.Text = "String pattern";
            this.btnStrPattern.UseVisualStyleBackColor = true;
            this.btnStrPattern.Click += new System.EventHandler(this.btnStrPattern_Click);
            // 
            // btnMetaCharacter
            // 
            this.btnMetaCharacter.Location = new System.Drawing.Point(12, 50);
            this.btnMetaCharacter.Name = "btnMetaCharacter";
            this.btnMetaCharacter.Size = new System.Drawing.Size(136, 23);
            this.btnMetaCharacter.TabIndex = 1;
            this.btnMetaCharacter.Text = "Meta Charecter";
            this.btnMetaCharacter.UseVisualStyleBackColor = true;
            this.btnMetaCharacter.Click += new System.EventHandler(this.btnMetaCharacter_Click);
            // 
            // btnStrSplit
            // 
            this.btnStrSplit.Location = new System.Drawing.Point(12, 88);
            this.btnStrSplit.Name = "btnStrSplit";
            this.btnStrSplit.Size = new System.Drawing.Size(136, 23);
            this.btnStrSplit.TabIndex = 2;
            this.btnStrSplit.Text = "String Split";
            this.btnStrSplit.UseVisualStyleBackColor = true;
            this.btnStrSplit.Click += new System.EventHandler(this.btnStrSplit_Click);
            // 
            // btnMatchGroup
            // 
            this.btnMatchGroup.Location = new System.Drawing.Point(12, 146);
            this.btnMatchGroup.Name = "btnMatchGroup";
            this.btnMatchGroup.Size = new System.Drawing.Size(136, 23);
            this.btnMatchGroup.TabIndex = 3;
            this.btnMatchGroup.Text = "Match Groups";
            this.btnMatchGroup.UseVisualStyleBackColor = true;
            this.btnMatchGroup.Click += new System.EventHandler(this.btnMatchGroup_Click);
            // 
            // btnNamedGroup
            // 
            this.btnNamedGroup.Location = new System.Drawing.Point(12, 175);
            this.btnNamedGroup.Name = "btnNamedGroup";
            this.btnNamedGroup.Size = new System.Drawing.Size(136, 23);
            this.btnNamedGroup.TabIndex = 4;
            this.btnNamedGroup.Text = "Named Groups";
            this.btnNamedGroup.UseVisualStyleBackColor = true;
            this.btnNamedGroup.Click += new System.EventHandler(this.btnNamedGroup_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(12, 245);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(136, 23);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnValidEmail
            // 
            this.btnValidEmail.Location = new System.Drawing.Point(12, 331);
            this.btnValidEmail.Name = "btnValidEmail";
            this.btnValidEmail.Size = new System.Drawing.Size(136, 23);
            this.btnValidEmail.TabIndex = 6;
            this.btnValidEmail.Text = "Valid Email";
            this.btnValidEmail.UseVisualStyleBackColor = true;
            this.btnValidEmail.Click += new System.EventHandler(this.btnValidEmail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnValidEmail);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnNamedGroup);
            this.Controls.Add(this.btnMatchGroup);
            this.Controls.Add(this.btnStrSplit);
            this.Controls.Add(this.btnMetaCharacter);
            this.Controls.Add(this.btnStrPattern);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStrPattern;
        private System.Windows.Forms.Button btnMetaCharacter;
        private System.Windows.Forms.Button btnStrSplit;
        private System.Windows.Forms.Button btnMatchGroup;
        private System.Windows.Forms.Button btnNamedGroup;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnValidEmail;
    }
}

