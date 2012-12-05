using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace ViciousDemo
{
    class Program
    {
        private class Entity
        {
            public int tile;
            public int x, y;
        }
        static Bitmap tileset;
        static int[,] map;
        static List<Entity> entities, fixtures;

        static int tileWidth = 48;
        static int tileHeight = 64;
        static int tileHIncrease = 16;
        static int tileVIncrease = 32;
        static int mapWidth;
        static int mapHeight;
        static ImageAttributes alphaAttributes;
        static Bitmap canvas;
        static Graphics gfx;
        static int cursorX = 3;
        static int cursorY = 3;

        static void Init()
        {
            tileset = new Bitmap(@"slashem-revised-transparent.png");
            map = new[,]
			{
{ 1178, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1179},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1186, 1177, 1194, 1177, 1177, 1177, 1179, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1176, 1194, 1194, 1194, 1194, 1194, 1176, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1194, 1176},
{ 1180, 1177, 1177, 1177, 1177, 1177, 1183, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1177, 1181}
/*
				{ 1178, 1177,1177,1177, 1177,1177, 1179, 1175,1175 },
				{ 1176, 1194,1194,1194, 1194,1194, 1176, 1175,1175 },
				{ 1186, 1177,1177,1194, 1194,1194, 1194, 1194,1194 },
 				{ 1176, 1194,1194,1194, 1194,1194, 1176, 1175,1175 },
				{ 1176, 1194,1194,1194, 1194,1194, 1176, 1175,1175 },
				{ 1180, 1177,1177,1194,1177,1177,1181, 1175,1175 },
				{ 1175,1175,1175,1194,1175,1175,1175,  1175,1175 },*/
			};
            fixtures = new List<Entity>()
			{
				new Entity() { tile = 1203, x = 1, y = 1 },
				new Entity() { tile = 1206, x = 2, y = 1 },
				new Entity() { tile = 1197, x = 5, y = 1 },
                new Entity() { tile = 1189, x = 2, y = 11},
                new Entity() { tile = 1188, x = 6, y = 15},
			};
            entities = new List<Entity>()
            {
				new Entity() { tile = 541 , x = 6, y = 2 },
				new Entity() { tile = 1409, x = 4, y = 3 },
				new Entity() { tile = 1406, x = 3, y = 4 },
				new Entity() { tile = 414 , x = 11, y = 5 },
				new Entity() { tile = 469 , x = 2, y = 10 },
				new Entity() { tile = 17, x = 8, y = 8 },
				new Entity() { tile = 14, x = 13, y = 6 },
				new Entity() { tile = 14, x = 15, y = 6 },
				new Entity() { tile = 14, x = 14, y = 6 },
				new Entity() { tile = 14, x = 12, y = 6 }
            };
            tileWidth = 48;
            tileHeight = 64;
            tileHIncrease = 16;
            tileVIncrease = 32;
            mapWidth = map.GetUpperBound(1);
            mapHeight = map.GetUpperBound(0);
            var alphaMatrix = new ColorMatrix();
            alphaMatrix.Matrix33 = 0.5f;
            alphaAttributes = new ImageAttributes();
            alphaAttributes.SetColorMatrix(alphaMatrix);
            canvas = new Bitmap((mapWidth * tileWidth) + tileHIncrease, (mapHeight * tileVIncrease) + tileHeight);
            gfx = Graphics.FromImage(canvas);
            cursorX = 3;
            cursorY = 3;
        }
        public static void Update()
        {
            Random rnd = new Random();
            foreach (Entity ent in entities)
            {
                var randVal = rnd.Next(4);
                switch (randVal)
                {
                    case 0: if (ent.x > 0 && (map[ent.y, ent.x - 1] == 1194) && entities.FirstOrDefault(e => e.x == ent.x - 1 && e.y == ent.y) == null) ent.x--;
                        break;
                    case 1: if (ent.y > 0 && (map[ent.y - 1, ent.x] == 1194) && entities.FirstOrDefault(e => e.x == ent.x && e.y == ent.y - 1) == null) ent.y--;
                        break;
                    case 2: if (ent.x + 1 < mapWidth && (map[ent.y, ent.x + 1] == 1194) && entities.FirstOrDefault(e => e.x == ent.x + 1 && e.y == ent.y) == null) ent.x++;
                        break;
                    case 3: if (ent.y + 1 < mapHeight && (map[ent.y + 1, ent.x] == 1194) && entities.FirstOrDefault(e => e.x == ent.x && e.y == ent.y + 1) == null) ent.y++;
                        break;
                }
            }
        }
        public static Bitmap Draw()
        {
            gfx.Clear(Color.FromArgb(32, 32, 32));
            for (var row = 0; row <= mapHeight; row++)
            {
                var pY = tileVIncrease * row;
                var pX = tileHIncrease * (mapHeight - 1 - row) + tileHIncrease;
                for (var col = 0; col <= mapWidth; col++)
                {
                    var dest = new Rectangle(pX, pY, tileWidth, tileHeight);
                    var tile = map[row, col];
                    var src = new Rectangle((tile % 38) * tileWidth, (tile / 38) * tileHeight, tileWidth, tileHeight);
                    gfx.DrawImage(tileset, dest, src, GraphicsUnit.Pixel);

#if !CURSORONFLOOR
                    if (cursorX == col && cursorY == row)
                        gfx.DrawImage(tileset, dest, (1442 % 38) * tileWidth, (1442 / 38) * tileHeight, tileWidth, tileHeight, GraphicsUnit.Pixel, alphaAttributes);
#endif

                    var entity = entities.FirstOrDefault(e => e.x == col && e.y == row);
                    var fixture = fixtures.FirstOrDefault(e => e.x == col && e.y == row);
                    if (entity != null)
                    {
                        tile = entity.tile;
                        src = new Rectangle((tile % 38) * tileWidth, (tile / 38) * tileHeight, tileWidth, tileHeight);
                        gfx.DrawImage(tileset, dest, src, GraphicsUnit.Pixel);
                    } else if (fixture != null)
                    {
                        tile = fixture.tile;
                        src = new Rectangle((tile % 38) * tileWidth, (tile / 38) * tileHeight, tileWidth, tileHeight);
                        gfx.DrawImage(tileset, dest, src, GraphicsUnit.Pixel);
                    }
                    pX += tileVIncrease;
                }
            }
#if CURSORONFLOOR
			gfx.DrawImage(tileset, new Rectangle((tileHIncrease * (mapHeight - 1 - cursorY) + tileHIncrease + (cursorX * tileVIncrease)), tileVIncrease * cursorY, tileWidth, tileHeight), tileWidth, 0, tileWidth, tileHeight, GraphicsUnit.Pixel, alphaAttributes);
#endif
            //			var silk = new Font("Silkscreen", 7, FontStyle.Regular);
            //			gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            //			gfx.DrawString("From actual code", silk, Brushes.White, 4, 4);
            return canvas;
            //canvas.Save(@"attempt.png", ImageFormat.Png);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Init();
            Application.EnableVisualStyles();
            try
            {
                Application.Run(new Vicious());
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}