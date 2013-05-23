using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Interfaces
{
    public interface IModel
    {
        void LoadModel(string filepath);
        void SaveModel(string filepath);
    }
}
