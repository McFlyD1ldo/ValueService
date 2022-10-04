using libValueService;

namespace wfValueService
{
    public partial class Form1 : Form
    {
        private static IValueService _vs;
        public Form1(IValueService valueService)
        {
            InitializeComponent();
            _vs = valueService;
            comboBox1.DataSource = _vs.PostFactors;
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "TextShort";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                textBox2.Text = _vs.GetDecimal(textBox3.Text).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error converting your number.\n" +
                                "Try entering your number with one of the following postfactors:\n" +
                                _vs.displayPostfactors(), "!ERROR!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = _vs.GetDisplayValue(Convert.ToDecimal(textBox1.Text.Replace('.', ',')), Convert.ToInt32(numericUpDown1.Value), comboBox1.SelectedValue.ToString());
            }
            catch (Exception)
            {

                MessageBox.Show("There was an error converting your number.\n" +
                                "Please enter a number without any characters in it\n" +
                                "(e.g. 1000000; 2435; 0,1284424; 0.2456)", "!ERROR!");
            }
            
        }
    }
}