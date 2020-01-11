using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paw.Web.Areas.Blocks.Controllers
{
    public class Block
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = String.Empty;

        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;

        public static List<Block> GetList()
        {
            return BlockList;
        }

        public static void Add(Block block)
        {
            BlockList.Add(block);
        }

        public static bool Delete(Guid id)
        {
            Block block = Block.BlockList.Find(x => x.Id == id);
            if (block == null)
            {
                return false;
            }

            Block.BlockList.Remove(block);
            return true;
        }

        public static List<Block> BlockList
        {
            get { return _BlockList; }
            set { _BlockList = value; }
        }
        private static List<Block> _BlockList = new List<Block>();

    }
}