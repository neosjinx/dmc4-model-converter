using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter
{
    public interface IModel
    {
        public void LoadModel(string filepath);
        public void SaveModel(string filepath);
    }
}
