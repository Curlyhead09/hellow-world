using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Cursor
{
    public partial class Form1 : Form
    {

        ListBox listBoxCursors = new ListBox();
        Panel testZone = new Panel();

        public Form1()
        {
            InitializeComponent();


            this.Text = "Cursor example";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(200, 200);
            this.ClientSize = new System.Drawing.Size(360, 240);

            this.listBoxCursors.Parent = this;
            this.listBoxCursors.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            this.listBoxCursors.Bounds = new System.Drawing.Rectangle(20, 20, 150, 200);
            this.listBoxCursors.Items.AddRange(new CursorNamePair[] {
        new CursorNamePair(Cursors.AppStarting, "AppStarting"),
        new CursorNamePair(Cursors.Arrow, "Arrow"),
        new CursorNamePair(Cursors.Cross, "Cross"),
        new CursorNamePair(Cursors.Default, "Default"),
        new CursorNamePair(Cursors.Hand, "Hand"),
        new CursorNamePair(Cursors.Help, "Help"),
        new CursorNamePair(Cursors.HSplit, "HSplit"),
        new CursorNamePair(Cursors.IBeam, "IBeam"),
        new CursorNamePair(Cursors.No, "No"),
        new CursorNamePair(Cursors.NoMove2D, "NoMove2D"),
        new CursorNamePair(Cursors.NoMoveHoriz, "NoMoveHoriz"),
        new CursorNamePair(Cursors.NoMoveVert, "NoMoveVert"),
        new CursorNamePair(Cursors.PanEast, "PanEast"),
        new CursorNamePair(Cursors.PanNE, "PanNE"),
        new CursorNamePair(Cursors.PanNorth, "PanNorth"),
        new CursorNamePair(Cursors.PanNW, "PanNW"),
        new CursorNamePair(Cursors.PanSE, "PanSE"),
        new CursorNamePair(Cursors.PanSouth, "PanSouth"),
        new CursorNamePair(Cursors.PanSW, "PanSW"),
        new CursorNamePair(Cursors.PanWest, "PanWest"),
        new CursorNamePair(Cursors.SizeAll, "SizeAll"),
        new CursorNamePair(Cursors.SizeNESW, "SizeNESW"),
        new CursorNamePair(Cursors.SizeNS, "SizeNS"),
        new CursorNamePair(Cursors.SizeNWSE, "SizeNWSE"),
        new CursorNamePair(Cursors.SizeWE, "SizeWE"),
        new CursorNamePair(Cursors.UpArrow, "UpArrow"),
        new CursorNamePair(Cursors.VSplit, "VSplit"),
        new CursorNamePair(Cursors.WaitCursor, "WaitCursor")
      });

            this.listBoxCursors.Click += delegate (object sender, EventArgs e) {
                this.testZone.Cursor = (this.listBoxCursors.SelectedItem as CursorNamePair).Cursor;
                //this.Cursor = (this.listBoxCursors.SelectedItem as CursorNamePair).Cursor;

            };

            this.listBoxCursors.SelectedIndex = 3;

            this.testZone.Parent = this;
            this.testZone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            this.testZone.BorderStyle = BorderStyle.Fixed3D;
            this.testZone.BackColor = System.Drawing.SystemColors.Window;
            //this.testZone.BackColor = Color.Black;
            this.testZone.Bounds = new System.Drawing.Rectangle(190, 20, 150, 200);

        }

        private class CursorNamePair : object
        {
            public CursorNamePair() { }
            public CursorNamePair(System.Windows.Forms.Cursor cursor, string name)
            {
                this.cursor = cursor;
                this.name = name;
            }

            public System.Windows.Forms.Cursor Cursor
            {
                get { return this.cursor; }
            }

            public string Name
            {
                get { return this.name; }
            }

            public override string ToString() { return this.Name; }

            private System.Windows.Forms.Cursor cursor = Cursors.Default;
            private string name = "Default";
        }

    }
}
