using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public List<string> Kod=new List<string>() ;
        public List<string> Def;
        public string[] perm;
        public Encoding Enc = Encoding.GetEncoding(1251);
        public Dictionary<string, string> Dict = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            Def = new List<string>(){ "а", "б", "в", "г", "д", "е","ё", "ж", "з", "и", "й", "к", "л", "м",
                                      "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ",
                                      "ы", "ь", "э", "ю", "я" };
            foreach (var v in Def)
            {
                textBox1.Text += v + " ";
            }
     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label10.Visible = false;
            label11.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label6.Visible = false;
            textBox3.Visible = false;
            textBox5.Visible = false;
            label8.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            checkBox1.Visible = false;
            timer1.Enabled = true;

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void исходныйАлфавитToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void Spy()
        {
            Dict.Clear();
            
            richTextBox2.Clear();

            var textlc = richTextBox1.Text;

            var j = 0;
            foreach (var v in Def)
            {
                if (!checkBox1.Checked)
                    Dict.Add(v, Kod[j]);
                else
                {
                    Dict.Add(Kod[j], v);
                }
                j++;
            }

            var regex = new Regex("\\W");
            var i = 0;
            while (textlc.Length != i)
            {
                var k = 0;
                foreach (var dicElem in Dict)
                {
                    
                    var str = textlc.Substring(i, Dict.First().Key.Length);
                    if (regex.IsMatch(str))
                    {
                        i++;
                        break;
                    }
                    if (String.Compare(dicElem.Key.Substring(0, Dict.First().Key.Length), str, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        textlc = textlc.Remove(i, Dict.First().Key.Length).Insert(i, dicElem.Value);
                        i += dicElem.Value.Length;
                        break;
                    }
                    k++;
                    if (k == Dict.Count) i++;

                }
            }
            richTextBox2.Text = textlc;
        }

        public void Xor()
        {
            if (richTextBox1.Text.Where(l => l == '0' || l == '1').Count() != richTextBox1.Text.Length
                || textBox3.Text.Where(l => l == '0' || l == '1').Count() != textBox3.Text.Length)
            {
                MessageBox.Show("Для ввода разрешены только 0 и 1");
                return;
            }
            richTextBox2.Clear();
            for (var i = 0; i < richTextBox1.Text.Length; i++)
            {
                richTextBox2.Text += (richTextBox1.Text[i] ^ textBox3.Text[i % textBox3.Text.Length]).ToString();
            }
        }

        public void Cesar()
        {
            Dict.Clear();
            textBox2.Clear();
            Kod.Clear();
            if (textBox4.Text.Length == 0) return;
            if (textBox4.Text == "-") return;

            try
            {
                var sh = int.Parse(textBox4.Text);

                foreach (var d in Def)
                {
                    Kod.Add(d);
                }

                if (sh > 0)
                {
                    for (var j = 0; j < Def.Count; j++)
                    {
                        if (j + sh < Def.Count) Kod[j] = Def[j + sh];
                        else Kod[j] = Def[(j + sh) % Def.Count];
                    }
                }
                else
                {
                    for (var j = 0; j < Def.Count; j++)
                    {

                        if (Math.Abs(sh) > Def.Count) sh = sh % Def.Count;
                        if (j + sh < 0) Kod[j] = Def[(j + sh) + Def.Count];
                        else
                            Kod[j] = Def[j + sh];
                    }
                }
                foreach (var v4 in Kod)
                {
                    textBox2.Text += v4+" ";
                }
                var j2 = 0;
                foreach (var v in Def)
                {
                    if (!checkBox1.Checked)
                        Dict.Add(v, Kod[j2]);
                    else
                    {
                        Dict.Add(Kod[j2], v);
                    }
                    j2++;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка");
                return;
            }
        }

        public void CesarText()
        {
            richTextBox2.Clear();
          var text = richTextBox1.Text;
            foreach (var v in text)
            {
                if (char.IsLetter(v))
                {
                    foreach (var b in Dict)
                    {
                        if (b.Key.ToUpper() == v.ToString().ToUpper()) richTextBox2.Text += b.Value;
                    }
                }
                else richTextBox2.Text += v.ToString();
            }
        }

        public void Permutation()
        {
            if (int.Parse(textBox5.Text.Where(l => Char.IsDigit(l)).Max().ToString()) > textBox5.Text.Where(l => Char.IsDigit(l)).Count())
            {
                MessageBox.Show("Выход за границу блока!!!");
                return;
            }
            if (perm.Where(l => int.Parse(l)<0).Count()>0)
            {
                MessageBox.Show("Для ввода разрешены только положительные числа!!!");
                return;
            }
            richTextBox2.Text = "";
            try
            {
                var text = richTextBox1.Text;
                var s = new List<char>();
                if (text.Length % perm.Length != 0)
                {
                    while (text.Length % perm.Length != 0)
                    {
                        text += "+";
                    }
                }

                var i = 0;
                while (i != text.Length)
                {
                    foreach (var p in perm)
                    {
                        s.Add(text[int.Parse(p) - 1 + i]);
                    }
                    i += perm.Length;
                }
                foreach (var v in s)
                {
                    richTextBox2.Text += v;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!!!");
                return;
            }
        }

        public void PermutationAlter()
        {
            richTextBox2.Text = "";
            var text = richTextBox1.Text;
            if (text.Length % perm.Length != 0)
            {
                while (text.Length % perm.Length != 0)
                {
                    text += "#";
                }
            }
            var res = new char[text.Length];
            var i = 0;
            while (i != text.Length)
            {
                var j = 0;
                foreach (var p in perm)
                {

                    res[int.Parse(p) - 1 + i] = text[j + i];
                    j++;
                }
                i += perm.Length;
            }
            foreach (var v in res)
            {
                richTextBox2.Text += v;
            }
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void кодированныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        public void Generate()
        {
            textBox2.Text = "";
            Kod.Clear();
            var random = new Random();
            foreach (var s in Def)
            {
                Kod.Add(s);
            }
            for (var j = Def.Count - 1; j > 1; j--)
            {
                var y = random.Next(j + 1);
                var temp = Kod[y];
                Kod[y] = Kod[j];
                Kod[j] = temp;
            }
            foreach (var v in Kod)
            {
                textBox2.Text += v + " ";
            }
        
    }
        private void сгенерироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox2.Text = "";
            Kod.Clear();
            var random = new Random();
            foreach (var s in Def)
            {
                Kod.Add(s);
            }
            for (var j = Def.Count - 1; j > 1; j--)
            {
                var y = random.Next(j + 1);
                var temp = Kod[y];
                Kod[y] = Kod[j];
                Kod[j] = temp;
            }
            foreach (var v in Kod)
            {
                textBox2.Text += v+" ";
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox2.Text = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "openFileDialog1") return;
            var fileName = openFileDialog1.FileName;
            var f = System.IO.File.ReadAllText(fileName, Enc);
            var tmp = f.Split(',');
            Kod.Clear();
            foreach (var s in tmp)
            {
                Kod.Add(s);
            }
            foreach (var v in Kod)
            {
                textBox2.Text += v+" ";
            }
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Def = new List<string>() { "а", "б", "в","г", "д", "е","ё", "ж", "з", "и", "й", "к", "л", "м",
                "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            textBox1.Text = "";
            foreach (var v in Def)
            {
                textBox1.Text += v+" ";
            }
            Generate();
        }

        private void загрузитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "openFileDialog1") return;
            var fileName = openFileDialog1.FileName;
            var f = System.IO.File.ReadAllText(fileName, Enc);
            Def.Clear();
            Def = f.Split(',').ToList();
            foreach (var v in Def)
            {
                textBox1.Text += v+" ";
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Def.Clear();
            for (int i = 97; i < 122; i++)
            {
                Def.Add(Convert.ToString((char)i));
            }
            foreach (var v in Def)
            {
                textBox1.Text += v+" ";
            }
            Generate();
        }

        private void загрузитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "openFileDialog1") return;
            var fileName = openFileDialog1.FileName;
           var text = System.IO.File.ReadAllText(fileName, Enc);
            richTextBox1.Text = text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!xored.Checked && !pere.Checked)
            {
                var regex = new Regex("\\w");
                var s1 = textBox1.Text.Split(new Char[] { ',', '.', '/', '|', '*', '/', ' ', '-' });
                var s2 = textBox2.Text.Split(new Char[] { ',', '.', '/', '|', '*', '/', ' ', '-' });
                Def.Clear();
                Kod.Clear();
                foreach (var v in s1)
                {
                    if (regex.IsMatch(v)) Def.Add(v);
                }
                foreach (var v in s2)
                {
                    if (regex.IsMatch(v)) Kod.Add(v);
                }
                if (Def.Count != Kod.Count) { MessageBox.Show("Разная мощность алфавитов!!!"); return; }
            }
            if (Podstanovka.Checked) Spy();
            if(ces.Checked) { Cesar(); CesarText(); }
            if(xored.Checked) Xor();
            if(pere.Checked)
            {
                if (radioButton1.Checked)
                    Permutation();
                if (radioButton2.Checked)
                    PermutationAlter();
            }



            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == "Подстановка" && item.Checked) Spy();
                if (item.Text == "Цезарь" && item.Checked) { Cesar(); CesarText(); }
                if (item.Text == "XOR" && item.Checked) Xor();
                if (item.Text == "Перестановка" && item.Checked) {
                    if (radioButton1.Checked)
                        Permutation();
                    if (radioButton2.Checked)
                        PermutationAlter();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var beginText = "Введите сюда текст который нужно зашифровать...";
                if (Podstanovka.Checked || ces.Checked)
                {
                    if (Kod.Count != 0 && richTextBox1.TextLength != 0 && richTextBox1.Text != beginText && textBox4.TextLength!=0)
                    {
                        checkBox1.Enabled = true;
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                        checkBox1.Enabled = false;
                    }
                }
                if (xored.Checked)
                {
                    if (textBox3.TextLength != 0 && richTextBox1.TextLength != 0 && richTextBox1.Text != beginText) button1.Enabled = true;
                    else button1.Enabled = false;
                }
                if (pere.Checked)
                {
                    if (textBox5.TextLength != 0 && richTextBox1.TextLength != 0 && richTextBox1.Text != beginText) button1.Enabled = true;
                    else button1.Enabled = false;
                }
                if (checkBox1.Visible)
                {
                    if (checkBox1.Checked)
                    {
                        label2.Text = "Зашифрованный текст";
                        label3.Text = "Расшифрованный текст";
                        button1.Text = "Расшифровать";
                    }
                    else
                    {
                        label2.Text = "Исходный текст";
                        label3.Text = "Зашифрованный текст";
                        button1.Text = "Зашифровать";
                    }
                }
                else
                {
                        label2.Text = "Исходный текст";
                        label3.Text = "Зашифрованный текст";
                        button1.Text = "Зашифровать";
                }
        }
        private void InitializeTimer()
        {
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           

                var txt = textBox2.Text;
                textBox2.Text = textBox1.Text;
                textBox1.Text = txt;

        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) return;
            var number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8 && number != Convert.ToChar(Keys.Enter) && number != Convert.ToChar("-"))
            {
                MessageBox.Show("Для ввода разрешены только числа!");
                e.Handled = true;
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Cesar(); 
            Cesar();
            
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left) return;
                perm = textBox5.Text.Split(new Char[] { ',', '.', '/', '|', '*', '/' });
                
                if (textBox5.Text.Split(new Char[] { ',', '.', '/', '|', '*', '/' }).Distinct().Count() < perm.Length)
                {
                    throw new Exception();
                }
            }catch(Exception)
            {
                MessageBox.Show("Ошибка! Одинаковые цифры.");
            }
        }

        private void подстановкаToolStripMenuItem_Click(object sender, EventArgs e)
        {//podstanovka
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == "Подстановка") item.Checked = true; else item.Checked = false;
            }
            
            Generate();
            label9.Text = "Подстановка";
            checkBox1.Visible = true;

            label4.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;

            label6.Visible = false;
            textBox3.Visible = false;
            label7.Visible = false;
            textBox4.Visible = false;
            label8.Visible = false;
            textBox5.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void цезарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cesar

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == "Цезарь") item.Checked = true; else item.Checked = false;
            }

            checkBox1.Visible = true;
            label9.Text = "Цезарь";
            label4.Visible = true;
            label5.Visible = true;
            label7.Visible = true;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox2.Clear();
            textBox4.Visible = true;

            label8.Visible = false;
            label6.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //xor

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == "XOR") item.Checked = true; else item.Checked = false;
            }



            checkBox1.Visible = false;
            label9.Text = "XOR";
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox5.Visible = false;

            label6.Visible = true;
            textBox3.Visible = true;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void перестановкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //perestanovka

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == "Перестановка") item.Checked = true; else item.Checked = false;
            }


            checkBox1.Visible = false;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            label9.Text = "Перестановка";
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
            label6.Visible = false;
            textBox3.Visible = false;


            label8.Visible = true;
            textBox5.Visible = true;

            radioButton1.Checked = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void подстановкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (Podstanovka.Checked)
            {
                label11.Visible = false;
                label10.Visible = false;
                label1.Visible = false;
                richTextBox2.Clear();
                Generate();
                label9.Text = "Подстановка";
                checkBox1.Visible = true;

                label4.Visible = true;
                label5.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;

                label6.Visible = false;
                textBox3.Visible = false;
                label7.Visible = false;
                textBox4.Visible = false;
                label8.Visible = false;
                textBox5.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (ces.Checked)
            {
                label11.Visible = false;
                label10.Visible = true;
                label1.Visible = false;
                richTextBox2.Clear();
                checkBox1.Visible = true;
                label9.Text = "Цезарь";
                label4.Visible = true;
                label5.Visible = true;
                label7.Visible = true;

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Clear();
                textBox4.Visible = true;

                label8.Visible = false;
                label6.Visible = false;
                textBox5.Visible = false;
                textBox3.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (xored.Checked)
            {
                label11.Visible = true;
                label10.Visible = false;
                label1.Visible = false;
                richTextBox2.Clear();
                checkBox1.Visible = false;
                label9.Text = "XOR";
                label4.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                textBox5.Visible = false;

                label6.Visible = true;
                textBox3.Visible = true;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (pere.Checked)
            {
                label11.Visible = true;
                label10.Visible = false;
                label1.Visible = false;
                richTextBox2.Clear();
                checkBox1.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                label9.Text = "Перестановка";
                label4.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                textBox3.Visible = false;


                label8.Visible = true;
                textBox5.Visible = true;

                radioButton1.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (richTextBox1.Text == "Введите сюда текст который нужно зашифровать...") richTextBox1.Clear();
        }

        private void richTextBox1_MouseLeave(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength == 0) richTextBox1.Text = "Введите сюда текст который нужно зашифровать...";
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (richTextBox1.Text == "Введите сюда текст который нужно зашифровать...") richTextBox1.Clear();
        }
    }
}
