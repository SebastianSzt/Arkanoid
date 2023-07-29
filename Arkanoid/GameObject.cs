using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Arkanoid
{
    public class GameObject : IGameObject
    {
        protected int posX;
        protected int posY;
        protected int width;
        protected int height;
        protected Color color;

        public GameObject(int posX, int posY, int width, int height, Color color)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public virtual void Draw(PaintEventArgs e) { }
    }
}