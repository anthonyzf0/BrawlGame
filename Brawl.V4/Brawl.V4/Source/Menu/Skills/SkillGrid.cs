using Brawl.V4.Source.Menu.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills
{
    class SkillGrid
    {
        //Type of skillGrid
        public enum GridType { Projectile };
        private GridType gridType;

        //List of nodes
        private SkillGridCreator gridLoader;
        private List<SkillNode> nodes;

        public SkillGrid(GridType type)
        {
            //Loads the nodes
            gridLoader = new SkillGridCreator();

            gridType = type;
            nodes = gridLoader.getGrid(gridType);

        }

        public void update()
        {
            foreach (SkillNode n in nodes)
                n.update();

        }

        public void draw(Render2D view)
        {

            foreach (SkillNode n in nodes)
                n.draw(view);
            
        }

    }
}
