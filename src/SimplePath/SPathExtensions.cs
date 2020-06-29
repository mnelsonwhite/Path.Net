using System.Linq;

namespace SimplePath
{
    public static class SPathExtensions
    {
        public static SPath? Parent(this SPath path)
        {
            if (path.Length < 2) return null;
            return new SPath(
                path.Take(path.Length - 1),
                path.DefaultDelimiter
            );
        }
    }
}
