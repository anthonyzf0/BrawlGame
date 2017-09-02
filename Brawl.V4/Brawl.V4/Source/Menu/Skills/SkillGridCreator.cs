using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Brawl.V4.Source.Menu.Skills.SkillGrid;

namespace Brawl.V4.Source.Menu.Skills
{
    class SkillGridCreator
    {
        private Dictionary<GridType, List<SkillNode>> grids = new Dictionary<GridType, List<SkillNode>>();

        public SkillGridCreator()
        {
            //Test
            addNode(GridType.Projectile,3,3,new string[]{ "aaa", "bbb", "ccc" });

        }

        private void addNode(GridType grid, int x, int y, String[] values)
        {
            //Create new spot in list if it doesnt exits
            if (!grids.ContainsKey(grid))
                grids.Add(grid, new List<SkillNode>());

            //Adds it in
            grids[grid].Add(new SkillNode(x, y, values));
        }
        
        //Gets the lists for use
        public List<SkillNode> getGrid(GridType grid)
        {
            return grids[grid];
        }

    }
}
