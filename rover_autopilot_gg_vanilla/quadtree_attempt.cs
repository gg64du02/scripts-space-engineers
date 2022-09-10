// opengenus quadtree converted


// Echo(TL);
// Echo(TR);
// Echo(BR);
// Echo(BL);

// struct Point{
    // int x;
    // int y;

    // Point() : x(-1), y(-1) {}

    // Point(int a, int b) : x(a), y(b) {}
// };


public class QuadTree{
	
	int TL = 0;    // top left
	int TR = 1;    // top right
	int BR = 2;    // bottom right
	int BL = 3;    // bottom left


	Point nullPoint = new Point(2049,2049);
		
    // if point == NULL, node is regional.
    // if point == (-1, -1), node is empty.
    Point point;

    Point top_left;
	Point bottom_right;   // represents the space.
    //std::vector<QuadTree *> children;
	List<QuadTree> children;
	
	public QuadTree(){
        // to declare empty node
        point = new Point();
	}
	
	public QuadTree(int x, int y){
        // to declare point node
        point = new Point(x, y);
    }
	
	public QuadTree(int x1, int y1, int x2, int y2){
        if(x2 < x1 || y2 < y1)
            return;
        // point = nullptr;
        point = nullPoint;
        top_left = new Point(x1, y1);
        bottom_right = new Point(x2, y2);
        // children.assign(4, nullPoint);
        children = new List<QuadTree>();
        for(int i = TL; i <= BL; ++i)
            children[i] = new QuadTree();
    }

	

    public void insert(int x, int y){
        // if(x < top_left->x || x > bottom_right->x
            // || y < top_left->y || y > bottom_right->y)
        if(x < top_left.X || x > bottom_right.X
            || y < top_left.Y || y > bottom_right.Y)
            return;
        int midx = (top_left.X + bottom_right.X) >> 1,
            midy = (top_left.Y + bottom_right.Y) >> 1;
        int pos = -1;
        if(x <= midx){
            if(y <= midy)
                pos = TL;
            else
                pos = BL;
        }
        else{
            if(y <= midy)
                pos = TR;
            else
                pos = BR;
        }

        if(children[pos].point == nullPoint){
           // if region node
            children[pos].insert(x, y);
            return;
        }
        else if(children[pos].point.X == -1){
            // if empty node
            // delete children[pos];
			children.RemoveAt(pos);
            children[pos] = new QuadTree(x, y);
            return;
        }
        else{
            int x_ = children[pos].point.X,
                y_ = children[pos].point.Y;
            // delete children[pos];
			children.RemoveAt(pos);
			QuadTree nullQuadTree = new QuadTree(2049,2049);
            children[pos] = nullQuadTree;
            if(pos == TL){
                children[pos] = new QuadTree(top_left.X, top_left.Y,
                                        midx, midy);
            }
            else if(pos == TR){
                children[pos] = new QuadTree(midx + 1, top_left.Y,
                                        bottom_right.X, midy);
            }
            else if(pos == BR){
                children[pos] = new QuadTree(midx + 1, midy + 1,
                                        bottom_right.X, bottom_right.Y);
            }
            else{
                children[pos] = new QuadTree(top_left.X, midy + 1,
                                        midx, bottom_right.Y);
            }
            children[pos].insert(x_, y_);
            children[pos].insert(x, y);
        }
    }


    bool find(int x, int y){
        if(x < top_left.X || x > bottom_right.X
            || y < top_left.Y || y > bottom_right.Y)
            // return 0;
            return false;
        int midx = (top_left.X + bottom_right.X) >> 1,
            midy = (top_left.Y + bottom_right.Y) >> 1;
        int pos = -1;
        if(x <= midx){
            if(y <= midy)
                pos = TL;
            else
                pos = BL;
        }
        else{
            if(y <= midy)
                pos = TR;
            else
                pos = BR;
        }
        if(children[pos].point == nullPoint){
           // if region node
            return children[pos].find(x, y);
        }
        else if(children[pos].point.X == -1){
            // if empty node
            // return 0;
            return false;
        }
        else{
            if(x == children[pos].point.X && y == children[pos].point.Y)
                // return 1;
                return true;
        }
        // return 0;
		return false;
    }



}