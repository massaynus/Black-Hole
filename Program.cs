using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using static System.Console;


namespace BlackHole
{
    class Program
    {
        static void Main(string[] args)
        {
            blackhole hole = new blackhole(0, 3, new int[] { 2, 2, 2});

            int t1 = (((hole.Children[1] as blackhole).Children[1] as blackhole).Children[0] as blackhole).getIndex();
            int t2 = (((hole.Children[1] as blackhole).Children[1] as blackhole).Children[1] as blackhole).getIndex();
            int t4 = (((hole.Children[0] as blackhole).Children[1] as blackhole).Children[1] as blackhole).getIndex();


            WriteLine(GC.GetTotalMemory(true));
        }
    }

    class blackhole //:IList
    {

        //TODO: implement Add, Search, Remove, Retrieve, Containes
        int dimensions, id, currentDimension;
        public int Dimensions { get { return dimensions; } }
        public int Id { get { return id; } }
        public int CurrentDimension { get { return currentDimension; } }
        int[] clusterSizes;

        bool hasChildren, root;
        blackhole parent;

        ArrayList children;
        public ArrayList Children { get { return children; } }

        ArrayList values;
        public blackhole() { }
        ///<summary>This Type was Made By ELMASSAOUDI YASSINE, please check the github repo for documentation at github.com/massaynus</summary>
        ///<param name="currentDimension">Just don't pass anything here</param>
        ///<param name="Dimensions">How many Dimensions You need, The Higher the number the faster you'll get your data, be reasonable though</param>
        ///<param name="clusterSizes">How many objects you want in a single cluster, the less here the better, but be cautious</param>
        public blackhole(int currentDimension, int Dimensions, params int[] clusterSizes)
        {
            this.dimensions = Dimensions;
            this.clusterSizes = clusterSizes;
            this.currentDimension = currentDimension;
            children = new ArrayList();

            if (Dimensions > 0)
            {
                this.hasChildren = true;
                for (int i = 0; i < clusterSizes[0]; i++)
                {
                    int[] Temp = new int[clusterSizes.Length - 1];
                    for (int y = 0; y < Temp.Length; y++)
                    {
                        Temp[y] = clusterSizes[y + 1];
                    }

                    this.children.Add(new blackhole(this.currentDimension + 1, Dimensions - 1, Temp) { id = i, parent = this });
                }
            }
            else
            {
                this.hasChildren = false;
            }

            if (this.currentDimension == 0)
            {
                root = true;
                parent = null;
            }
        }
        ulong Hash(string input)
        {
            ulong hash = 0;
            using (SHA1 sha1 = SHA1.Create())
            {
                hash = BitConverter.ToUInt64(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)), 0);
            }
            return hash;
        }

        public bool isLeaf() => !this.hasChildren;
        public bool isRoot() => this.root;

        public blackhole firstKey()
        {
            throw new NotImplementedException();
            // if (this.isLeaf()) return this;
            // else return (this.children[0] as blackhole).firstKey();
            
        }

        public void Add(string key, object value)
        {
            throw new NotImplementedException();

            // ulong keyHash = Hash(key);
            // blackhole temp = this.firstKey();
        }

        public int getIndex()
        {
            if (!isLeaf()) return -1;
            else
            {
                int index = this.id;
                blackhole temp = this;

                do
                {
                    index += temp.parent.id * temp.parent.children.Count;
                    temp = temp.parent;
                } while (temp.parent != null);

                return index;
            }
        }
    }
}