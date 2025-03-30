namespace WebApplication1.Services
{
    public class GradeCalculator
    {
        public static (string LetterGrade, decimal GPA) CalculateLetterGrade(int percentage)
        {
            return percentage switch
            {
                >= 97 => ("A+", 4.0m),
                >= 93 => ("A", 3.9m),
                >= 90 => ("A-", 3.7m),
                >= 87 => ("B+", 3.3m),
                >= 83 => ("B", 3.0m),
                >= 80 => ("B-", 2.7m),
                >= 77 => ("C+", 2.3m),
                >= 73 => ("C", 2.0m),
                >= 70 => ("C-", 1.7m),
                >= 67 => ("D+", 1.3m),
                >= 63 => ("D", 1.0m),
                >= 60 => ("D-", 0.7m),
                _ => ("F", 0.0m)
            };
        }
    }
}
