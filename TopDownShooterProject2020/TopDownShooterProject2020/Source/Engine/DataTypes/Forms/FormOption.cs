#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class FormOption
    {
        public string name;
        public object value;

        public FormOption(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

    }
}
