using System.Security.Policy;

namespace at_yarışı;

public partial class Yaris : Form
{
    public Yaris()
    {
        InitializeComponent();
        SetupDataGridView();
    }
    int no1atsolauzaklık, no2atsolauzaklık, no3atsolauzaklık, no4atsolauzaklık, no5atsolauzaklık;

    double oran1, oran2, oran3, oran4, oran5, money;

    int horse1Max, horse2Max, horse3Max, horse4Max, horse5Max;
    int horse1Min, horse2Min, horse3Min, horse4Min, horse5Min;

    byte bahis = 0, kazanan;

    Random rastgele = new Random();

    private void Yaris_Load_1(object sender, EventArgs e)
    {
        //pictureBox'ların (atların) sola olan uzaklıklarını global alanda oluşturmuş olduğum değişkenlere gönderdim.
        //Global alana gönderme sebebim Timer'ımızın "Tick" eventi altında başlangıç değerlenie (atların sola olan uzaklığına) ulaşmamız gerektiği için..

        btnRes.Enabled = true;

        no1atsolauzaklık = pictureBox1.Left;
        no2atsolauzaklık = pictureBox2.Left;
        no3atsolauzaklık = pictureBox3.Left;
        no4atsolauzaklık = pictureBox4.Left;
        no5atsolauzaklık = pictureBox5.Left;

        SetupDataGridView();

        Oran_Belirle();

        InitializeDataGridView();
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        // DataGridView'in boş olup olmadığını kontrol et
        if (dataGridViewBets.Rows.Count == 0 || dataGridViewBets.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
        {
            MessageBox.Show("Lütfen önce oyuncuları ve bahisleri ekleyin.");
            return;
        }

        //timer kontrolü gerçekleştirdim.
        // timer1.Enabled = true;

        if (timer1.Enabled == false)
        {
            timer1.Start();
        }
        else
        {
            timer1.Stop();
        }

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        int derece = Convert.ToInt32(lblzaman.Text);
        derece++;
        lblzaman.Text = derece.ToString();

        //Atların genişliklerini değişkenlerle gösterdim.
        int no1atgenislik = pictureBox1.Width;
        int no2atgenislik = pictureBox2.Width;
        int no3atgenislik = pictureBox3.Width;
        int no4atgenislik = pictureBox4.Width;
        int no5atgenislik = pictureBox5.Width;

        //Bitiş çizgimizin formun sola olan uzaklığını yakalyıp değişkene gönderdim.
        int bitisuzaklik = lblBitis.Left;

        pictureBox1.Left = pictureBox1.Left + rastgele.Next(horse1Min, horse1Max);
        pictureBox2.Left = pictureBox2.Left + rastgele.Next(horse2Min, horse2Max);
        pictureBox3.Left = pictureBox3.Left + rastgele.Next(horse3Min, horse3Max);
        pictureBox4.Left = pictureBox4.Left + rastgele.Next(horse4Min, horse4Max);
        pictureBox5.Left = pictureBox5.Left + rastgele.Next(horse5Min, horse5Max);
        #region Anlık Durumları
        //Yarış durumunu anlık olarak raporladım.
        if (pictureBox1.Left > pictureBox2.Left + 5 && pictureBox1.Left > pictureBox3.Left + 5 && pictureBox1.Left > pictureBox4.Left + 5 && pictureBox1.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "1 - Atılgan  Yarışı Önde Götürüyor!!";
        }

        if (pictureBox2.Left > pictureBox1.Left + 5 && pictureBox2.Left > pictureBox3.Left + 5 && pictureBox2.Left > pictureBox4.Left + 5 && pictureBox2.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "2 - Gülbatur  İyi Bir Atakla Öne Geçti!!";
        }

        if (pictureBox3.Left > pictureBox1.Left + 5 && pictureBox3.Left > pictureBox2.Left + 5 && pictureBox3.Left > pictureBox4.Left + 5 && pictureBox3.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "3 - Şahbatur  Rakiplerini Geçiyor!!";
        }

        if (pictureBox4.Left > pictureBox1.Left + 5 && pictureBox4.Left > pictureBox2.Left + 5 && pictureBox4.Left > pictureBox3.Left + 5 && pictureBox4.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "4 - Prens  Rakiplerini Yakaladı Ve Öne Geçti!!";
        }

        if (pictureBox5.Left > pictureBox1.Left + 5 && pictureBox5.Left > pictureBox2.Left + 5 && pictureBox5.Left > pictureBox3.Left + 5 && pictureBox5.Left > pictureBox4.Left + 5)
        {
            lblanlik.Text = "5 - Beyaz Saray  Liderliği Ele Geçirdi!!";
        }

        if (pictureBox1.Left == pictureBox2.Left && pictureBox1.Left > pictureBox3.Left + 5 && pictureBox1.Left > pictureBox4.Left + 5 && pictureBox1.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "1 - Atılgan  ve 2 - Gülbaturlar Berabere Önde";
        }

        if (pictureBox1.Left == pictureBox3.Left && pictureBox1.Left > pictureBox2.Left + 5 && pictureBox1.Left > pictureBox4.Left + 5 && pictureBox1.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "1 - Atılgan  ve 3 - Şahbaturlar Berabere Önde";
        }

        if (pictureBox1.Left == pictureBox4.Left && pictureBox1.Left > pictureBox2.Left + 5 && pictureBox1.Left > pictureBox3.Left + 5 && pictureBox1.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "1 - Atılgan  ve 4 - Prenslar Berabere Önde";
        }

        if (pictureBox1.Left == pictureBox5.Left && pictureBox1.Left > pictureBox2.Left + 5 && pictureBox1.Left > pictureBox3.Left + 5 && pictureBox1.Left > pictureBox4.Left + 5)
        {
            lblanlik.Text = "1 - Atılgan  ve 5 - Beyaz Saraylar Berabere Önde";
        }

        if (pictureBox2.Left == pictureBox3.Left + 5 && pictureBox2.Left > pictureBox1.Left + 5 && pictureBox2.Left > pictureBox4.Left + 5 && pictureBox2.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "2 - Gülbatur ve 3 - Şahbaturlar Berabere Önde";
        }

        if (pictureBox2.Left == pictureBox4.Left + 5 && pictureBox2.Left > pictureBox1.Left + 5 && pictureBox2.Left > pictureBox3.Left + 5 && pictureBox2.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "2 - Gülbatur ve 4 - Prenslar Berabere Önde";
        }

        if (pictureBox2.Left == pictureBox5.Left + 5 && pictureBox2.Left > pictureBox1.Left + 5 && pictureBox2.Left > pictureBox3.Left + 5 && pictureBox2.Left > pictureBox4.Left + 5)
        {
            lblanlik.Text = "2 - Gülbatur ve 5 - Beyaz Saraylar Berabere Önde";
        }

        if (pictureBox3.Left == pictureBox4.Left + 5 && pictureBox3.Left > pictureBox1.Left + 5 && pictureBox3.Left > pictureBox2.Left + 5 && pictureBox3.Left > pictureBox5.Left + 5)
        {
            lblanlik.Text = "3 - Şahbatur 4 - Prenslar Berabere Önde";
        }
        if (pictureBox3.Left == pictureBox5.Left + 5 && pictureBox3.Left > pictureBox1.Left + 5 && pictureBox3.Left > pictureBox2.Left + 5 && pictureBox3.Left > pictureBox4.Left + 5)
        {
            lblanlik.Text = "3 - Şahbatur 5 - Beyaz Saraylar Berabere Önde";
        }

        if (pictureBox4.Left == pictureBox5.Left + 5 && pictureBox4.Left > pictureBox1.Left + 5 && pictureBox4.Left > pictureBox2.Left + 5 && pictureBox4.Left > pictureBox3.Left + 5)
        {
            lblanlik.Text = "4 - Prens ve 5 - Beyaz Saraylar Berabere Önde";
        }

        if (pictureBox1.Left > pictureBox2.Left + 20 && pictureBox1.Left > pictureBox3.Left + 20 && pictureBox1.Left > pictureBox4.Left + 20 && pictureBox1.Left > pictureBox5.Left + 20)
        {
            lblanlik.Text = "1 - Atılgan  Farkı Açtı!!";
        }

        if (pictureBox2.Left > pictureBox1.Left + 20 && pictureBox2.Left > pictureBox3.Left + 20 && pictureBox2.Left > pictureBox4.Left + 20 && pictureBox2.Left > pictureBox5.Left + 20)
        {
            lblanlik.Text = "2 - Gülbatur  Farkı Açtı!!";
        }

        if (pictureBox3.Left > pictureBox1.Left + 20 && pictureBox3.Left > pictureBox2.Left + 20 && pictureBox3.Left > pictureBox4.Left + 20 && pictureBox3.Left > pictureBox5.Left + 20)
        {
            lblanlik.Text = "3 - Şahbatur  Farkı Açtı!!";
        }

        if (pictureBox4.Left > pictureBox1.Left + 20 && pictureBox4.Left > pictureBox2.Left + 20 && pictureBox4.Left > pictureBox3.Left + 20 && pictureBox4.Left > pictureBox5.Left + 20)
        {
            lblanlik.Text = "4 - Prens  Farkı Açtı!!";
        }

        if (pictureBox5.Left > pictureBox1.Left + 20 && pictureBox5.Left > pictureBox2.Left + 20 && pictureBox5.Left > pictureBox3.Left + 20 && pictureBox5.Left > pictureBox4.Left + 20)
        {
            lblanlik.Text = "5 - Beyaz Saray  Farkı Açtı!!";
        }

        if (pictureBox1.Left > pictureBox2.Left + 40 && pictureBox1.Left > pictureBox3.Left + 40 && pictureBox1.Left > pictureBox4.Left + 40 && pictureBox1.Left > pictureBox5.Left + 40)
        {
            lblanlik.Text = "1 - Atılgan  Şampiyonluğa Koşuyor!!";
        }

        if (pictureBox2.Left > pictureBox1.Left + 40 && pictureBox2.Left > pictureBox3.Left + 40 && pictureBox2.Left > pictureBox4.Left + 40 && pictureBox2.Left > pictureBox5.Left + 40)
        {
            lblanlik.Text = "2 - Gülbatur  Şampiyonluğa Koşuyor!!";
        }

        if (pictureBox3.Left > pictureBox1.Left + 40 && pictureBox3.Left > pictureBox2.Left + 40 && pictureBox3.Left > pictureBox4.Left + 40 && pictureBox3.Left > pictureBox5.Left + 40)
        {
            lblanlik.Text = "3 - Şahbatur  Şampiyonluğa Koşuyor!!";
        }

        if (pictureBox4.Left > pictureBox1.Left + 40 && pictureBox4.Left > pictureBox2.Left + 40 && pictureBox4.Left > pictureBox3.Left + 40 && pictureBox4.Left > pictureBox5.Left + 40)
        {
            lblanlik.Text = "4 - Prens  Şampiyonluğa Koşuyor!!";
        }

        if (pictureBox5.Left > pictureBox1.Left + 40 && pictureBox5.Left > pictureBox2.Left + 40 && pictureBox5.Left > pictureBox3.Left + 40 && pictureBox5.Left > pictureBox4.Left + 40)
        {
            lblanlik.Text = "5 - Beyaz Saray  Şampiyonluğa Koşuyor!!";
        }
        #endregion

        #region Kazanan
        // Yarışı kazanan kim ise onu raporladım.
        if (no1atgenislik + pictureBox1.Left >= bitisuzaklik)
        {
            // timer1.Enabled = false;
            timer1.Stop();
            lblanlik.Text = $"1 - Atılgan yarışı kazandı!!";
            kazanan = 1;
        }

        if (no2atgenislik + pictureBox2.Left >= bitisuzaklik)
        {
            // timer1.Enabled = false;
            timer1.Stop();
            lblanlik.Text = "2 - Gülbatur yarışı kazandı!!";
            kazanan = 2;
        }

        if (no3atgenislik + pictureBox3.Left >= bitisuzaklik)
        {
            // timer1.Enabled = false;
            timer1.Stop();
            lblanlik.Text = "3 - Şahbatur yarışı kazandı!!";
            kazanan = 3;
        }

        if (no4atgenislik + pictureBox4.Left >= bitisuzaklik)
        {
            // timer1.Enabled = false;
            timer1.Stop();
            lblanlik.Text = "4 - Prens yarışı kazandı!!";
            kazanan = 4;
        }

        if (no5atgenislik + pictureBox5.Left >= bitisuzaklik)
        {
            // timer1.Enabled = false;
            timer1.Stop();
            lblanlik.Text = "5 - Beyaz Saray yarışı kazandı!!";
            kazanan = 5;
        }
        #endregion

        //Yarış başlayınca başlat butonu pasif hale gelir
        btnStart.Enabled = false;
        //Yarış başlayınca pasıf olan sıfırla butonu aktif olur.
        btnRes.Enabled = true;
        HesaplaKazanc();
        YarışBitti();
    }

    private void btnRes_Click(object sender, EventArgs e)
    {
        pictureBox1.Left = no1atsolauzaklık;
        pictureBox2.Left = no2atsolauzaklık;
        pictureBox3.Left = no3atsolauzaklık;
        pictureBox4.Left = no4atsolauzaklık;
        pictureBox5.Left = no5atsolauzaklık;

        lblanlik.Text = "";

        lbl1.Text = "";
        lbl2.Text = "";
        lbl3.Text = "";
        lbl4.Text = "";
        lbl5.Text = "";

        lblzaman.Text = " 0 ";

        btnStart.Enabled = true;
        Oran_Belirle();
    }

    private void Oran_Belirle()
    {
        Random rOran = new Random();
        double minOran = 1.01;
        double maxOran = 4.00;
        double minFark = 0.35;  // Oranlar arasındaki minimum fark
        int numberOfOdds = 5;
        double[] oranlar = new double[numberOfOdds];
        double totalOran;
        bool validOranlar = false;
        double allowedTotal = 13.00;

        while (!validOranlar)
        {
            double remainingTotal = allowedTotal;

            // Oranların bir tanesini 1.01 ile 2.00 arasında ayarla
            oranlar[0] = Math.Round(1.01 + rOran.NextDouble() * (2.00 - 1.01), 2);
            remainingTotal -= oranlar[0];

            // Diğer oranları oluştur ve en az minFark farkını sağla
            bool sufficientDifference = false;

            while (!sufficientDifference)
            {
                sufficientDifference = true;
                remainingTotal = allowedTotal - oranlar[0];

                // Diğer oranları rastgele oluştur
                for (int i = 1; i < numberOfOdds - 1; i++)
                {
                    double maxAllowable = Math.Min(maxOran, remainingTotal - minOran * (numberOfOdds - i - 1));
                    oranlar[i] = Math.Round(minOran + rOran.NextDouble() * (maxAllowable - minOran), 2);
                    remainingTotal -= oranlar[i];
                }

                // Son oranı kalan toplamdan ayarla
                oranlar[numberOfOdds - 1] = Math.Round(remainingTotal, 2);

                // Oranlar arasındaki farkları kontrol et
                for (int i = 0; i < numberOfOdds; i++)
                {
                    for (int j = i + 1; j < numberOfOdds; j++)
                    {
                        if (Math.Abs(oranlar[i] - oranlar[j]) < minFark)
                        {
                            sufficientDifference = false;
                            break;
                        }
                    }
                    if (!sufficientDifference) break;
                }
            }

            // Oranların toplamını kontrol et
            totalOran = oranlar.Sum();

            // Oranların toplamı allowedTotal'a eşit olmalıdır ve her oran belirtilen aralıkta olmalıdır
            if (totalOran == allowedTotal && oranlar.All(o => o >= minOran && o <= maxOran))
            {
                validOranlar = true;

                // Oranları doğru formatta ayarla
                oran1 = oranlar[0];
                oran2 = oranlar[1];
                oran3 = oranlar[2];
                oran4 = oranlar[3];
                oran5 = oranlar[4];
            }
        }

        lblOran1.Text = oran1.ToString();
        lblOran2.Text = oran2.ToString();
        lblOran3.Text = oran3.ToString();
        lblOran4.Text = oran4.ToString();
        lblOran5.Text = oran5.ToString();

        #region Orana göre hız
        if (oran1 >= oran2 && oran1 >= oran3 && oran1 >= oran4 && oran1 >= oran5)
        {
            horse1Max = 10; horse1Min = 5;
            if (oran2 >= oran3 && oran2 >= oran4 && oran2 >= oran5)
            {
                horse2Max = 11; horse2Min = 6;
                if (oran3 >= oran4 && oran3 >= oran5)
                {
                    horse3Max = 12; horse3Min = 7;
                    horse4Max = 13; horse4Min = 8;
                    horse5Max = 15; horse5Min = 9;
                }
                else if (oran4 >= oran5)
                {
                    horse3Max = 12; horse3Min = 7;
                    horse4Max = 15; horse4Min = 9;
                    horse5Max = 13; horse5Min = 8;
                }
                else
                {
                    horse3Max = 12; horse3Min = 7;
                    horse4Max = 13; horse4Min = 8;
                    horse5Max = 15; horse5Min = 9;
                }
            }
            else if (oran3 >= oran4 && oran3 >= oran5)
            {
                horse2Max = 12; horse2Min = 7;
                horse3Max = 13; horse3Min = 8;
                horse4Max = 15; horse4Min = 9;
                horse5Max = 11; horse5Min = 6;
            }
            else if (oran4 >= oran5)
            {
                horse2Max = 13; horse2Min = 8;
                horse3Max = 15; horse3Min = 9;
                horse4Max = 11; horse4Min = 6;
                horse5Max = 12; horse5Min = 7;
            }
            else
            {
                horse2Max = 15; horse2Min = 9;
                horse3Max = 11; horse3Min = 6;
                horse4Max = 12; horse4Min = 7;
                horse5Max = 13; horse5Min = 8;
            }
        }
        else if (oran2 >= oran1 && oran2 >= oran3 && oran2 >= oran4 && oran2 >= oran5)
        {
            horse2Max = 10; horse2Min = 5;
            if (oran1 >= oran3 && oran1 >= oran4 && oran1 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse3Max = 12; horse3Min = 7;
                horse4Max = 13; horse4Min = 8;
                horse5Max = 15; horse5Min = 9;
            }
            else if (oran3 >= oran4 && oran3 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse3Max = 12; horse3Min = 7;
                horse4Max = 15; horse4Min = 9;
                horse5Max = 13; horse5Min = 8;
            }
            else if (oran4 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse3Max = 15; horse3Min = 9;
                horse4Max = 12; horse4Min = 7;
                horse5Max = 13; horse5Min = 8;
            }
            else
            {
                horse1Max = 11; horse1Min = 6;
                horse3Max = 13; horse3Min = 8;
                horse4Max = 15; horse4Min = 9;
                horse5Max = 12; horse5Min = 7;
            }
        }
        else if (oran3 >= oran1 && oran3 >= oran2 && oran3 >= oran4 && oran3 >= oran5)
        {
            horse3Max = 10; horse3Min = 5;
            if (oran1 >= oran2 && oran1 >= oran4 && oran1 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 12; horse2Min = 7;
                horse4Max = 13; horse4Min = 8;
                horse5Max = 15; horse5Min = 9;
            }
            else if (oran2 >= oran4 && oran2 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse4Max = 15; horse4Min = 9;
                horse5Max = 12; horse5Min = 7;
            }
            else if (oran4 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 15; horse2Min = 9;
                horse4Max = 12; horse4Min = 7;
                horse5Max = 13; horse5Min = 8;
            }
            else
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse4Max = 15; horse4Min = 9;
                horse5Max = 12; horse5Min = 7;
            }
        }
        else if (oran4 >= oran1 && oran4 >= oran2 && oran4 >= oran3 && oran4 >= oran5)
        {
            horse4Max = 10; horse4Min = 5;
            if (oran1 >= oran2 && oran1 >= oran3 && oran1 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 12; horse2Min = 7;
                horse3Max = 13; horse3Min = 8;
                horse5Max = 15; horse5Min = 9;
            }
            else if (oran2 >= oran3 && oran2 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse3Max = 15; horse3Min = 9;
                horse5Max = 12; horse5Min = 7;
            }
            else if (oran3 >= oran5)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 15; horse2Min = 9;
                horse3Max = 12; horse3Min = 7;
                horse5Max = 13; horse5Min = 8;
            }
            else
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse3Max = 15; horse3Min = 9;
                horse5Max = 12; horse5Min = 7;
            }
        }
        else
        {
            horse5Max = 10; horse5Min = 5;
            if (oran1 >= oran2 && oran1 >= oran3 && oran1 >= oran4)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 12; horse2Min = 7;
                horse3Max = 13; horse3Min = 8;
                horse4Max = 15; horse4Min = 9;
            }
            else if (oran2 >= oran3 && oran2 >= oran4)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse3Max = 15; horse3Min = 9;
                horse4Max = 12; horse4Min = 7;
            }
            else if (oran3 >= oran4)
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 15; horse2Min = 9;
                horse3Max = 12; horse3Min = 7;
                horse4Max = 13; horse4Min = 8;
            }
            else
            {
                horse1Max = 11; horse1Min = 6;
                horse2Max = 13; horse2Min = 8;
                horse3Max = 15; horse3Min = 9;
                horse4Max = 12; horse4Min = 7;
            }
        }

        #endregion
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        
        // Oyuncu ismi, bahis miktarı ve seçilen atı al
        string playerName = txtPlayerName.Text;
        decimal betAmount = numBet.Value;
        int selectedHorse = GetSelectedHorse(); // Seçilen atı al
        double odds = (double)GetOddsForHorse(selectedHorse); // Seçilen atın oranını al

        if (string.IsNullOrWhiteSpace(playerName))
        {
            MessageBox.Show("Lütfen geçerli bir oyuncu ismi girin.");
            return;
        }

        if (betAmount <= 0)
        {
            MessageBox.Show("Bahis miktarı 0'dan büyük olmalıdır.");
            return;
        }

        if (selectedHorse == 0)
        {
            MessageBox.Show("Lütfen geçerli bir at seçin.");
            return;
        }

        // DataGridView'e ekle
        dataGridViewBets.Rows.Add(playerName, betAmount, selectedHorse, odds);

        // TextBox ve NumericUpDown'u temizle
        txtPlayerName.Clear();
        numBet.Value = 0;
    }

    public class Player
    {
        public string Name { get; set; }
        public int SelectedHorse { get; set; } // Horse number chosen by the player
        public double BetAmount { get; set; } // Amount bet by the player
        public double Odds { get; set; } // Odds for the horse
    }

    private List<Player> players = new List<Player>();

    private int GetSelectedHorse()
    {
        if (rb1.Checked) return 1;
        if (rb2.Checked) return 2;
        if (rb3.Checked) return 3;
        if (rb4.Checked) return 4;
        if (rb5.Checked) return 5;
        return 0;
    }

    private decimal GetOddsForHorse(int horseNumber)
    {
        switch (horseNumber)
        {
            case 1: return (decimal)oran1;
            case 2: return (decimal)oran2;
            case 3: return (decimal)oran3;
            case 4: return (decimal)oran4;
            case 5: return (decimal)oran5;
            default: return 0;
        }
    }

    private void InitializeDataGridView()
    {
        dataGridViewBets.Columns.Clear();

        dataGridViewBets.Columns.Add("PlayerName", "Oyuncu Adı");
        dataGridViewBets.Columns.Add("BetAmount", "Bahis Miktarı");
        dataGridViewBets.Columns.Add("SelectedHorse", "Seçilen At");
        dataGridViewBets.Columns.Add("Odds", "Oran");
        dataGridViewBets.Columns.Add("Winnings", "Kazanç");
    }

    private void UpdateWinningsInDataGridView()
    {
        foreach (DataGridViewRow row in dataGridViewBets.Rows)
        {
            if (row.IsNewRow) continue; // Yeni satır yer tutucusunu atla

            string playerName = row.Cells["PlayerName"].Value.ToString();
            var player = players.FirstOrDefault(p => p.Name == playerName);

            if (player != null)
            {
                double winnings = 0;
                if (player.SelectedHorse == kazanan)
                {
                    winnings = player.BetAmount * player.Odds;
                    money += winnings;
                }
                else if (kazanan == 4) // Foto-finish
                {
                    winnings = player.BetAmount;
                    money += winnings;
                }
                else
                {
                    winnings = -player.BetAmount;
                    money += winnings;
                }

                row.Cells["Winnings"].Value = winnings;
            }
        }
    }


    private void SetupDataGridView()
    {
        dataGridViewBets.Columns.Add("PlayerName", "Oyuncu Adı");
        dataGridViewBets.Columns.Add("BetAmount", "Bahis Miktarı");
        dataGridViewBets.Columns.Add("SelectedHorse", "Seçilen At");
        dataGridViewBets.Columns.Add("Odds", "Oran");
        dataGridViewBets.Columns.Add("Winnings", "Kazanç"); // Kazanç sütununu ekleyin
    }

    private void button5_Click(object sender, EventArgs e)
    {
        dataGridViewBets.Rows.Clear();
    }

    private void HesaplaKazanc()
    {
        
        switch (kazanan)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
        foreach (DataGridViewRow row in dataGridViewBets.Rows)
        {
            if (row.IsNewRow) break;

            string playerName = row.Cells[0].Value.ToString();
            double betAmount = Convert.ToDouble(row.Cells[1].Value);
            int selectedHorse = Convert.ToInt32(row.Cells[2].Value);
            double odds = Convert.ToDouble(row.Cells[3].Value);

            double winnings = 0;

            if (selectedHorse == kazanan)
            {
                winnings = betAmount * odds;
                money += winnings;
               // MessageBox.Show($"{playerName} Kazandı !!! +{winnings} WON");
            }
            else
            {
                money -= betAmount;
               // MessageBox.Show($"{playerName} Kaybetti !!! -{betAmount} WON");
            }

            row.Cells[4].Value = winnings; // Kazançları DataGridView'e yaz
        }
        break;
        default:
        // Foto-finish veya özel durumlar için işlem yapabilirsiniz.
        break;
        }
        

        
    }

    private void YarışBitti()
    {
        // Her at için mesafeyi hesaplayın
        var atMesafeleri = new Dictionary<PictureBox, double>
    {
        { pictureBox1, AtMesafesiHesapla(pictureBox1) },
        { pictureBox2, AtMesafesiHesapla(pictureBox2) },
        { pictureBox3, AtMesafesiHesapla(pictureBox3) },
        { pictureBox4, AtMesafesiHesapla(pictureBox4) },
        { pictureBox5, AtMesafesiHesapla(pictureBox5) }
    };

        // Mesafelere göre sıralama yapın
        var siraliAtlar = atMesafeleri.OrderBy(at => at.Value).ToList();

        // Sıralamayı lbl1, lbl2, lbl3, lbl4 - Prens ve lbl5 etiketlerine uygulayın
        for (int i = 0; i < siraliAtlar.Count; i++)
        {
            var pictureBox = siraliAtlar[i].Key;
            var sıralama = i + 1; // 1, 2, 3, 4, 5 şeklinde sıralama

            switch (pictureBox)
            {
                case var _ when pictureBox == pictureBox1:
                    lbl1.Text = $"{sıralama}";
                    break;
                case var _ when pictureBox == pictureBox2:
                    lbl2.Text = $"{sıralama}";
                    break;
                case var _ when pictureBox == pictureBox3:
                    lbl3.Text = $"{sıralama}";
                    break;
                case var _ when pictureBox == pictureBox4:
                    lbl4.Text = $"{sıralama}";
                    break;
                case var _ when pictureBox == pictureBox5:
                    lbl5.Text = $"{sıralama}";
                    break;
            }
        }
    }
    private double AtMesafesiHesapla(PictureBox pictureBox)
    {
        // Bitiş çizgisine olan mesafeyi hesaplayın
        int bitisuzaklik = lblBitis.Left; // Bitiş çizgisinin sol kenarının form üzerindeki konumu
        int atKonumu = pictureBox.Left; // Atın sol kenarının form üzerindeki konumu

        // Mesafe, bitiş çizgisine olan uzaklık ve atın mevcut konumu arasındaki farktır
        return bitisuzaklik - atKonumu;
    }
}

