using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace libValueService
{
    public class ValueService : IValueService
    {
        public ValueService()
        {
            //fill the list containing the postfactors and their power
            PostFactors = new List<PostFactor> {
                new PostFactor() { Text="", TextShort="", Potenz=null},
                new PostFactor() { Text = "milli", TextShort = "m", Potenz = -3 },
                new PostFactor() { Text = "mikro", TextShort = "µ", Potenz = -6 },
                new PostFactor() { Text = "nano", TextShort = "n", Potenz = -9 },
                new PostFactor() { Text = "piko", TextShort = "p", Potenz = -12 },
                new PostFactor() { Text = "kilo", TextShort = "k", Potenz = 3 },
                new PostFactor() { Text = "Mega", TextShort = "M", Potenz = 6 },
                new PostFactor() { Text = "Giga", TextShort = "G", Potenz = 9 },
                new PostFactor() { Text = "Tera", TextShort = "T", Potenz = 12 },
                new PostFactor() { Text = "Peta", TextShort = "P", Potenz = 15 },
                new PostFactor() { Text = "Exa", TextShort = "E", Potenz = 18 }
            };
        }

        public List<PostFactor> PostFactors { get; }

        //return the postfactor with its full name and power
        public string displayPostfactors()
        {
            string output = "";
            foreach (var item in PostFactors)
            {
                if(item.Potenz != null) output += item.TextShort + "  " + item.Text + "  Potenz:  " + item.Potenz + "\n";   //return everything but the blank item from the list
            }
            return output;
        }

        //get the decmal value of a string with or without postfactor
        public decimal GetDecimal(string value)
        {
            decimal result = 0;
            value = value.Replace('.', ',');
            string? postFactor = null;
            string[]? numbers = null;
            try
            {

                foreach (var textShort in PostFactors.Where(element => value.Contains(element.TextShort)))  //look for postfactor in the string and split there
                {
                    numbers = value.Split(textShort.TextShort);
                    postFactor = textShort.TextShort;
                }
                if (numbers == null) 
                { 
                    result = decimal.Parse(value);  //if split does not return a value throw an exception/parse a value without postfactor
                }


                if (numbers != null)
                {
                    if (numbers.Count() > 2) throw new FormatException();   //if there were more than one postfactor throw an exception
                    if (numbers[1] != "" && numbers[1] != " ") return Pow10PostFactor(decimal.Parse(String.Join(",", numbers)), postFactor);    //if there is a value after the postfactor replace it with a comma
                    else return Pow10PostFactor(decimal.Parse(String.Join("", numbers)), postFactor); 
                }
                

            }
            catch (FormatException)
            {
                throw new FormatException();               
            }

            
            return Pow10PostFactor(result, postFactor);

        }

        //convert a decimal value to the most fitting postfactor or to the desired postfactor
        public string GetDisplayValue(decimal value, int precision, string desiredpf = "")
        {
            string postfactor =  desiredpf != "" ? desiredpf :  GetPostFactor(value);       //use the desired postfactor if its not empty, else get it yourself
            double.TryParse(Convert.ToString(GetPotenz(postfactor)), out double dblPotenz);     //get the power of the postfactor
            value /= (decimal)Math.Pow(10.00d, dblPotenz);
            value = Math.Round(value, precision);
            return value + postfactor; 
        }

        //get the postfactor fitting best for a given value
        public string GetPostFactor(decimal value)
        {
            var potenz = (int)Math.Floor(Math.Log10((double)value));    //use base 10 logarithm to get the power needed to get to our value
            var postfactor = PostFactors.FirstOrDefault(element => element.Potenz + 1 == potenz  || element.Potenz + 2 == potenz  || element.Potenz == potenz); //if power is one or two greater than in our list or is the same then use that postfactor
            return postfactor != null ? postfactor.TextShort : "";
        }

        public int? GetPotenz(string value)
        {
            if (value == "") return 0;
            return PostFactors.FirstOrDefault(element => element.TextShort == value)?.Potenz;
        }

        public decimal Pow10PostFactor(decimal number, string PostFactor)
        {
            double.TryParse(Convert.ToString(GetPotenz(PostFactor)), out double dblPotenz);
            return number * (decimal)Math.Pow(10.00d, dblPotenz);
        }
    }

    public class PostFactor
    {

        public string Text { get; set; } = "";
        public string TextShort { get; set; } = "";
        public int? Potenz { get; set; }
    }
}
