namespace Assets.Scripts
{

    public static class RedDotUtility
    {
        public static RedDotRoot CreateRoot(RedDotData d)
        {
            RedDotRoot root = new RedDotRoot(d);
            root.OnCreate();
            //RedDotManager.AddNodeToMap(root);
            RedDotManager.AddToRootMap(root);
            return root;
        }

        public static RedDotNode CreateNode(RedDotData d, RedDotNode father, RedDotRoot root = null)
        {
            RedDotNode node = new RedDotNode(d);
            node.OnCreate(root, father);
            father.AddChild(node);//¸æËßËû°Ö

            RedDotManager.AddNodeToMap(node);
            return node;
        }
        public static RedDotLeaf CreateLeaf(RedDotData d, RedDotNode father, RedDotTriggerBase checker = null, int index = -1)
        {
            if (checker == null)
                checker = RedDotManager.GetChecker(d.Id);
            var leaf = new RedDotLeaf(d);
            leaf.OnCreate(null, father, checker, index);
            father.AddChild(leaf);
            RedDotManager.AddNodeToMap(leaf);
            return leaf;
        }
         

    }
}
