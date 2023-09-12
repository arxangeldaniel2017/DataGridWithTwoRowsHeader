namespace DataGridWithTwoRowsHeader
{
    public partial class Form1 : Form
    {
        private int[] daysInMonths;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var users = new List<User>() { 
                new User() { Name = "Elena" },
                new User() { Name = "Daniel" }
            };
            dataGridView1.DataSource = users;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("JanWin", "Win111");
            this.dataGridView1.Columns.Add("FebWin", "Win22");
            this.dataGridView1.Columns.Add("MarWin", "Win");
            this.dataGridView1.Columns.Add("AprWin", "Win");
            this.dataGridView1.Columns.Add("MayWin", "Win");

            for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
            {
                this.dataGridView1.Columns[j].Width = 120;
            }

            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 2;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
            this.dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);
            this.dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);
            this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
        }

        void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;
            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;
            this.dataGridView1.Invalidate(rtHeader);
        }

        void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;
            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;
            this.dataGridView1.Invalidate(rtHeader);
        }

        void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            string[] monthes = { "January", "February", "March", "April", "May" };

            for (int j = 0; j < 5;)
            {
                Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);
                int w2 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true).Width;
                r1.X += 1;
                r1.Y += 1;
                r1.Width = (r1.Width + w2) / 2 - 2;
                r1.Height = r1.Height / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(monthes[j],

                this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor),
                    r1,
                    format);
                j += 1;
            }
        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;

                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
        }
    }
}