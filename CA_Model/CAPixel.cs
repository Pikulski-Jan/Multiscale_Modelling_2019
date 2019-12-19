using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATest
{
    class CAPixel
    {
        private int x, y;
        private System.Drawing.Color color;
        public System.Drawing.Color color_temp;
        private bool isBorder;
        public bool isSelected;
        private int ID;
        public bool isSet = false;
        public float energy = 2.0f;

        public CAPixel(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.color = System.Drawing.Color.FromArgb(255, 255, 255);
            this.isBorder = false;
            this.ID = 0;
            isSelected = false;

        }
        public CAPixel(int x, int y, System.Drawing.Color color, bool isBorder)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.isBorder = isBorder;
            string buffer = color.R.ToString() + color.G.ToString() + color.B.ToString();
            this.ID = Int32.Parse(buffer);
            isSelected = false;
        }

        public void printID()
        {
            Console.WriteLine(ID);
        }
        
        public void setGrain(System.Drawing.Color c, bool border)
        {
            if(ID == 0)
            {
                string buffer = c.R.ToString() + c.G.ToString() + c.B.ToString();
                this.ID = Int32.Parse(buffer);
            }
            this.color = c;
            this.isBorder = border;
        }

        public void setMCGrain(System.Drawing.Color c, bool border)
        {
            string buffer = c.R.ToString() + c.G.ToString() + c.B.ToString();
            this.ID = Int32.Parse(buffer);
            this.color = c;
            this.isBorder = border;
        }

        public System.Drawing.Color GetColor()
        {
            return this.color;
        }

        public void SetColor(System.Drawing.Color c)
        {
            this.color = c;
        }

        public void ResetPixel()
        {
            this.color = System.Drawing.Color.White;
            this.isSelected = false;
            this.ID = 0;
            this.isBorder = false;
        }

        public void CopyData(int ID, System.Drawing.Color c, bool b)
        {
            this.ID = ID;
            this.color = c;
            this.isBorder = b;
        }

        public int GetID()
        {
            return this.ID;
        }

        public void SetID(int id)
        {
            this.ID = id;
        }

        public bool GetIsBorder()
        {
            return isBorder;
        }

    }
}
