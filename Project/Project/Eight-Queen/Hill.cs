using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Eight_Queen
{
    public partial class Hill : Form
    {
        
        int i, j, n, size;
        int[] x = new int[30];
        bool[] a = new bool[100];
        bool[] b = new bool[100];
        bool[] c = new bool[100];
        int solution_i = 1;
        int count_back = 0;
        int demHau = 0;
        int[] y = new int[100];

        int cot;
        int dong;

        int[] nmang;
        static int SIZE = 8;
        static string chuoi;
        static int pos;
            
        public static int[] queenStartCord { get; private set; }
        public static void SetqueenStartCord(int[] queenStart)
        { queenStartCord = queenStart; }
        int[,] board = new int[SIZE, SIZE];
        int cost = 0;

        int minValue = int.MaxValue; // 
        int count = 0;
        
        public Hill()
        {
            InitializeComponent();
            foreach (var pb in this.Controls.OfType<PictureBox>()) pb.BackColor = Color.Transparent;

        }

        
        public int CostCalculate(List<int> temp)
        {
            if (temp.Count > 1)
            {
                for (int i = 1; i < temp.Count; i++)
                {
                    if (temp[0] == temp[i]) // Row check
                    {
                        cost++;
                    }
                    if (Math.Abs(temp[0] - temp[i]) == Math.Abs(0 - (i)))
                    {
                        cost++;
                    }

                }
                temp.RemoveAt(0);
                CostCalculate(temp);
            }
            return cost;
        }

        
        public async Task CheckSquares(int[] array)
        {
            count++;

            for (int i = 0; i < SIZE; i++) // Column
            {
                for (int j = 0; j < SIZE; j++) // Row
                {
                    cost = 0;
                    if (array[i] != j)
                    {
                        var myList = array.ToList();
                        myList[i] = j;
                        board[j, i] = CostCalculate(myList);
                    }
                    else
                    {
                        board[j, i] = 99;
                    }
                }
            }

            
            var myCord = FindMin(board);

            
            int number = GetRandom(myCord.Count);

            
            queenStartCord[myCord[number + 1]] = myCord[number];

            await Task.Delay(10);
            await AllImageBoxDisable();
            await ImageBoxEnable(queenStartCord);


        }

        
        public List<int> FindMin(int[,] board)
        {
            minValue = int.MaxValue;
            var minCord = new List<int>();
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    if (board[row, col] == minValue)
                    {

                        minCord.Add(row);
                        minCord.Add(col);

                    }
                    if (board[row, col] < minValue)
                    {
                        minValue = board[row, col];

                        minCord.Clear();
                        minCord.Add(row);
                        minCord.Add(col);

                    }

                }

            }

            return minCord;
        }

        
        public int GetRandom(int length)
        {
            Random rnd = new Random();
            int number = 2 * rnd.Next(0, length / 2);
            return number;

        }

        Button[,] arr = null;
        Button last = null;
        int imang = 0;
        public void ChonViTri()
        {
            try
            {
                imang = 0;
                int sl = int.Parse(cboSoLuong.Text);

                if (cboSoLuong.Text == "8")
                {
                    chessboard8.Visible = true;
                    chessboard7.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                    pnViTri.Width = 384;
                    pnViTri.Height = 384;
                }
                if (cboSoLuong.Text == "7")
                {
                    chessboard8.Visible = false;
                    chessboard7.Visible = true;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                    pnViTri.Width = 336;
                    pnViTri.Height = 336;
                }
                if (cboSoLuong.Text == "6")
                {
                    chessboard8.Visible = false;
                    chessboard7.Visible = false;
                    chessboard6.Visible = true;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                    pnViTri.Width = 288;
                    pnViTri.Height = 288;
                }
                if (cboSoLuong.Text == "5")
                {
                    chessboard8.Visible = false;
                    chessboard7.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = true;
                    chessboard4.Visible = false;
                    pnViTri.Width = 240;
                    pnViTri.Height = 240;
                }
                if (cboSoLuong.Text == "4")
                {
                    chessboard8.Visible = false;
                    chessboard7.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = true;
                    pnViTri.Width = 192;
                    pnViTri.Height = 192;
                }

                queenStartCord = new int[sl];
                for (int i = 0; i < sl; i++)
                {
                    queenStartCord[i] = 0;
                }
                int k = 0;
                int SoLuong = int.Parse(cboSoLuong.Text);
                arr = new Button[SoLuong, SoLuong];
                pnViTri.Controls.Clear();
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        k++;
                        Button btn = new Button();
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Text = k + "";
                        btn.Width = btn.Height = 48;
                        btn.Location = new Point(j * btn.Width, i * btn.Height);
                        pnViTri.Controls.Add(btn);
                        arr[i, j] = btn;
                        if (j % 2 == 0 && i % 2 != 0 || i % 2 == 0 && j % 2 != 0)
                        {
                            btn.BackColor = Color.White;

                        }


                        btn.Click += Btn_Click;



                        btn.Tag = i + ";" + j;
                    }
                    k = 0;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose size of board first !");
            }
        }
        private void btnChonViTri_Click(object sender, EventArgs e)
        {
            ChonViTri();
        }
        static string vitri01;
        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                last = btn;
                last.BackgroundImage = Image.FromFile("../../Resources/Qeen_1.png");
                last.BackgroundImageLayout = ImageLayout.Stretch;
                string vitri = last.Tag.ToString();
                vitri01 += vitri[0].ToString();
                vitri01 += vitri[2].ToString();
                //MessageBox.Show(vitri[2].ToString());
                queenStartCord[Convert.ToInt32(vitri[0].ToString())] = Convert.ToInt32(vitri[2].ToString());

                last.Text = "";
                imang++;
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose invalid quantity !");
                ChonViTri();
            }

        }


        Stopwatch st = new Stopwatch();
        
        private async void button7_Click(object sender, EventArgs e)
        {
            try
            {
                pnViTri.Visible = false;
                lstngon.Items.Clear();
                try
                {
                    SIZE = Convert.ToInt32(cboSoLuong.Text);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                if (SIZE == 8)
                {
                    chessboard8.Visible = true;
                    //chessboard8.Image = Image.FromFile("../../Resources/chessboard.jpg");
                    chessboard7.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 7)
                {
                    chessboard7.Visible = true;
                    chessboard7.BackgroundImage = Image.FromFile("../../Resources/board7.png");
                    chessboard7.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 6)
                {
                    chessboard6.Visible = true;
                    chessboard6.BackgroundImage = Image.FromFile("../../Resources/board6.png");
                    chessboard6.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard7.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 5)
                {
                    chessboard5.Visible = true;
                    chessboard5.BackgroundImage = Image.FromFile("../../Resources/board5.png");
                    chessboard5.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard7.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 4)
                {
                    chessboard4.Visible = true;
                    chessboard4.BackgroundImage = Image.FromFile("../../Resources/board4.png");
                    chessboard4.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard7.Visible = false;
                }
                lbl_log.Text = "Starting...";
                board = new int[SIZE, SIZE];
                cost = 0;
                minValue = int.MaxValue;
                count = 0;
                queenStartCord = new int[SIZE];  //row ;




                if (Convert.ToInt32(cboSoLuong.Text) == 8)
                {


                    queenStartCord[0] = Convert.ToInt32(textBox1.Text)-1;
                    queenStartCord[1] = Convert.ToInt32(textBox2.Text)-1;
                    queenStartCord[2] = Convert.ToInt32(textBox3.Text)-1;
                    queenStartCord[3] = Convert.ToInt32(textBox4.Text)-1;
                    queenStartCord[4] = Convert.ToInt32(textBox5.Text)-1;
                    queenStartCord[5] = Convert.ToInt32(textBox6.Text)-1;
                    queenStartCord[6] = Convert.ToInt32(textBox7.Text)-1;
                    queenStartCord[7] = Convert.ToInt32(textBox8.Text)-1;
                }
                if (Convert.ToInt32(cboSoLuong.Text) == 7)
                {
                    queenStartCord[0] = Convert.ToInt32(textBox1.Text)-1;
                    queenStartCord[1] = Convert.ToInt32(textBox2.Text)-1;
                    queenStartCord[2] = Convert.ToInt32(textBox3.Text)-1;
                    queenStartCord[3] = Convert.ToInt32(textBox4.Text)-1;
                    queenStartCord[4] = Convert.ToInt32(textBox5.Text)-1;
                    queenStartCord[5] = Convert.ToInt32(textBox6.Text)-1;
                    queenStartCord[6] = Convert.ToInt32(textBox7.Text)-1;
                }
                if (Convert.ToInt32(cboSoLuong.Text) == 6)
                {
                    queenStartCord[0] = Convert.ToInt32(textBox1.Text)-1;
                    queenStartCord[1] = Convert.ToInt32(textBox2.Text)-1;
                    queenStartCord[2] = Convert.ToInt32(textBox3.Text)-1;
                    queenStartCord[3] = Convert.ToInt32(textBox4.Text)-1;
                    queenStartCord[4] = Convert.ToInt32(textBox5.Text)-1;
                    queenStartCord[5] = Convert.ToInt32(textBox6.Text)-1;

                }
                if (Convert.ToInt32(cboSoLuong.Text) == 5)
                {
                    queenStartCord[0] = Convert.ToInt32(textBox1.Text)-1;
                    queenStartCord[1] = Convert.ToInt32(textBox2.Text)-1;
                    queenStartCord[2] = Convert.ToInt32(textBox3.Text)-1;
                    queenStartCord[3] = Convert.ToInt32(textBox4.Text)-1;
                    queenStartCord[4] = Convert.ToInt32(textBox5.Text)-1;

                }
                if (Convert.ToInt32(cboSoLuong.Text) == 4)
                {
                    queenStartCord[0] = Convert.ToInt32(textBox1.Text)-1;
                    queenStartCord[1] = Convert.ToInt32(textBox2.Text)-1;
                    queenStartCord[2] = Convert.ToInt32(textBox3.Text)-1;
                    queenStartCord[3] = Convert.ToInt32(textBox4.Text)-1;


                }


                await AllImageBoxDisable();
                await ImageBoxEnable(queenStartCord);
                await Task.Delay(2000);
                lbl_log.Text = "Finding...";
                while (minValue != 0)
                {
                    st.Start();
                    await CheckSquares(queenStartCord);
                    st.Stop();
                    await Task.Delay(1);
                    lbl_cost.Text = minValue.ToString();

                }
                lbl_log.Text = "Success";
                lbtime.Text = st.Elapsed.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again !");
            }


        }

        
        private async void Random_Click(object sender, EventArgs e)
        {

            try
            {
                pnViTri.Visible = false;
                lstngon.Items.Clear();
                try
                {
                    SIZE = Convert.ToInt32(cboSoLuong.Text);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                if (SIZE == 8)
                {
                    chessboard8.Visible = true;
                    //chessboard8.Image = Image.FromFile("../../Resources/chessboard.jpg");
                    chessboard7.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 7)
                {
                    chessboard7.Visible = true;
                    chessboard7.BackgroundImage = Image.FromFile("../../Resources/board7.png");
                    chessboard7.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 6)
                {
                    chessboard6.Visible = true;
                    chessboard6.BackgroundImage = Image.FromFile("../../Resources/board6.png");
                    chessboard6.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard7.Visible = false;
                    chessboard5.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 5)
                {
                    chessboard5.Visible = true;
                    chessboard5.BackgroundImage = Image.FromFile("../../Resources/board5.png");
                    chessboard5.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard7.Visible = false;
                    chessboard4.Visible = false;
                }
                if (SIZE == 4)
                {
                    chessboard4.Visible = true;
                    chessboard4.BackgroundImage = Image.FromFile("../../Resources/board4.png");
                    chessboard4.BackgroundImageLayout = ImageLayout.Stretch;
                    chessboard8.Visible = false;
                    chessboard6.Visible = false;
                    chessboard5.Visible = false;
                    chessboard7.Visible = false;
                }
                lbl_log.Text = "Starting...";
                board = new int[SIZE, SIZE];
                cost = 0;
                minValue = int.MaxValue;
                count = 0;
                queenStartCord = RandomQueenPlaceGenerator();


                await AllImageBoxDisable();
                await ImageBoxEnable(queenStartCord);
                await Task.Delay(2000);
                lbl_log.Text = "Finding...";
                while (minValue != 0)
                {
                    st.Start();
                    await CheckSquares(queenStartCord);
                    st.Stop();
                    await Task.Delay(1);
                    lbl_cost.Text = minValue.ToString();

                }
                lbl_log.Text = "Success";
                lbtime.Text = st.Elapsed.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again !");
            }
        }

        
        public int[] RandomQueenPlaceGenerator()
        {
            int[] randomQueen = new int[SIZE];
            Random rnd = new Random();
            for (int i = 0; i < SIZE; i++)
            {
                randomQueen[i] = rnd.Next(0, SIZE);

            }

            return randomQueen;
        }

        
        public async Task AllImageBoxDisable()
        {
            foreach (var pb in this.Controls.OfType<PictureBox>()) pb.Visible = false;
            await Task.Delay(1);
        }

        
        public async Task ImageBoxEnable(int[] array)
        {

            string a = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {

                a = a.ToString() + '(' + i.ToString() + ',' + array[i].ToString() + ')' + ' ';

                #region 1 Row Images
                if (i == 0)
                {
                    if (array[i] == 0)
                    {
                        queen00.Visible = true;
                        queen00.BringToFront();
                    }
                    else if (array[i] == 1)
                    {
                        queen10.Visible = true;
                        queen10.BringToFront();
                    }
                    else if (array[i] == 2)
                    {
                        queen20.Visible = true;
                        queen20.BringToFront();
                    }
                    else if (array[i] == 3)
                    {
                        queen30.Visible = true;
                        queen30.BringToFront();
                    }
                    else if (array[i] == 4)
                    {
                        queen40.Visible = true;
                        queen40.BringToFront();
                    }
                    else if (array[i] == 5)
                    {
                        queen50.Visible = true;
                        queen50.BringToFront();
                    }
                    else if (array[i] == 6)
                    {
                        queen60.Visible = true;
                        queen60.BringToFront();
                    }
                    else if (array[i] == 7)
                    {
                        queen70.Visible = true;
                        queen70.BringToFront();
                    }
                }

                #endregion

                #region 2 Row Images
                if (i == 1)
                {
                    if (array[i] == 0)
                    {
                        queen01.Visible = true;
                        queen01.BringToFront();

                    }
                    else if (array[i] == 1)
                    {
                        queen11.Visible = true;
                        queen11.BringToFront();
                    }
                    else if (array[i] == 2)
                    {
                        queen21.Visible = true;
                        queen21.BringToFront();
                    }
                    else if (array[i] == 3)
                    {
                        queen31.Visible = true;
                        queen31.BringToFront();
                    }
                    else if (array[i] == 4)
                    {
                        queen41.Visible = true;
                        queen41.BringToFront();
                    }
                    else if (array[i] == 5)
                    {
                        queen51.Visible = true;
                        queen51.BringToFront();
                    }
                    else if (array[i] == 6)
                    {
                        queen61.Visible = true;
                        queen61.BringToFront();
                    }
                    else if (array[i] == 7)
                    {
                        queen71.Visible = true;
                        queen71.BringToFront();
                    }
                }

                #endregion

                #region MyRegion
                if (i == 2)
                {
                    if (array[i] == 0)
                    {
                        queen02.Visible = true;
                        queen02.BringToFront();

                    }
                    else if (array[i] == 1)
                    {
                        queen12.Visible = true;
                        queen12.BringToFront();
                    }
                    else if (array[i] == 2)
                    {
                        queen22.Visible = true;
                        queen22.BringToFront();
                    }
                    else if (array[i] == 3)
                    {
                        queen32.Visible = true;
                        queen32.BringToFront();
                    }
                    else if (array[i] == 4)
                    {
                        queen42.Visible = true;
                        queen42.BringToFront();
                    }
                    else if (array[i] == 5)
                    {
                        queen52.Visible = true;
                        queen52.BringToFront();
                    }
                    else if (array[i] == 6)
                    {
                        queen62.Visible = true;
                        queen62.BringToFront();
                    }
                    else if (array[i] == 7)
                    {
                        queen72.Visible = true;
                        queen72.BringToFront();
                    }
                }

                #endregion

                #region 3 Row Images

                else if (i == 3)
                {
                    if (array[i] == 0)
                    {
                        queen03.Visible = true;
                        queen03.BringToFront();

                    }
                    else if (array[i] == 1)
                    {
                        queen13.Visible = true;
                        queen13.BringToFront();
                    }
                    else if (array[i] == 2)
                    {
                        queen23.Visible = true;
                        queen23.BringToFront();
                    }
                    else if (array[i] == 3)
                    {
                        queen33.Visible = true;
                        queen33.BringToFront();
                    }
                    else if (array[i] == 4)
                    {
                        queen43.Visible = true;
                        queen43.BringToFront();
                    }
                    else if (array[i] == 5)
                    {
                        queen53.Visible = true;
                        queen53.BringToFront();
                    }
                    else if (array[i] == 6)
                    {
                        queen63.Visible = true;
                        queen63.BringToFront();
                    }
                    else if (array[i] == 7)
                    {
                        queen73.Visible = true;
                        queen73.BringToFront();
                    }
                }


                #endregion

                #region 4 Row Images
                else if (i == 4)
                {
                    if (array[i] == 0)
                    {
                        queen04.Visible = true;
                        queen04.BringToFront();

                    }
                    if (array[i] == 1)
                    {
                        queen14.Visible = true;
                        queen14.BringToFront();
                    }
                    if (array[i] == 2)
                    {
                        queen24.Visible = true;
                        queen24.BringToFront();
                    }
                    if (array[i] == 3)
                    {
                        queen34.Visible = true;
                        queen34.BringToFront();
                    }
                    if (array[i] == 4)
                    {
                        queen44.Visible = true;
                        queen44.BringToFront();
                    }
                    if (array[i] == 5)
                    {
                        queen54.Visible = true;
                        queen54.BringToFront();
                    }
                    if (array[i] == 6)
                    {
                        queen64.Visible = true;
                        queen64.BringToFront();
                    }
                    if (array[i] == 7)
                    {
                        queen74.Visible = true;
                        queen74.BringToFront();
                    }

                }

                #endregion

                #region 5 Row Images
                else if (i == 5)
                {

                    if (array[i] == 0)
                    {
                        queen05.Visible = true;
                        queen05.BringToFront();

                    }
                    else if (array[i] == 1)
                    {
                        queen15.Visible = true;
                        queen15.BringToFront();

                    }
                    else if (array[i] == 2)
                    {
                        queen25.Visible = true;
                        queen25.BringToFront();

                    }
                    else if (array[i] == 3)
                    {
                        queen35.Visible = true;
                        queen35.BringToFront();

                    }
                    else if (array[i] == 4)
                    {
                        queen45.Visible = true;
                        queen45.BringToFront();

                    }
                    else if (array[i] == 5)
                    {
                        queen55.Visible = true;
                        queen55.BringToFront();

                    }
                    else if (array[i] == 6)
                    {
                        queen65.Visible = true;
                        queen65.BringToFront();

                    }
                    else if (array[i] == 7)
                    {
                        queen75.Visible = true;
                        queen75.BringToFront();

                    }
                }


                #endregion

                #region 6 Row Images
                else if (i == 6)
                {
                    if (array[i] == 0)
                    {
                        queen06.Visible = true;
                        queen06.BringToFront();
                    }
                    else if (array[i] == 1)
                    {
                        queen16.Visible = true;
                        queen16.BringToFront();

                    }
                    else if (array[i] == 2)
                    {
                        queen26.Visible = true;
                        queen26.BringToFront();

                    }
                    else if (array[i] == 3)
                    {
                        queen36.Visible = true;
                        queen36.BringToFront();

                    }
                    else if (array[i] == 4)
                    {
                        queen46.Visible = true;
                        queen46.BringToFront();

                    }
                    else if (array[i] == 5)
                    {
                        queen56.Visible = true;
                        queen56.BringToFront();

                    }
                    else if (array[i] == 6)
                    {
                        queen66.Visible = true;
                        queen66.BringToFront();


                    }
                    else if (array[i] == 7)
                    {
                        queen76.Visible = true;
                        queen76.BringToFront();

                    }
                }

                #endregion

                #region 7 Row Images
                else if (i == 7)
                {

                    if (array[i] == 0)
                    {
                        queen07.Visible = true;
                        queen07.BringToFront();

                    }
                    else if (array[i] == 1)
                    {
                        queen17.Visible = true;
                        queen17.BringToFront();
                    }
                    else if (array[i] == 2)
                    {
                        queen27.Visible = true;
                        queen27.BringToFront();
                    }
                    else if (array[i] == 3)
                    {
                        queen37.Visible = true;
                        queen37.BringToFront();
                    }
                    else if (array[i] == 4)
                    {
                        queen47.Visible = true;
                        queen47.BringToFront();
                    }
                    else if (array[i] == 5)
                    {
                        queen57.Visible = true;
                        queen57.BringToFront();
                    }
                    else if (array[i] == 6)
                    {
                        queen67.Visible = true;
                        queen67.BringToFront();

                    }
                    else if (array[i] == 7)
                    {
                        queen77.Visible = true;
                        queen77.BringToFront();
                    }
                }
                #endregion


                lbl_count.Text = count.ToString();
                lbl_cordinates.Text = a;
                await Task.Delay(100);

            }
            lstngon.Items.Add(a);
            string ngat = "\n\n\n------------------------------------------------------------";
            lstngon.Items.Add(ngat);

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChonViTri();
        }

        private void Thoat(object sender, EventArgs e)
        {
            Close();
        }

        private void cboSoLuong_TextChanged(object sender, EventArgs e)
        {
            ChonViTri();



        }

        private void Hill_Load(object sender, EventArgs e)
        {
            panel1.BackgroundImage = Image.FromFile("../../Resources/board7.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.BackgroundImage = Image.FromFile("../../Resources/icon.jpg");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public async Task InBatKy()
        {
            int[] queenStartCord_new = new int[8];
            // tạo 1 cái list trước nếu cái list 
            //List<int> termsList = new List<int>();
            foreach (object item in listBox1.Items)
            {
                chuoi = item.ToString();
                //MessageBox.Show(chuoi);
            }
            for (int i = 0; i < chuoi.Length; i++)
            {
                if (chuoi[i].ToString() == "(")
                {
                    int cot = Convert.ToInt32((chuoi[i + 1]).ToString());
                    //termsList.Add(cot);
                    //MessageBox.Show(cot.ToString());
                    int dong = Convert.ToInt32((chuoi[i + 3]).ToString());
                    //termsList.Add(dong);
                    //MessageBox.Show(dong.ToString());

                    queenStartCord_new[cot] = dong;

                }
            }

            //queenStartCord_new = new int[] { 0, 2, 4, 6, 1, 3, 5, 7 };  //row ; 
            await Task.Delay(10);
            await AllImageBoxDisable();
            await ImageBoxEnable(queenStartCord_new);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InBatKy();
        }

        private void lstngon_Click(object sender, EventArgs e)
        {
            /*
            int groupid = (Int32)lstngon.SelectedValue;
            lstngon.SelectionMode = SelectionMode.MultiSimple;
            lstngon.SetSelected(1, true);
            lstngon.SetSelected(2, true);
            
            foreach (object item in lstngon.Items)
            {
                
            }
            */
            listBox1.Items.Clear();
            pos = lstngon.SelectedIndex;
            //MessageBox.Show(pos.ToString());
            lbl_cordinates.Text = Convert.ToString(lstngon.Text);
            listBox1.Items.Add(lbl_cordinates.Text);
            //MessageBox.Show((string)lstngon.SelectedValue);
            InBatKy();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                pos = pos + 2;
                lstngon.SelectedIndex = pos;
                lbl_cordinates.Text = Convert.ToString(lstngon.Text);
                listBox1.Items.Add(lbl_cordinates.Text);
                //MessageBox.Show((string)lstngon.SelectedValue);
                InBatKy();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try Again!!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                pos = pos - 2;
                lstngon.SelectedIndex = pos;
                lbl_cordinates.Text = Convert.ToString(lstngon.Text);
                listBox1.Items.Add(lbl_cordinates.Text);
                //MessageBox.Show((string)lstngon.SelectedValue);
                InBatKy();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try Again!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //pos = pos + 2;
                lstngon.SelectedIndex = 0;
                lbl_cordinates.Text = Convert.ToString(lstngon.Text);
                listBox1.Items.Add(lbl_cordinates.Text);
                //MessageBox.Show((string)lstngon.SelectedValue);
                InBatKy();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try Again!!!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (object item in lstngon.Items)
                {
                    count++;
                }
                //pos = pos + 2;
                lstngon.SelectedIndex = count - 2;
                lbl_cordinates.Text = Convert.ToString(lstngon.Text);
                listBox1.Items.Add(lbl_cordinates.Text);
                //MessageBox.Show((string)lstngon.SelectedValue);
                InBatKy();
            }
            catch (Exception)
            {
                MessageBox.Show("Please try Again!!!");
            }
        }


        static int N;

        static Boolean toPlaceOrNotToPlace(int[,] board, int row, int col)
        {
            int i, j;
            for (i = 0; i < col; i++)
            {
                if (board[row, i] == 1) return false;
            }
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 1) return false;
            }
            for (i = row, j = col; j >= 0 && i < N; i++, j--)
            {
                if (board[i, j] == 1) return false;
            }
            return true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int sl = int.Parse(cboSoLuong.Text);

            queenStartCord = new int[sl];

            int k = 0;
            int SoLuong = int.Parse(cboSoLuong.Text);
            arr = new Button[SoLuong, SoLuong];
            pnViTri.Controls.Clear();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    k++;
                    Button btn = new Button();
                    btn.Text = k + "";
                    btn.Width = btn.Height = 27;
                    btn.Location = new Point(j * btn.Width, i * btn.Height);
                    pnViTri.Controls.Add(btn);
                    arr[i, j] = btn;
                    btn.BackColor = Color.WhiteSmoke;

                    btn.Click += Btn_Click;



                    btn.Tag = i + ";" + j;
                }
                k = 0;

            }
        }



        static Boolean theBoardSolver(int[,] board, int col)
        {
            if (col >= N) return true;
            for (int i = 0; i < N; i++)
            {
                if (toPlaceOrNotToPlace(board, i, col))
                {
                    board[i, col] = 1;
                    if (theBoardSolver(board, col + 1)) return true;
                    // Backtracking is hella important in this one.  
                    board[i, col] = 0;
                }
            }
            return false;
        }

        public async Task BackTracking()
        {
            N = SIZE;

            int[,] board = new int[N, N];
            if (!theBoardSolver(board, 0))
            {
                Console.WriteLine("Solution not found.");
            }

            int[] queenStartCord_new = new int[8];
            // tạo 1 cái list trước nếu cái list 
            //List<int> termsList = new List<int>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board[i, j] == 1)
                    {
                        queenStartCord_new[i] = j;
                    }
                }
            }




            //queenStartCord_new = new int[] { 0, 2, 4, 6, 1, 3, 5, 7 };  //row ; 
            await Task.Delay(10);
            await AllImageBoxDisable();
            await ImageBoxEnable(queenStartCord_new);
        }
        public async Task InBatKy2()
        {
            int[] queenStartCord_new = new int[] { 0, 2, 4, 6, 1, 3, 5, 7 };
            // tạo 1 cái list trước nếu cái list 
            //List<int> termsList = new List<int>();
            /*
            //MessageBox.Show(vitri01);
            for (int i = 0; i < vitri01.Length/2; i++)
            {
                
                
                    //int cot = Convert.ToInt32((chuoi[i + 1]).ToString());
                    //termsList.Add(cot);
                    //MessageBox.Show(cot.ToString());
                    //int dong = Convert.ToInt32((chuoi[i + 3]).ToString());
                    //termsList.Add(dong);
                    //MessageBox.Show(dong.ToString());

                    queenStartCord_new[Convert.ToInt32(vitri01[i].ToString())] = Convert.ToInt32(vitri01[i+1].ToString());

                    
            }
            */
            //queenStartCord_new = new int[] { 0, 2, 4, 6, 1, 3, 5, 7 };  //row ; 
            await Task.Delay(10);
            await AllImageBoxDisable();
            await ImageBoxEnable(queenStartCord_new);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SIZE = Convert.ToInt32(cboSoLuong.Text);
            MessageBox.Show(SIZE.ToString());
            chessboard8.Visible = false;
            chessboard7.Visible = false;
            chessboard6.Visible = false;
            chessboard5.Visible = false;
            chessboard4.Visible = false;
            if (SIZE == 8)
            {
                chessboard8.Visible = true;
                //chessboard8.Image = Image.FromFile("../../Resources/chessboard.jpg");
                chessboard7.Visible = false;
                chessboard6.Visible = false;
                chessboard5.Visible = false;
                chessboard4.Visible = false;
            }
            if (SIZE == 7)
            {
                chessboard7.Visible = true;
                chessboard7.BackgroundImage = Image.FromFile("../../Resources/board7.png");
                chessboard7.BackgroundImageLayout = ImageLayout.Stretch;
                chessboard8.Visible = false;
                chessboard6.Visible = false;
                chessboard5.Visible = false;
                chessboard4.Visible = false;
            }
            if (SIZE == 6)
            {
                chessboard6.Visible = true;
                chessboard6.BackgroundImage = Image.FromFile("../../Resources/board6.png");
                chessboard6.BackgroundImageLayout = ImageLayout.Stretch;
                chessboard8.Visible = false;
                chessboard7.Visible = false;
                chessboard5.Visible = false;
                chessboard4.Visible = false;
            }
            if (SIZE == 5)
            {
                chessboard5.Visible = true;
                chessboard5.BackgroundImage = Image.FromFile("../../Resources/board5.png");
                chessboard5.BackgroundImageLayout = ImageLayout.Stretch;
                chessboard8.Visible = false;
                chessboard6.Visible = false;
                chessboard7.Visible = false;
                chessboard4.Visible = false;
            }
            if (SIZE == 4)
            {
                chessboard4.Visible = true;
                chessboard4.BackgroundImage = Image.FromFile("../../Resources/board4.png");
                chessboard4.BackgroundImageLayout = ImageLayout.Stretch;
                chessboard8.Visible = false;
                chessboard6.Visible = false;
                chessboard5.Visible = false;
                chessboard7.Visible = false;
            }
            Stopwatch st = new Stopwatch();
            st.Start();
            ChonViTri();
            BackTracking();
            st.Stop();
            lbtime.Text = st.Elapsed.ToString();
        }
    }
}



