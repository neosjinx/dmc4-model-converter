using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter.MTFramework;

namespace DMC4ModelConverter.Wavefront
{
    public class WavefrontModel : IModel
    {
        
        public WavefrontModel()
        {

        }

        public static WavefrontModel FromDMC4Model(DMC4Model model)
        {

            return null;
        }


        public void LoadModel(string filepath)
        {
            throw new NotImplementedException();
        }

        public void SaveModel(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
