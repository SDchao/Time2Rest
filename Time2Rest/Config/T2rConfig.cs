using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time2Rest.Config
{
    class T2rConfig
    {
        // Config
        // Function Config
        public int alertInterval = 1;
        public int minimumRestTime = 3;
        public int alertAgainInterval = 3;

        // UI Config
        public double maxOpacity = 0.8;
        private Color _backColor = Color.Black;
        private Color _foreColor = Color.White;

        public string backColorString
        {
            set { _backColor = ColorTranslator.FromHtml(value); }
            get { return ColorTranslator.ToHtml(_backColor); }
        }

        public string foreColorString
        {
            set { _foreColor = ColorTranslator.FromHtml(value); }
            get { return ColorTranslator.ToHtml(_foreColor); }
        }

        public String backGroundImgPath = "";

        public Color GetBackColor()
        {
            return _backColor;
        }

        public Color GetForeColor()
        {
            return _foreColor;
        }
    }
}
