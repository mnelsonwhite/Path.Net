using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Path.Net
{
    public struct Path
        : IEquatable<Path>
        , IComparable<Path>
        , IFormattable
        , IEnumerable<string>
    {
        private static readonly string _defaultDelimiter = System.IO.Path.DirectorySeparatorChar.ToString();
        private readonly string[] _path;

        public string this[int key]
        {
            get => _path[key];
            set => _path[key] = value;
        }

        public int Length => _path.Length;

        public static Path Parse(string path, string? delmiter = null)
        {
            return new Path(Split(path, delmiter));
        }

        public Path(IEnumerable<string> path)
        {
            _path = path.ToArray();
        }

        public Path(Path path) : this(path._path) { }

        public Path(params string[] path) : this((IEnumerable<string>)path) { }

        public Path Concat(params string[] segments)
        {
            return new Path(_path.Concat(segments));
        }

        public Path Concat(Path path)
        {
            return new Path(_path.Concat(path._path));
        }

        public bool IsChildOf(Path path)
        {
            if (path._path.Length >= _path.Length)
            {
                return false;
            }

            var self = this;
            return path._path
                .Select((segment, index) => new { segment, index })
                .All(item => self._path[item.index] == item.segment);
        }

        public Path ToRelative(Path path)
        {
            var self = this;
            var length = path._path
                .Select((segment, index) => new { segment, index })
                .TakeWhile(item => self._path[item.index] == item.segment)
                .Count();

            return new Path(_path.Skip(length));
        }

        public Path CommonParent(Path path)
        {
            var self = this;
            var segments = path._path
                .Select((segment, index) => new { segment, index })
                .Where(item => self._path[item.index] == item.segment)
                .Select(item => item.segment);

            return new Path(segments);
        }

        public int CompareTo(Path other)
        {
            if (IsChildOf(other)) return -1;
            if (Equals(other)) return 0;
            return 1;
        }

        public override string ToString()
        {
            return ToString(_defaultDelimiter);
        }

        public string ToString(string delimiter)
        {
            return string.Join(delimiter, _path);
        }

        public static Path operator +(Path lhs, string rhs)
        {
            return lhs.Concat(rhs);
        }

        public static Path operator +(Path lhs, string[] rhs)
        {
            return lhs.Concat(rhs);
        }

        public static Path operator +(Path lhs, Path rhs)
        {
            return lhs.Concat(rhs);
        }

        public static bool operator ==(Path lhs, Path rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Path lhs, Path rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is Path path)
            {
                return Equals(path);
            }
            return false;
        }

        public bool Equals(Path other)
        {
            return _path.SequenceEqual(other._path);
        }

        public override int GetHashCode()
        {
            return _path?.GetHashCode() ?? 0;
        }

        private static string[] Split(string target, string? delimiter)
        {
            return target.Split(
                new[] { delimiter ?? _defaultDelimiter },
                StringSplitOptions.None);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return (IEnumerator<string>) _path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _path.GetEnumerator();
        }
    }
}
