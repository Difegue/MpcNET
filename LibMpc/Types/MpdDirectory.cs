using System.Collections.Generic;
using System.Linq;

namespace LibMpc.Types
{
    public class MpdDirectory
    {
        private readonly IList<MpdFile> _files = new List<MpdFile>();

        public MpdDirectory(string path)
        {
            path.CheckNotNull();

            Path = path;

            var name = path.Split('/').Last();
            Name = string.IsNullOrEmpty(name) ? "root" : name;
        }

        public string Path { get; }
        public string Name { get; }
        public IEnumerable<MpdFile> Files => _files;

        internal void AddFile(string file)
        {
            _files.Add(new MpdFile(file));
        }
    }
}