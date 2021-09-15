using System.Linq;

namespace School.UI
{
    public static class Validation
    {
        public static bool FirstOrLastName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (name.Any(char.IsDigit))
                return false;

            return
                name.Length is >= 2 and <= 20;
        }

        public static bool ClassName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return
                name.Length is >= 1 and <= 3;
        }

        public static bool SubjectName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (name.Any(char.IsDigit))
                return false;

            return
                name.Length is >= 4 and <= 30;
        }
    }
}