using JCoin.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Client
{
    public class FolderBlockChain : IBlockChain
    {
        readonly string folder;
        const string headBlockFileName = "head.blk";

        public FolderBlockChain()
            : this( 
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "BlockChain"
                )
            )
        {
        }

        public FolderBlockChain(string folder)
        {
            this.folder = folder;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public void Write(Block b)
        {
            using (var file = new FileStream(Path.Combine(folder, string.Format("{0:x}.blk", b.Id)), FileMode.CreateNew, FileAccess.Write))
            using (var writer = new StreamWriter(file))
            {
                var formatter = new BlockPrettyWriter(writer);
                formatter.Write(b);
            }

            using (var file = new FileStream(Path.Combine(folder, headBlockFileName), FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(file))
            {
                var formatter = new BlockPrettyWriter(writer);
                formatter.Write(b);
            }
        }

        public Block Read(ulong id)
        {
            using (var file = new FileStream(Path.Combine(folder, string.Format("{0:x}.blk", id)), FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file))
            {
                var formatter = new BlockReader(reader);
                return formatter.Read();
            }
        }

        public Block Head()
        {
            using (var file = new FileStream(Path.Combine(folder, headBlockFileName), FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file))
            {
                var formatter = new BlockReader(reader);
                return formatter.Read();
            }
        }
    }
}
