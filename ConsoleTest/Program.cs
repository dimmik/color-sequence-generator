using ColorSequenceGenerator;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IColorSequenceGenerator csg = ColorSequenceGenerator.ColorSequenceGenerator.Instance;
            string fn = @"/tmp/color-sequence.html";
            Console.WriteLine($"Write to {Path.GetFullPath(fn)}");
            using var fs = File.OpenWrite(fn);
            using var sw = new StreamWriter(fs);
            sw.WriteLine("<html><body>");
            var colors = csg.ColorSequence();
            foreach (var color in colors.Take(200))
            {
                sw.WriteLine($"<div style=\"background-color: {color}\">{color}</div>");
            }
            var colorSeq = csg.ColorSequence();
            var persons = new[] { "John Doe", "Peter Smith", "Jack Daniels" };
            var personsAndColors = persons.Zip(csg.ColorSequence());
            Console.WriteLine($"{string.Join(",", personsAndColors.Select(pc => $"({pc.First}, {pc.Second})"))}");

            sw.WriteLine("</body></html>");

            RGB r = "rgb(23, 54, 134)";
            string s = r;

        }
    }
}