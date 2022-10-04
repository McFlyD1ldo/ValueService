namespace libValueService
{
        
    public interface IValueService {

        List<PostFactor> PostFactors { get; }

        /// <summary>
        /// converts the given number as string with/without postfactor to a decimal 
        /// </summary>
        /// <param name="value">string of the number to convert. Can contain - , . postfactor inside or at the end of the string</param>
        /// <returns>decimal representation of the string number without power to and postfactor</returns>
        decimal GetDecimal(string value);
        
        /// <summary>
        /// Converts the decimal input value to string including postfactor and a specific number of postcomma digits
        /// </summary>
        /// <param name="value">decimal value to be converted</param>
        /// <param name="precision">number of postcomma digits</param>
        /// <returns>string representation to use at UIs</returns>
        string GetDisplayValue(decimal value, int precision, string desiredpf);

        /// <summary>
        /// calculate a decimal number out of a number and its postfactor (e.g. 100k = 1000000)
        /// </summary>
        /// <param name="number">decimal number to multiply the postfactor to</param>
        /// <param name="PostFactor">string postfactor to calculate the number</param>
        /// <returns>decimal representation of number and postfactor</returns>
        decimal Pow10PostFactor(decimal number, string PostFactor);


        //decimal Pow10(decimal value, int potenz);
        
        /// <summary>
        /// determines the power (10^x) from the list of postfactors
        /// </summary>
        /// <param name="value">postfactor to be searched</param>
        /// <returns>power x (10^x) as signed integer. null if no postfactor is found</returns>
        int? GetPotenz(string value);
        
        /// <summary>
        /// determines the postfactor from a given value. The optimized factor is found, when the value is > 0 and < 1000
        /// </summary>
        /// <param name="value">decimal value without postfactor</param>
        /// <returns>optimal postfactor for the value (1 character)</returns>
        string GetPostFactor(decimal value);

        /// <summary>
        /// Displays the postfactors with their full name and power
        /// </summary>
        /// <returns></returns>
        public string displayPostfactors();
    }
}