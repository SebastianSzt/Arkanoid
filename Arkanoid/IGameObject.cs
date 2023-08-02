using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Arkanoid
{
    internal interface IGameObject
    {
        void Draw(PaintEventArgs e);
        void Move();
        void ChangeSize(float xRatio, float yRatio);
    }
}